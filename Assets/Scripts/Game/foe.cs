using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("trigeriser");
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log("colision");
	}
}
