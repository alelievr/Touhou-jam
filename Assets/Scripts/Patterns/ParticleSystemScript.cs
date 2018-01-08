using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 


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
			// Debug.Log("loop");
			return(ParticleSystemShapeMultiModeValue.Loop);
		}
		if (psd.mode == ParticleEmissionMode.PingPong)
		{
			// Debug.Log("ping");
			return(ParticleSystemShapeMultiModeValue.PingPong);
	}
		if (psd.mode == ParticleEmissionMode.BurstSpread)
		{
			// Debug.Log("burst");
			return(ParticleSystemShapeMultiModeValue.BurstSpread);
		}
		return(ParticleSystemShapeMultiModeValue.Random);
	}

	static public bool	setconestartrot = false;
	public static bool playeditorparticle = true;

	public static void SetPSFromData(ParticleSystem ps, ParticleSystemData psd)
	{	
		var main = ps.main;
		// ps.Stop();
		// main.duration = psd.duration;
		if (playeditorparticle == true)
		{
			ps.Stop();
			main.duration = psd.duration;
			ps.Play();
			playeditorparticle = false;
		}
		//ps.Stop();
		main.maxParticles = 20000;
		main.simulationSpace = ParticleSystemSimulationSpace.World;
		main.startDelay = psd.startDelay;
		main.loop = false;
		// main.duration = psd.duration;
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
		FOL.enabled = true;
		FOL.x = psd.xforce;
		FOL.y = psd.yforce;
		
		var VOL = ps.velocityOverLifetime;
		VOL.x = psd.xvel;
		VOL.y = psd.yvel;

		//ps.Play();
	}

	static public	void	SetPSHromData(GameObject psHolder, ParticleSystemData psd)
	{
		ps = psHolder.GetComponent<ParticleSystem>();
		SetPSFromData(ps, psd);
	}

	static public PlayerPattern GetPSListFromDataList(PatternData pd, Transform parent)
	{
		GameObject prefab = Resources.Load< GameObject >("defaultParticleSsytem");

		PlayerPattern playerPattern = new PlayerPattern();
		foreach (ParticleSystemData psd in pd.particlePatterns)
		{
			GameObject psh = GameObject.Instantiate(prefab, parent.position, parent.rotation, parent);
			ParticleSystem tmpps = psh.GetComponent<ParticleSystem>();
			SetPSHromData(psh, psd);
			playerPattern.particleSystems.Add(tmpps);
		}

		playerPattern.cooldown = pd.cooldown;
		playerPattern.duration = pd.duration;
		return playerPattern;
	}

}
