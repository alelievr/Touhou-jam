using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleSystemDataCone : ParticleSystemData
{
    // Rot 90 x
    // scale 0 y
    public float	radius = 1;
    public float	angle = 20;

    public float	arc = 360;

    public float	rotspeed = 1;
    public float	zrotGameObject = 0;

    public float	xscale = 0;
    public float	zscale = 0;
}
