﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foe : MonoBehaviour {
	
	public	GM			gm;
	[Space]
	public	bool Shield_right;
	public	GameObject	shield_right;

	public	bool Shield_left;
	public	GameObject	shield_left;

	public	bool Shield_front;
	public	GameObject	shield_front;

	public	bool Shield_back;
	public	GameObject	shield_back;
	[Space]
	public	int			blank_left;
	public	GameObject	blankPrefab;

	// Use this for initialization
	void Start () {

		int level = Global.GetGameLevel();

		switch (level)
		{
			case 0:
				break ;
			case 1:
				shield_back.SetActive(true);
				break ;
			case 2:
				shield_right.SetActive(true);
				shield_left.SetActive(true);
				break ;
			case 3:
				shield_right.SetActive(true);
				shield_left.SetActive(true);
				shield_front.SetActive(true);
				shield_back.SetActive(true);
				break ;
		}

		// shield_right.SetActive(Shield_right);
		// shield_left.SetActive(Shield_left);
		// shield_front.SetActive(Shield_front);
		// shield_back.SetActive(Shield_back);
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Blank() {
		Instantiate(blankPrefab, transform.position, Quaternion.identity, transform);
		blank_left -= 1;
	}
	void OnParticleCollision(GameObject other) {
		Debug.Log("DEAD");
		if (blank_left > 0)
			Blank();
		else
			gm.Win(true);
	}
}
