using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class Vector2Extension {
     
     public static Vector2 Rotate(this Vector2 v, float degrees) {
         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }
}

public class seikuken : MonoBehaviour {
	public	List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
	public	GameObject	Dodger;
	// public	GameObject	Limits;
	public	GameObject	Player;

	[Space]
	public GM			gm;
	[Space]
	public float		dodgePower;

	private	Rigidbody	rb;
	private	bool		inLimits;
	private bool		busy;
	private	float		starty;
	private Vector3		dodgerpos;

	public enum 		edashtype { throught, fuite, right, left, randomaxewithouttrought, random, bcissapproved};

	public	bool		candash = false;
	public	float		dashdist = 3;
	public	float		dashtime = 0.2f;
	public	float		dashdisttrigger = 0.2f;
	public	edashtype	dashtype = edashtype.fuite;
	private float		dashdisttraveled = 0;
	public	float		cddash = 5;
	private float		cdcurrent = 0;

	public	float		speedMax = 100;
	public	float		speedPower = 75;
	

	// Use this for initialization
	void Start () {
		rb = Dodger.GetComponent<Rigidbody>();
		// HPtext = HPbar.GetComponent<Text>();
		inLimits = true;
		busy = false;
		starty = transform.position.y;
		cdcurrent = cddash;
		inside = new List<ParticleSystem.Particle>();
	}
	
	void FixedUpdate()
	{
		Refresh();
		Dodge();
		Move();
	}

	// Update is called once per frame
	void Update () {
		if (cdcurrent >= 0)
			cdcurrent -= Time.deltaTime;
	}

	void Refresh() {
		rb.velocity = Vector3.zero;
		busy = false;
	}

	Vector2 getvectomostnear()
	{
		Vector2 ret = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		for (int i = 0; i < inside.Count; i++) {
				// Debug.Log(inside[i].position);
				if (ret.magnitude > (transform.position - inside[i].position).magnitude)
					ret = (transform.position - inside[i].position);
			}
		return -ret;
	}

	/*STATIC*/private Vector2 vectdirdash;
	/*STATIC*/ private Vector2 destdash;

	void dash(Vector2 vectm, Vector3 dir)
	{
		edashtype tmpdashtype = dashtype;
		if (dashdisttraveled == 0)
		{
			if (dashtype == edashtype.randomaxewithouttrought)
				tmpdashtype = (edashtype)Random.Range((int)edashtype.fuite, (int)edashtype.randomaxewithouttrought);

			if (tmpdashtype == edashtype.random)
				vectdirdash = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
			else if (tmpdashtype == edashtype.fuite)
				 vectdirdash = -vectm;
			else if (tmpdashtype == edashtype.throught)
				 vectdirdash = -vectm;
			else if (tmpdashtype == edashtype.left)
				vectdirdash = vectm.Rotate(-90f);
			else if (tmpdashtype == edashtype.right)
				vectdirdash = vectm.Rotate(90f);
			else if (tmpdashtype == edashtype.bcissapproved)
				vectdirdash = dir;
			vectdirdash = vectdirdash.normalized;
			destdash = (Vector2)transform.position + vectdirdash * dashdist;
			rb.detectCollisions = false;
			Color tmp = Dodger.GetComponent<SpriteRenderer>().color;
			tmp.a = 0.5f;
			Dodger.GetComponent<SpriteRenderer>().color = tmp;
		}

		// the value of dashtime need to be greater than Time.deltaime (more than once)

		rb.transform.position = Vector2.MoveTowards(transform.position, destdash, dashdist * Time.deltaTime / dashtime);
		dashdisttraveled += dashdist * Time.deltaTime / dashtime;
		if (dashdisttraveled >= dashdist)
		{
			dashdisttraveled = 0;	
			cdcurrent = cddash;
			rb.detectCollisions = true;
			Color tmp = Dodger.GetComponent<SpriteRenderer>().color;
			tmp.a = 1f;
			Dodger.GetComponent<SpriteRenderer>().color = tmp;
		}
	}

	bool Move() {	
		return true;
	}

	void insiderefresh()
	{
		List<ParticleSystem> tmp = PlayerController.GetActiveParticleSystems();
		inside = new List<ParticleSystem.Particle>();
		List<ParticleSystem.Particle> inside2 = new List<ParticleSystem.Particle>();

		foreach(ParticleSystem ps in tmp)
		{
			Debug.Log(ps);
			foreach (var d in ps.GetComponent<TriggerDetector>().inside)
				Debug.Log("d: " + d);
			inside.AddRange(ps.GetComponent<TriggerDetector>().inside);
		}
	}

	void Dodge() {
		insiderefresh();
		Vector3 dir = Vector3.zero;
		if (inside != null) {
			for (int i = 0; i < inside.Count; i++) {
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
		Vector3 directionX = deltaX.normalized * Mathf.Clamp(Mathf.Pow(deltaX.magnitude, 2), 0, 1000);
		Debug.DrawRay(transform.position, directionX, Color.red);
		dir += directionX;

		Vector3 targety = new Vector3(0, starty, 0);
		dir += (targety - transform.position) / 2f;
		Debug.DrawRay(transform.position,dir, Color.green);
		
		Vector2 vectmn = getvectomostnear();
		if (dashdisttraveled != 0 || (candash == true && cdcurrent 	< 0 && vectmn.magnitude < dashdisttrigger))
		{
			Debug.Log("fdsf");
			dash(vectmn, dir);
			return ;
		}
		if (dir != Vector3.zero){
			dir = dir * speedPower;
			rb.AddForce(Vector3.ClampMagnitude(dir, speedMax));
		}
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
