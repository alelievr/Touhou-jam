using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seikuken : MonoBehaviour {
	public	List<ParticleSystem.Particle> inside;
	public	GameObject	Dodger;
	// public	GameObject	Limits;
	public	GameObject	Player;

	[Space]
	public float		dodgePower;

	private	Rigidbody	rb;
	private	bool		inLimits;
	private bool		busy;
	private	float		starty;
	private Vector3		dodgerpos;
	// Use this for initialization
	void Start () {
		rb = Dodger.GetComponent<Rigidbody>();
		inLimits = true;
		busy = false;
		starty = transform.position.y;
	}
	
	void FixedUpdate() {
		Refresh();
		if (!Dodge())
			Move();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void Refresh() {
		rb.velocity = Vector3.zero;
		busy = false;
	}

	bool Move() {	
		return true;
	}

	bool Dodge() {
		Vector3 dir = Vector3.zero;
		if (inside != null) {
			for (int i = 0; i < inside.Count; i++) {
				// Debug.Log((inside[i].position - transform.position).magnitude);
				Vector3 delta = inside[i].position - transform.position;
				Vector3 direction = -delta.normalized;
				float directionForce = dodgePower / delta.magnitude;
				Debug.DrawRay(transform.position, direction * directionForce, Color.cyan);
				dir += direction * directionForce; // prendre radius
			}
		}
		// if (!inLimits) {
		// 	dir += (Limits.transform.position - transform.position);
		// 	// Debug.Log(Limits.transform.position - transform.position);
		// }
		
		dir = new Vector3(dir.x * 3f, dir.y, dir.z);
		
		// reste en face du joueur et ne monte pas trop haut
		Vector3 targetx = new Vector3(Player.transform.position.x, starty, 0);
		Vector3 deltaX = targetx - transform.position;
		Vector3 directionX = deltaX.normalized * Mathf.Clamp(Mathf.Exp(deltaX.magnitude / 3), 0, 1000);
		Debug.DrawRay(transform.position, directionX, Color.red);
		dir += directionX;

		Vector3 targety = new Vector3(0, starty, 0);
		dir += (targety - transform.position) / 1.5f;
		Debug.DrawRay(transform.position,dir, Color.green);
		
		if (dir != Vector3.zero){
			rb.AddForce(dir * 100);
			return true;
		}
		return false;
	}

	Vector3 getDodgerPos() {
		return (new Vector3(transform.position.x, transform.position.y + 0.3f, 0));
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
