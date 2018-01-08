using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : MonoBehaviour
{

	public Image[]	spellCooldownImages;
	public Text[]	spellCooldownText;

	public static GameGUIManager	instance;
	
	static bool[]	cooldowns = new bool[5];
	static float[]	cooldownTimes = new float[5];
	static float[]	cooldownValues = new float[5];
	static float[]	cooldownElapsed = new float[5];

	void Awake()
	{
		instance = this;
	}

	public static void ActivateSpellCard(int spellcardIndex, float cooldown, float duration)
	{
		int	i = 0;

		foreach (var spellCooldownImage in instance.spellCooldownImages)
		{
			instance.StartCoroutine(Cooldown(spellCooldownImage, (i == spellcardIndex) ? cooldown : duration, i));
			i++;
		}
	}

	public static bool IsSpellcardInCooldown(int spellcardIndex)
	{
		return cooldowns[spellcardIndex];
	}

	static IEnumerator Cooldown(Image cooldownImage, float duration, int spellcardIndex)
	{
		Debug.Log("cooldown: " + duration);
		if (cooldowns[spellcardIndex])
		{
			// Debug.Log("cooldown update for " + spellcardIndex + ": " + cooldownValues[spellcardIndex] + ", d: " + duration + ", col: " + cooldownElapsed[spellcardIndex]);
			float remaining = cooldownValues[spellcardIndex] * (1 - (cooldownElapsed[spellcardIndex]));
			// Debug.Log("remaining: " + remaining);
			cooldownValues[spellcardIndex] = Mathf.Max(remaining, duration);
			cooldownTimes[spellcardIndex] = Time.time;
			yield break ;
		}

		cooldownValues[spellcardIndex] = duration;
		cooldownTimes[spellcardIndex] = Time.time;
		
		cooldowns[spellcardIndex] = true;

		do {
			cooldownElapsed[spellcardIndex] = 1 - ((Time.time - cooldownTimes[spellcardIndex]) / cooldownValues[spellcardIndex]);
			// instance.spellCooldownText[spellcardIndex].text = cooldownElapsed[spellcardIndex].ToString("F2");
			cooldownImage.fillAmount = cooldownElapsed[spellcardIndex];
			yield return new WaitForEndOfFrame();
		} while (Time.time - cooldownTimes[spellcardIndex] < cooldownValues[spellcardIndex]);
		cooldownImage.fillAmount = 0;
		cooldowns[spellcardIndex] = false;
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}
}
