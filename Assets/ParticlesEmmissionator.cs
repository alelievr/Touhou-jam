using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEmmissionator : MonoBehaviour {

	new ParticleSystem	particleSystem;

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent< ParticleSystem >();
	}

	void FixedUpdate()
	{
		//particleSystem.Emit(1);
	}

}
