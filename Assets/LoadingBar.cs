using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
	public float blinkTime = .5f;

	void Start()
	{
		StartCoroutine(Blink());
	}

	IEnumerator Blink()
	{
		while (true)
		{
			gameObject.SetActive(true);
			yield return new WaitForSeconds(blinkTime);
			gameObject.SetActive(false);
			yield return new WaitForSeconds(blinkTime);
		}
	}
}
