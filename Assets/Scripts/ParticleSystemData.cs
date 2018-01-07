using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleEmissionShape
{
	Cone,
	Donut,
	Edge,
	Circle,
}

public enum ParticleEmissionMode
{
	Loop,
	PingPong,
	BurstSpread,
}

[System.Serializable]
public class ParticleSystemData
{
	public float	startDelay = 1;
 
	public float	duration = 1;
	public float	lifetime = 10;
	public float	speed = 1;
	public float	size = 0.1f;
	public Color	color;

	public bool		isBurst = false;
	public float	rate = 20;
 
	public short	burstCount = 0;
	public int		burstCycles = 0;
	public float	burstinterval = 0;

	public ParticleEmissionShape	shape = ParticleEmissionShape.Cone;
	public ParticleEmissionMode		mode = ParticleEmissionMode.Loop;

	public Vector3	velocityOverLifetime = Vector3.zero;
	public Vector3	forceOverLifetime = Vector3.zero;

	public float	radius = 1;
    public float	arc = 360;
    public float	rotspeed = 1;
    public float	zrot = 0;	
	public float	donutRadius = 0;
	public float	xscale = 1;
	public float	yscale = 1;
	public float	zscale = 1;
	public float	angle = 20;
	public float	zrotGameObject = 0;
}
