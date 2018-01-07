using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPattern
{
	public List< ParticleSystem >	particleSystems = new List< ParticleSystem >();

	public float					cooldown;
	public float					duration;
	public string					name;
}
