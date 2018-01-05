﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seikuken : MonoBehaviour {
	public	List<ParticleSystem.Particle> inside;
	public	GameObject	Dodger;
	public	GameObject	Limits;

	private	Rigidbody	rb;
	private	bool		inLimits;	
	// Use this for initialization
	void Start () {
		rb = Dodger.GetComponent<Rigidbody>();
		inLimits = true;
	}
	
	void FixedUpdate()
	{
		
	}

	// Update is called once per frame
	void Update () {
		Refresh();
		Dodge();
	}

	void Refresh()
	{
		rb.velocity = Vector3.zero;
	}

	void Dodge()
	{
		if (inside != null) {
			Vector3 dir = new Vector3(0,0);
			for (int i = 0; i < inside.Count; i++) {
				// Debug.Log(inside[i].position);
				dir += transform.position - inside[i].position;
			}
			if (!inLimits) {
				dir += Limits.transform.position - transform.position;
				Debug.Log(Limits.transform.position - transform.position);
			}
			// Debug.Log(dir);
			rb.AddForce(dir * 15);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		inLimits = true;
	}

	void OnTriggerExit(Collider other)
	{
		inLimits = false;
	}
	
	void OnCollisionStay(Collision other)
	{
		Debug.Log("colision");
	}
	
	void OnParticleCollision(GameObject other) {
		Debug.Log("touchew");
	}
}
