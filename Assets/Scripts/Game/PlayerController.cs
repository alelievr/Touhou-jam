﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public GM 						gm;
	[HideInInspector]
	public List< PlayerPattern >	patterns = new List< PlayerPattern >();

	public static PlayerController 	instance;

	[HideInInspector]
	public int						activePattern = 0;

	public Slider		HPbar;
	private	Slider		HPSlider;
	public	float		HPmax = 1000000f;
	public	float		HP;

	new Rigidbody		rigidbody;

	Dictionary< KeyCode, int > patternBindings = new Dictionary< KeyCode, int >()
	{
		{KeyCode.None, 0}, //default attack
		{KeyCode.Alpha1, 1},
		{KeyCode.Alpha2, 2},
		{KeyCode.Alpha3, 3},
		{KeyCode.Alpha4, 4},
	};

	void Awake()
	{
		instance = this;
	}

	public static List< ParticleSystem > GetActiveParticleSystems()
	{
		if (instance == null)
			return null;
		
		return instance.patterns[instance.activePattern].particleSystems;
	}

	void Start ()
	{
		rigidbody = GetComponent< Rigidbody >();
		HPSlider = HPbar.GetComponent<Slider>();

		HP = HPmax;

		//Load the spellcards using the current save number;

		int saveIndex = Global.GetCurrentSaveIndex();

		patterns.Clear();

		for (int i = 0; i < 5; i++)
		{
			PatternData data = BinaryLoader.LoadPatternData(i, saveIndex);

			if (data != null)
			{
				var pattern = ParticleSystemScript.GetPSListFromDataList(data, transform);

				patterns.Add(pattern);
			}
		}

		//disable all particle system emissions
		foreach (var kp in patterns)
			foreach (var system in kp.particleSystems)
			{
				var emission = system.emission;
				emission.enabled = false;
			}
		
		ActivateSpellCard(0);
	}
	
	void Update ()
	{
		HPSlider.value = -((HP / HPmax) * 100);
		foreach (var kp in patternBindings)
			if (Input.GetKeyDown(kp.Key))
			{
				if (GameGUIManager.IsSpellcardInCooldown(kp.Value - 1))
					continue ;
				
				Debug.Log(kp.Value != activePattern);
				if (kp.Value != activePattern)
					ActivateSpellCard(kp.Value);
			}
		
		Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		rigidbody.velocity = move;
	}

	IEnumerator RestartDefaultPattern(float cooldown)
	{
		yield return new WaitForSeconds(cooldown);

		ActivateSpellCard(0);
	}

	void ActivateSpellCard(int spellcardIndex)
	{
		int oldActivePattern = activePattern;

		activePattern = spellcardIndex;

		foreach (var particleSystem in patterns[oldActivePattern].particleSystems)
		{
			var emission = particleSystem.emission;
			emission.enabled = false;
		}

		foreach (var particleSystem in patterns[activePattern].particleSystems)
		{
			var emission = particleSystem.emission;
			emission.enabled = true;
		}

		if (spellcardIndex != 0)
		{
			Debug.Log("pattern: " + activePattern);
			GameGUIManager.ActivateSpellCard(activePattern - 1, patterns[activePattern].cooldown, patterns[activePattern].duration);
			StartCoroutine(RestartDefaultPattern(patterns[activePattern].cooldown));
		}
	}
	
	void OnParticleCollision(GameObject other) {
		Debug.Log("touchew");
		if (HP <= 0)
			gm.Win(false);
			
		gm.AddPts(-12500);
		if (HP < HPmax / 4)
			HP -= 1000;
		else
			HP -= 4000;
	}
}
