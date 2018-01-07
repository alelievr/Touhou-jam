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

	[Space]
	public GameObject		configParticleSystemPanel;
	[HideInInspector]
	public GameObject		configPatternPanel;

	[HideInInspector]
	public PatternData		currentPatternData;

	public static ConfigPatterns	instance;

	[System.NonSerialized]
	int		currentSave = 0;
	[System.NonSerialized]
	int		currentSpellcard = 0;
		[System.NonSerialized]
	int		currentParticleSystem = 0;

	void Awake()
	{
		instance = this;
		configPatternPanel = gameObject;
	}

	void Start()
	{
		LoadSave(Global.GetCurrentSaveIndex());
		UpdatePatternData();
	}

	public void LoadSpellcard(int index)
	{
		currentSpellcard = index;
		UpdatePatternData();
	}

	public void LoadSave(int index)
	{
		saveButtons[currentSave].sprite = greyButtonSprite;
		currentSave = index;
		saveButtons[currentSave].sprite = greenButtonSprite;
		UpdatePatternData();
		
		Global.SetCurrenSaveIndex(index);
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
				ParticleSystemScript.SetPSFromData(previewParticleSystems[i], currentPatternData.particlePatterns[i]);
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
		if (index >= currentPatternData.particlePatterns.Count)
		{
			currentPatternData.particlePatterns.Add(new ParticleSystemData());
		}

		configPatternPanel.SetActive(false);
		configParticleSystemPanel.SetActive(true);
	}

	public void SaveCurrentPattern()
	{
		if (currentPatternData == null)
			return ;
		
		Debug.Log("saving " + currentPatternData.name);

		BinaryLoader.SavePatternData(currentPatternData, currentSpellcard, currentSave);

		UpdatePatternData();
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
		
		Debug.Log("ist: " + instance.currentParticleSystem);
		Debug.Log("ist: " + instance.currentPatternData.particlePatterns.Count);
		return instance.currentPatternData.particlePatterns[instance.currentParticleSystem];
	}
}
