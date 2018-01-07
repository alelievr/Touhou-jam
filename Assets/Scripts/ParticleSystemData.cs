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
public abstract class ParticleSystemData
{
	public float	startDelay;
 
	public float	duration;
	public float	speed;
	public float	size;
	public Color	color;

	public bool		isBurst;
	public float	rate;
 
	public short	burstCount;
	public int		burstCycles;
	public float	burstinterval;

	public ParticleEmissionShape	shape;
	public ParticleEmissionMode		mode;


	public Vector3	velocityOverLifetime;
	public Vector3	forceOverLifetime;
}
