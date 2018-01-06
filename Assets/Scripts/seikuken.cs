using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
	public	List<ParticleSystem.Particle> inside;
	public	GameObject	Dodger;
	public	GameObject	Limits;

	private	Rigidbody	rb;
	private	bool		inLimits;

	public enum 		edashtype { throught, fuite, right, left, randomaxewithouttrought, random};

	public	bool		candash = false;
	public	float		dashdist = 3;
	public	float		dashtime = 0.2f;
	public	float		dashdisttrigger = 0.2f;
	public	edashtype	dashtype = edashtype.fuite;
	private float		dashdisttraveled = 0;
	public	float		cddash = 5;
	private float		cdcurrent = 0;


	// Use this for initialization
	void Start () {
		rb = Dodger.GetComponent<Rigidbody>();
		inLimits = true;
		cdcurrent = cddash;
	}

	void FixedUpdate()
	{
		Refresh();
		Dodge();
	}

	// Update is called once per frame
	void Update () {
		Refresh();
		Dodge();
		if (cdcurrent >= 0)
			cdcurrent -= Time.deltaTime;
	}

	void Refresh()
	{
		rb.velocity = Vector3.zero;
	}

	private Vector2 vectdirdash;
	private Vector2 destdash;

	void dash(Vector2 vectm)
	{
		edashtype tmpdashtype = dashtype;
		if (dashdisttraveled == 0)
		{
			if (dashtype == edashtype.randomaxewithouttrought)
				tmpdashtype = (edashtype)Random.Range((int)edashtype.fuite, (int)edashtype.randomaxewithouttrought);

			if (tmpdashtype == edashtype.random)
				vectdirdash = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
			else if (tmpdashtype == edashtype.fuite)
				 vectdirdash = -vectm;
			else if (tmpdashtype == edashtype.throught)
				 vectdirdash = -vectm;
			else if (tmpdashtype == edashtype.left)
				vectdirdash = vectm.Rotate(-90f);
			else if (tmpdashtype == edashtype.right)
				vectdirdash = vectm.Rotate(90f);
			vectdirdash = vectdirdash.normalized;
			destdash = (Vector2)transform.position + vectdirdash * dashdist;
		}

		// the value of dashtime need to be greater than Time.deltaime (more than once)

		transform.position = Vector2.MoveTowards(transform.position, destdash, dashdist * Time.deltaTime / dashtime);
		dashdisttraveled += dashdist * Time.deltaTime / dashtime;
		if (dashdisttraveled >= dashdist)
			dashdisttraveled = 0;	
	}

	Vector2 getvectomostnear()
	{
		Vector2 ret = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		for (int i = 0; i < inside.Count; i++) {
				// Debug.Log(inside[i].position);
				if (ret.magnitude > (transform.position - inside[i].position).magnitude)
					ret = (transform.position - inside[i].position);
			}
		return ret;
	}

	void Dodge()
	{
		Vector3 dir = new Vector3(0, 0);

		if (inside != null) {
			Vector2 vectmn = getvectomostnear();
			if (dashdisttraveled != 0 || (candash == true && cddash < 0 && vectmn.magnitude < dashdisttrigger))
			{
				dash(vectmn);
				return ;
			}
			for (int i = 0; i < inside.Count; i++) {
				// Debug.Log(inside[i].position);
				dir += -(inside[i].position - transform.position).normalized * (1 - ((inside[i].position - transform.position).magnitude / 1.5f)); // prendre radius
			}
		}
		if (!inLimits) {
			dir += (Limits.transform.position - transform.position);
			// Debug.Log(Limits.transform.position - transform.position);
		}
		Debug.DrawRay(transform.position,dir, Color.green);
		rb.AddForce(dir * 100);
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
