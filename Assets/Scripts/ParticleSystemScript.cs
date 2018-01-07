using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public static class ParticleSystemScript
{
	static ParticleSystem ps;

	// ParticleSystemDataCircle psdci { get { return psd as ParticleSystemDataCircle; } }
	// ParticleSystemDataCone psdco { get { return psd as ParticleSystemDataCone; } }
	// ParticleSystemDataDonut psddo { get { return psd as ParticleSystemDataDonut; } }
	// ParticleSystemDataEdge psded { get { return psd as ParticleSystemDataEdge; } }

	// Use this for initialization

	static ParticleSystemShapeMultiModeValue		SetPSShape(ParticleSystemData psd)
	{
		if (psd.mode == ParticleEmissionMode.Loop)
		{
			Debug.Log("loop");
			return(ParticleSystemShapeMultiModeValue.Loop);
		}
		if (psd.mode == ParticleEmissionMode.PingPong)
		{
			Debug.Log("ping");
			return(ParticleSystemShapeMultiModeValue.PingPong);
	}
		if (psd.mode == ParticleEmissionMode.BurstSpread)
		{
			Debug.Log("burst");
			return(ParticleSystemShapeMultiModeValue.BurstSpread);
		}
		return(ParticleSystemShapeMultiModeValue.Random);
	}

	static public bool	setconestartrot = false;

	public static void SetPSFromData(ParticleSystem ps, ParticleSystemData psd)
	{

		//ps.Stop();
		var main = ps.main;
		main.startDelay = psd.startDelay;
	//	main.duration = psd.duration;
		main.startLifetime = psd.lifetime;
		main.startSpeed = psd.speed;
		main.startSize = psd.size;

		var em = ps.emission;
		if (psd.isBurst == true)
		{
			em.rateOverTime = 0;
			em.SetBursts(new ParticleSystem.Burst[] {new ParticleSystem.Burst(0f, psd.burstCount, psd.burstCount, psd.burstCycles, psd.burstinterval)});
		}
		else
		{
			em.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 0, 0) });
			em.rateOverTime= psd.rate;
		}

		var sh = ps.shape;
		if (psd.shape == ParticleEmissionShape.Cone)
		{
			sh.shapeType = ParticleSystemShapeType.Cone;
			sh.arcMode = SetPSShape(psd);
			sh.angle = psd.angle;
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(90f, 0, 0);
			sh.scale = new Vector3(psd.xscale, 0, psd.zscale);
			ps.transform.eulerAngles =  new Vector3(0, 0, psd.zrot);
			ps.transform.Rotate(0, 0, psd.zrotGameObject);
		}
		else if (psd.shape == ParticleEmissionShape.Donut)
		{
			sh.shapeType = ParticleSystemShapeType.Donut;
			sh.arcMode = SetPSShape(psd);
			sh.donutRadius = psd.donutRadius;
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(0, 0, psd.zrot);
			sh.scale = new Vector3(psd.xscale, psd.yscale, 0);
			ps.transform.Rotate(0, 0, psd.zrotGameObject);
			
		}
		else if (psd.shape == ParticleEmissionShape.Edge)
		{
			Debug.Log("Edge");
			sh.shapeType = ParticleSystemShapeType.SingleSidedEdge;
			sh.arcMode = SetPSShape(psd);
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(0, 0, psd.zrot);
			sh.scale = new Vector3(1, 1, 1);
			ps.transform.Rotate(0, 0, psd.zrotGameObject);
		}
		else if (psd.shape == ParticleEmissionShape.Circle)
		{
			sh.shapeType = ParticleSystemShapeType.Circle;
			sh.arcMode = SetPSShape(psd);
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(0, 0, psd.zrot);
			sh.scale = new Vector3(psd.xscale, psd.yscale, 0);
			ps.transform.Rotate(0, 0, psd.zrotGameObject);
			
		}
		var FOL = ps.forceOverLifetime;
		FOL.x = psd.forceOverLifetime.x;
		FOL.y = psd.forceOverLifetime.y;
		FOL.z = psd.forceOverLifetime.z;
		
		var VOL = ps.velocityOverLifetime;
		VOL.x = psd.velocityOverLifetime.x;
		VOL.y = psd.velocityOverLifetime.y;
		VOL.z = psd.velocityOverLifetime.z;
		//ps.Play();
	}

	static public	void	SetPSHromData(GameObject psHolder, ParticleSystemData psd)
	{
		ps = psHolder.GetComponent<ParticleSystem>();
		SetPSFromData(ps, psd);
	}

	static public PlayerPattern GetPSListFromDataList(PatternData pd)
	{
		PlayerPattern playerPattern = new PlayerPattern();
		foreach (ParticleSystemData psd in pd.particlePatterns)
		{
			GameObject psh = new GameObject();
			ParticleSystem tmpps = psh.AddComponent<ParticleSystem>();
			SetPSHromData(psh, psd);
			playerPattern.particleSystems.Add(tmpps);
		}

		playerPattern.cooldown = pd.cooldown;
		playerPattern.duration = pd.duration;
		return playerPattern;
	}

}
