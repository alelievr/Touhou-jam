using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderShowMode : MonoBehaviour {

		public Text sliderValue;
 		public Slider slider;
 
 	void Update()
	{
		if (slider.value == 0)
 			sliderValue.text = "Loop";
		if (slider.value == 1)
 			sliderValue.text = "PingPong";
		if (slider.value == 2)
 			sliderValue.text = "BurstSpread";
 	}
}
