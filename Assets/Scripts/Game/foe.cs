using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foe : MonoBehaviour {
	public	bool Shield_right;
	public	GameObject	shield_right;

	public	bool Shield_left;
	public	GameObject	shield_left;

	public	bool Shield_front;
	public	GameObject	shield_front;

	public	bool Shield_back;
	public	GameObject	shield_back;

	// Use this for initialization
	void Start () {
		shield_right.SetActive(Shield_right);
		shield_left.SetActive(Shield_left);
		shield_front.SetActive(Shield_front);
		shield_back.SetActive(Shield_back);
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnParticleCollision(GameObject other) {
		Debug.Log("DEAD");
	}
}
