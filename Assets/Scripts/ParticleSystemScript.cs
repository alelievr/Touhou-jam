using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ParticleSystemScript : MonoBehaviour 
{
	ParticleSystem ps;

	ParticleSystemDataCircle psdci { get { return psd as ParticleSystemDataCircle; } }
	ParticleSystemDataCone psdco { get { return psd as ParticleSystemDataCone; } }
	ParticleSystemDataDonut psddo { get { return psd as ParticleSystemDataDonut; } }
	ParticleSystemDataEdge psded { get { return psd as ParticleSystemDataEdge; } }
	
	ParticleSystemData	psd;

	float	startDelay;
	float	duration;
	float	speed;
	float	size;
	// Color	color;
	float	rate;
	bool	isBurst;
	float	burstCount;
	float	burstCycles;
	float	burstinterval;

	ParticleEmissionShape	shape;
	ParticleEmissionMode	mode;

	Vector3	velocityOverLifetime;
	Vector3	forceOverLifetime;

	float	radius;
	float	donutRadius;
    float	arc;
  	float	angle;
    float	rotspeed;

    float	zrot;
    float	zrotGameObject;
	
    float	xscale;
    float	yscale;
 	float	zscale;
	// Use this for initialization
	void Start () {
	}

	ParticleSystemShapeMultiModeValue		SetPSShape()
	{

		if (psd.mode == ParticleEmissionMode.Loop)
			return(ParticleSystemShapeMultiModeValue.Loop);
		if (psd.mode == ParticleEmissionMode.PingPong)
			return(ParticleSystemShapeMultiModeValue.PingPong);
		if (psd.mode == ParticleEmissionMode.BurstSpread)
			return(ParticleSystemShapeMultiModeValue.BurstSpread);
		return(ParticleSystemShapeMultiModeValue.Random);
	}

	void	SetPSFromData(GameObject psHolder)
	{
		ps = psHolder.GetComponent<ParticleSystem>();
		ps.Stop();
		var main = ps.main;
		main.startDelay = psd.startDelay;
		main.duration = psd.duration;
		main.startSpeed = psd.speed;
		main.startSize = psd.size;
		main.startLifetime = 20f;

		var em = ps.emission;
		if (psd.isBurst == true)
		{
			em.SetBurst(1, new ParticleSystem.Burst(0f, psd.burstCount, psd.burstCount, psd.burstCycles, psd.burstinterval));
		}
		else
		{
			em.rateOverTime= psd.rate;
		}

		var sh = ps.shape;
		if (psd.shape == ParticleEmissionShape.Cone)
		{
			sh.shapeType = ParticleSystemShapeType.Cone;
			sh.arcMode = SetPSShape();
			sh.angle = psdco.angle;
			sh.radius = psdco.radius;
			sh.arc = psdco.arc;
			sh.arcSpeed = psdco.rotspeed;
			sh.rotation = new Vector3(90f, 0, 0);
			sh.scale = new Vector3(psdco.xscale, 0, psdco.zscale);
			psHolder.transform.Rotate(0, 0, psdco.zrotGameObject);
		}
		if (psd.shape == ParticleEmissionShape.Donut)
		{
			sh.shapeType = ParticleSystemShapeType.Donut;
			sh.arcMode = SetPSShape();
			sh.donutRadius = psddo.donutRadius;
			sh.radius = psddo.radius;
			sh.arc = psddo.arc;
			sh.arcSpeed = psddo.rotspeed;
			sh.rotation = new Vector3(0, 0, psddo.zrot);
			sh.scale = new Vector3(psddo.xscale, psddo.yscale, 0);

		}
		if (psd.shape == ParticleEmissionShape.Edge)
		{
			sh.shapeType = ParticleSystemShapeType.SingleSidedEdge;
			sh.arcMode = SetPSShape();
			sh.radius = psdci.radius;
			sh.arc = psdci.arc;
			sh.arcSpeed = psdci.rotspeed;
			sh.rotation = new Vector3(0, 0, psdci.zrot);
		}
		if (psd.shape == ParticleEmissionShape.Circle)
		{
			sh.shapeType = ParticleSystemShapeType.Circle;
			sh.arcMode = SetPSShape();
			sh.radius = psdci.radius;
			sh.arc = psdci.arc;
			sh.arcSpeed = psdci.rotspeed;
			sh.rotation = new Vector3(0, 0, psdci.zrot);
			sh.scale = new Vector3(psdci.xscale, psdci.yscale, 0);
		}
		var FOL = ps.forceOverLifetime;
		FOL.x = psd.forceOverLifetime.x;
		FOL.y = psd.forceOverLifetime.y;
		FOL.z = psd.forceOverLifetime.z;
		
		var VOL = ps.velocityOverLifetime;
		VOL.x = psd.velocityOverLifetime.x;
		VOL.y = psd.velocityOverLifetime.y;
		VOL.z = psd.velocityOverLifetime.z;
		ps.Play();
	}

	public List< ParticleSystemData> particlePatterns;
	List<ParticleSystem> psl;

	List<ParticleSystem> GetPSListFromDataList()
	{
		foreach (ParticleSystemData psd in particlePatterns)
		{
			GameObject psh = new GameObject();
			ParticleSystem tmpps = psh.AddComponent<ParticleSystem>();
			SetPSFromData(psh);
			psl.Add(tmpps);
		}
	}
}
