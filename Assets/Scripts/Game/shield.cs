using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour {

	// Use this for initialization
	private		Rigidbody	rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity += new Vector3(0, 0.05f, 0);
	}

	void OnBecameInvisible()
	{
		GameObject.Destroy(gameObject);
	}
}
