using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializableVector3
{
	public float	x;
	public float	y;
	public float	z;

	public SerializableVector3(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public SerializableVector3(Vector3 v)
	{
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public static implicit operator Vector3(SerializableVector3 serializableVector3)
	{
		return new Vector3(serializableVector3.x, serializableVector3.y, serializableVector3.z);
	}

	public static explicit operator SerializableVector3(Vector3 v)
	{
		return new SerializableVector3(v);
	}
}
