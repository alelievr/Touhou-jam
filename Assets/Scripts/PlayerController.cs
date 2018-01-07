using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public List< PlayerPattern >	patterns = new List< PlayerPattern >();

	public static PlayerController 	instance;

	[HideInInspector]
	public int						activePattern = 0;

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


}
