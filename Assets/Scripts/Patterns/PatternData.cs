using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternData
{
	public List< ParticleSystemData> particlePatterns = new List< ParticleSystemData >();

	public float		cooldown;
	public float		duration;
	public string		name;
}
