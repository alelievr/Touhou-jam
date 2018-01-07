using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour
{
	public float			audioLevelInput {get; set;}
	public float			audioLevelTrigger = .8f;
	public int				emitCountWhenTriggered = 4;

	new ParticleSystem		particleSystem;
	// ParticleSystem.EmissionModule	emission;

	void Start () {
		particleSystem = GetComponent< ParticleSystem >();
		// emission = particleSystem.emission;
	}
	
	void Update () {
		// Debug.Log("audioLevel: " + audioLevelInput);
		if (audioLevelInput > audioLevelTrigger)
		{
			StartCoroutine(SyncToSound());
		}
	}

	IEnumerator SyncToSound()
	{
		yield return new WaitForSeconds(0.00f);
		particleSystem.Emit(emitCountWhenTriggered);
	}
}
