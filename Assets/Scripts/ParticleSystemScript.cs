using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ParticleSystemScript : MonoBehaviour 
{
	ParticleSystem ps;

	// ParticleSystemDataCircle psdci { get { return psd as ParticleSystemDataCircle; } }
	// ParticleSystemDataCone psdco { get { return psd as ParticleSystemDataCone; } }
	// ParticleSystemDataDonut psddo { get { return psd as ParticleSystemDataDonut; } }
	// ParticleSystemDataEdge psded { get { return psd as ParticleSystemDataEdge; } }

	// Use this for initialization

	ParticleSystemShapeMultiModeValue		SetPSShape(ParticleSystemData psd)
	{
		if (psd.mode == ParticleEmissionMode.Loop)
			return(ParticleSystemShapeMultiModeValue.Loop);
		if (psd.mode == ParticleEmissionMode.PingPong)
			return(ParticleSystemShapeMultiModeValue.PingPong);
		if (psd.mode == ParticleEmissionMode.BurstSpread)
			return(ParticleSystemShapeMultiModeValue.BurstSpread);
		return(ParticleSystemShapeMultiModeValue.Random);
	}

	bool	setconestartrot = false;

	public	void	SetPSFromData(GameObject psHolder, ParticleSystemData psd)
	{
		if (psd == null)
			Debug.Log(null);
		ps = psHolder.GetComponent<ParticleSystem>();
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
			if (setconestartrot == false)
			{
				setconestartrot = true;
				psHolder.transform.Rotate(0, 0, psd.zrot);
			}
			psHolder.transform.Rotate(0, 0, psd.zrotGameObject);
		}
		if (psd.shape == ParticleEmissionShape.Donut)
		{
			sh.shapeType = ParticleSystemShapeType.Donut;
			sh.arcMode = SetPSShape(psd);
			sh.donutRadius = psd.donutRadius;
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(0, 0, psd.zrot);
			sh.scale = new Vector3(psd.xscale, psd.yscale, 0);
			psHolder.transform.Rotate(0, 0, psd.zrotGameObject);
			
		}
		if (psd.shape == ParticleEmissionShape.Edge)
		{
			sh.shapeType = ParticleSystemShapeType.SingleSidedEdge;
			sh.arcMode = SetPSShape(psd);
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(0, 0, psd.zrot);
			psHolder.transform.Rotate(0, 0, psd.zrotGameObject);
		}
		if (psd.shape == ParticleEmissionShape.Circle)
		{
			sh.shapeType = ParticleSystemShapeType.Circle;
			sh.arcMode = SetPSShape(psd);
			sh.radius = psd.radius;
			sh.arc = psd.arc;
			sh.arcSpeed = psd.rotspeed;
			sh.rotation = new Vector3(0, 0, psd.zrot);
			sh.scale = new Vector3(psd.xscale, psd.yscale, 0);
			psHolder.transform.Rotate(0, 0, psd.zrotGameObject);
			
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

	public PlayerPattern GetPSListFromDataList(PatternData pd)
	{
		PlayerPattern playerPattern = new PlayerPattern();
		foreach (ParticleSystemData psd in pd.particlePatterns)
		{
			GameObject psh = new GameObject();
			ParticleSystem tmpps = psh.AddComponent<ParticleSystem>();
			SetPSFromData(psh, psd);
			playerPattern.particleSystems.Add(tmpps);
		}

		playerPattern.cooldown = pd.cooldown;
		playerPattern.duration = pd.duration;
		return playerPattern;
	}

}
