using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour 
{
	ParticleSystem ps;

	float	duration;
	float	speed;
	float	size;
	Color	color;
	float	rate;


	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
