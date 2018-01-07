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
		{KeyCode.Alpha1, 0},
		{KeyCode.Alpha2, 1},
		{KeyCode.Alpha3, 2},
		{KeyCode.Alpha4, 3},
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
	}
	
	void Update ()
	{
		foreach (var kp in patternBindings)
			if (Input.GetKeyDown(kp.Key))
			{
				int oldActivePattern = activePattern;
				activePattern = kp.Value;

				if (oldActivePattern != activePattern)
				{
					foreach (var particleSystem in patterns[oldActivePattern].particleSystems)
						particleSystem.gameObject.SetActive(false);
					foreach (var particleSystem in patterns[activePattern].particleSystems)
						particleSystem.gameObject.SetActive(true);
					GameGUIManager.ActivateSpellCard(activePattern, patterns[activePattern].cooldown, patterns[activePattern].duration);
				}
			}
		
		Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		rigidbody.velocity = move;
	}


}
