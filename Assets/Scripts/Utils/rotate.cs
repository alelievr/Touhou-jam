using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	public	float	min = -1;
	public	float	max = -1;
	public	float	speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (min == -1 && max == -1) {
			transform.Rotate (new Vector3(0,0,1) * Time.deltaTime * speed, Space.World);
		} else {
			// Debug.Log (transform.rotation.y * 360);
			if (transform.rotation.z * 360 < min || transform.rotation.z * 360 > max) {
				speed *= -1;
			}
			transform.Rotate (new Vector3(0,0,1) * Time.deltaTime * -speed, Space.World);
		}
	}
}
