using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class ConfigPatterns : MonoBehaviour
{
	public InputField		spellcardName;

	[Space]
	public Text[]			spellcardNames;

	public ParticleSystem[]	previewParticleSystems;

	[Space]
	public Sprite			greenButtonSprite;
	public Sprite			greyButtonSprite;
	public Image[]			saveButtons;

	[HideInInspector]
	public PatternData		currentPatternData;

	public static ConfigPatterns	instance;

	int		currentSave = 0;
	int		currentSpellcard = 0;
	int		currentParticleSystem = 0;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		UpdatePatternData();
	}

	public void LoadSpellcard(int index)
	{
		currentSpellcard = index;
		UpdatePatternData();
	}

	public void LoadSave(int index)
	{
		SaveCurrentPattern();
		saveButtons[currentSave].sprite = greyButtonSprite;
		currentSave = index;
		saveButtons[currentSave].sprite = greenButtonSprite;
		UpdatePatternData();
	}

	void UpdatePatternData()
	{
		currentPatternData = BinaryLoader.LoadPatternData(currentSpellcard, currentSave);

		//if the file does not exists:
		if (currentPatternData == null)
		{
			currentPatternData = new PatternData();
			currentPatternData.name = "Spellcard name";
		}

		for (int i = 0; i < previewParticleSystems.Length; i++)
		{
			var emission = previewParticleSystems[i].emission;

			if (i < currentPatternData.particlePatterns.Count)
			{
				//TODO: update particle systems
				emission.enabled = true;
			}
			else
				emission.enabled = false;
		}

		spellcardName.text = currentPatternData.name;
	}

	public void ConfigureParticleSystem(int index)
	{
		currentParticleSystem = index;
	}

	public void SaveCurrentPattern()
	{
		if (currentPatternData == null)
			return ;

		BinaryLoader.SavePatternData(currentPatternData, currentSpellcard, currentSave);
	}

	void Update()
	{
		currentPatternData.name = spellcardName.text;
		spellcardNames[currentSpellcard].text = currentPatternData.name;
	}

	public void RefreshParticleSystems()
	{
		foreach (var ps in previewParticleSystems)
		{
			if (ps.emission.enabled)
			{
				ps.Clear();
				ps.Stop();
				ps.Play();
			}
		}
	}

	public static ParticleSystemData GetCurrentParticleSystemData()
	{
		if (instance == null || instance.currentPatternData == null)
			return null;
		
		return instance.currentPatternData.particlePatterns[instance.currentParticleSystem];
	}
}
