// LASP - Low-latency Audio Signal Processing plugin for Unity
// https://github.com/keijiro/Lasp

using UnityEngine;
using UnityEngine.Audio;

namespace Lasp
{
    // High-level audio input interface that provides the basic functionality
    // of LASP. 
    public static class AudioInput
    {
        #region Public methods

        // Returns the peak level during the last frame.
        public static float GetPeakLevel(FilterType filter)
        {
            UpdateState();
            return _filterBank[(int)filter].peak;
        }

        // Returns the peak level during the last frame in dBFS.
        public static float GetPeakLevelDecibel(FilterType filter)
        {
            // Full scale square wave = 0 dBFS : refLevel = 1
            return ConvertToDecibel(GetPeakLevel(filter), 1);
        }

        // Calculates the RMS level of the last frame.
        public static float CalculateRMS(FilterType filter)
        {
            UpdateState();
            return _filterBank[(int)filter].rms;
        }

        // Calculates the RMS level of the last frame in dBFS.
        public static float CalculateRMSDecibel(FilterType filter)
        {
            // Full scale sin wave = 0 dBFS : refLevel = 1/sqrt(2)
            return ConvertToDecibel(CalculateRMS(filter), 0.7071f);
        }

		static float[] audioSamplesR = new float[512];
		static float[] audioSamplesL = new float[512];
        static float[] rawAudioSamplesR = new float[512];
        static float[] rawAudioSamplesL = new float[512];
		static float audioVolume;
        static float rms;
        static float packed;

		static public float[] frequencyBandR = new float[8];
		static public float[] frequencyBandL = new float[8];
		static public float[] bandBufferR = new float[8];
		static public float[] bandBufferL = new float[8];
		static float[] bufferDecrease = new float[8];

		static float LinearToDecibel(float linear)
		{
			float dB;

			if (linear != 0)
				dB = 20.0f * Mathf.Log10(linear);
			else
				dB = -144.0f;

			return dB;
		}


		public static float CalculateRMSDecibel()
        {
            AudioListener.GetSpectrumData(audioSamplesR, 0, FFTWindow.BlackmanHarris);
            AudioListener.GetSpectrumData(audioSamplesL, 0, FFTWindow.BlackmanHarris);
            AudioListener.GetOutputData(rawAudioSamplesR, 0);
            AudioListener.GetOutputData(rawAudioSamplesL, 0);

            GetFrequencyBand(audioSamplesR, frequencyBandR);
            GetFrequencyBand(audioSamplesL, frequencyBandL);
	    	GetBandBuffer(bandBufferR, frequencyBandR);
	    	GetBandBuffer(bandBufferL, frequencyBandL);
            GetRMS();
            GetPacked();

			float m = 1e20f;
            Vector3 start = Vector3.zero;
			foreach (var f in bandBufferR)
            {
                Debug.DrawLine(start, start + Vector3.up * f, Color.green);
                start.x += .5f;
				m = Mathf.Min(m, f / 10 - .0f);
            }

            start.y = -2;
            start.x = 0;
            foreach (var f in audioSamplesR)
            {
                Debug.DrawLine(start, start + Vector3.up * f * 6, Color.red);
                start.x += .05f;
            }

            start.y = -4;
            start.x = 0;
            foreach (var f in frequencyBandR)
            {
                Debug.DrawLine(start, start + Vector3.up * f, Color.blue);
                start.x += .5f;
            }

            m = 0;
            bool ok = true;
            for (int i = 1; i < 7; i++)
                if (frequencyBandR[i] < .9f)
                    ok = false;
                    
            for (int i = 1; i < 7; i++)
                if (frequencyBandL[i] < .9f)
                    ok = false;

            m = (ok) ? 1 : 0;

            m = Mathf.Clamp01(m);

            return rms;
        }

        static void GetRMS()
        {
            float sum = 0; 

            foreach (var raw in rawAudioSamplesR)
                sum += raw;
            foreach (var raw in rawAudioSamplesL)
                sum += raw;
            
            rms = Mathf.Sqrt(Mathf.Abs(sum / (rawAudioSamplesR.Length + rawAudioSamplesL.Length)));
        }

        static void GetPacked()
        {
            float sum = 0;
            foreach (var raw in rawAudioSamplesL)
                sum += Mathf.Abs(raw);
            foreach (var raw in rawAudioSamplesR)
                sum += Mathf.Abs(raw);
            packed = sum / 1000f;
        }

		static void GetFrequencyBand(float[] samples, float[] frequencyBand)
		{
			int count = 0;

			for (int i = 0; i < frequencyBand.Length; i++)
			{
				float average = 0;
				int sampleCount = (int)Mathf.Pow(2, i) * 2;

				if (i == frequencyBand.Length - 1)
					sampleCount += 2;

				for (int j = 0; j < sampleCount; j++)
				{
					average += samples[count] * (count + 1);
					count++;
				}

				average /= count;
				frequencyBand[i] = average * 10;
			}
		}

		static void GetBandBuffer(float[] bandBuffer, float[] frequencyBand)
		{
			for (int i = 0; i < bandBuffer.Length; i++)
			{
				if (frequencyBand[i] > bandBuffer[i])
				{
					bandBuffer[i] = frequencyBand[i];
					bufferDecrease[i] = 0.05f;
				}

				if (frequencyBand[i] < bandBuffer[i])
				{
					bandBuffer[i] -= bufferDecrease[i];
					bufferDecrease[i] *= 1.2f;
				}
			}
		}


		// Retrieve and copy the waveform.
		public static void RetrieveWaveform(FilterType filter, float[] dest)
        {
            UpdateState();
            _stream.RetrieveWaveform(filter, dest, dest.Length);
        }

        #endregion

        #region Internal methods

        static LaspStream _stream;
        static FilterBlock[] _filterBank;
        static int _lastUpdateFrame;

        static float ConvertToDecibel(float level, float refLevel)
        {
            const float zeroOffset = 1.5849e-13f;
            return 20 * Mathf.Log10(level / refLevel + zeroOffset);
        }

        static void Initialize()
        {
            _stream = new Lasp.LaspStream();

            if (!_stream.Open())
                Debug.LogWarning("LASP: Failed to open the default audio input device.");

            LaspTerminator.Create(Terminate);

            _filterBank = new[] {
                new FilterBlock(FilterType.Bypass, _stream),
                new FilterBlock(FilterType.LowPass, _stream),
                new FilterBlock(FilterType.BandPass, _stream),
                new FilterBlock(FilterType.HighPass, _stream)
            };

            _lastUpdateFrame = -1;
        }

        static void UpdateState()
        {
            if (_stream == null) Initialize();

            if (_lastUpdateFrame < Time.frameCount)
            {
                foreach (var fb in _filterBank) fb.InvalidateState();
                _lastUpdateFrame = Time.frameCount;
            }
        }

        static void Terminate()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
                _filterBank = null;
            }
        }

        #endregion
    }
}
