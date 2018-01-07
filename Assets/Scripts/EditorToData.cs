using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorToData : MonoBehaviour 
{

	// float	startDelay;
	// float	duration;
	// float	speed;
	// float	size;
	// // Color	color;
	// float	rate;
	// bool	isBurst;
	// float	burstCount;
	// float	burstCycles;
	// float	burstinterval;
	// ParticleEmissionShape	shape;
	// ParticleEmissionMode	mode;
	// Vector3	velocityOverLifetime;
	// Vector3	forceOverLifetime;
	// float	radius;
	// float	donutRadius;
    // float	arc;
  	// float	angle;
    // float	rotspeed;
    // float	zrot;
    // float	zrotGameObject;
    // float	xscale;
    // float	yscale;
 	// float	zscale;
	// Use his for initialization

	float	oldfirerate = 20f;

	ParticleSystemData psd;
	public GameObject	psh;
	void Start()
	{
		psd = new ParticleSystemData();
	}

	public void	SpeedSlider(float speed)
	{
		psd.speed = speed;
	}

	public void	LifetimeSlider(float lifetime)
	{
		psd.lifetime = lifetime;
	}
	
	public void	SizeSlider(float size)
	{
		psd.size = size;
	}

	public void	BurstSlider(float burst)
	{
		if (burst == 1)
		{
			psd.isBurst = true;
			oldfirerate = psd.rate;
			psd.rate = 0;
		}
		else
		{
			psd.isBurst = false;
			psd.rate = oldfirerate;
			
		}
	}

	public void	rateSlider(float rate)
	{
		oldfirerate = rate;
		psd.rate = rate;
	}

	public void	BurstCountSlider(float burstCount)
	{
		psd.burstCount = (short)burstCount;
	}

	public void	BurstCyclesSlider(float burstCycles)
	{
		psd.burstCycles = (int)burstCycles;
	}

	public void	BurstintervalSlider(float burstinterval)
	{
		psd.burstinterval = burstinterval;
	}

	public void	ShapeSlider(float shapess)
	{
		if (shapess == 0)
 			psd.shape = ParticleEmissionShape.Cone;
		if (shapess == 1)
 			psd.shape = ParticleEmissionShape.Donut;
		if (shapess == 2)
 			psd.shape = ParticleEmissionShape.Edge;
		if (shapess == 3)
 			psd.shape = ParticleEmissionShape.Circle;
	}

	public void	ModeSlider(float modess)
	{
		if (modess == 0)
 			psd.mode = ParticleEmissionMode.Loop;
		if (modess == 1)
 			psd.mode = ParticleEmissionMode.PingPong;
		if (modess == 2)
 			psd.mode = ParticleEmissionMode.BurstSpread;
 	}

	public void	RadiusSlider(float radius)
	{
		psd.radius = radius;
	}

	public void	ArcSlider(float arc)
	{
		psd.arc = arc;
	}

	public void	ArcspeedSlider(float arcspeed)
	{
		psd.rotspeed = arcspeed;
	}

	public void	rotationSlider(float rotation)
	{
		psd.zrot = rotation;
		ParticleSystemScript.setconestartrot = false;
	}

	public void	rotateSlider(float rotation)
	{
		psd.zrotGameObject = rotation;
	}

	public Slider S;	
	public void	ResetSlider()
	{
		psd.zrotGameObject = 0;
		S.value = 0;
	}

	public void	XscaleSlider(float xscale)
	{
		psd.xscale = xscale;
	}

	public void	YscaleSlider(float yscale)
	{
		psd.yscale = yscale;
	}

	public void	ZscaleSlider(float zscale)
	{
		psd.zscale = zscale;
	}
	
	public void	xforce(float xforce)
	{
		psd.xforce = xforce;
	}

	public void	yforce(float yforce)
	{
		psd.yforce = yforce;
	}

	public void	xvel(float xvel)
	{
		psd.xvel = xvel;
	}

	public void	yvel(float yvel)
	{
		psd.yvel = yvel;
	}

	public void	AngleSlider(float angle)
	{
		psd.angle = angle;
	}

	

	public void	DonutRadiusSlider(float donutRadius)
	{
		psd.donutRadius = donutRadius;
	}
	// Update is called once per frame
	void Update () {
		// if (psd.isBurst == true)
		// 	psd.rate = 0;
		ParticleSystemScript.SetPSHromData(psh, psd);
		if (true)
		{
			// Debug.Log(psd.isBurst);
			// Debug.Log(psd.rate);
			// Debug.Log(psd.mode);
			// Debug.Log(psd.shape);
		}
	}
}
