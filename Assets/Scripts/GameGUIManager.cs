using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : MonoBehaviour
{

	public Image[]	spellCooldownImages;

	public static GameGUIManager	instance;

	void Awake()
	{
		instance = this;
	}

	public static void ActivateSpellCard(int spellcardIndex, float cooldown, float duration)
	{
		int	i = 0;

		foreach (var spellCooldownImage in instance.spellCooldownImages)
		{
			spellCooldownImage.fillAmount = 1;
			instance.StartCoroutine(Cooldown(spellCooldownImage, (i == spellcardIndex) ? cooldown : duration));
			i++;
		}
	}

	public static bool IsSpellcardInCooldown(int spellcardIndex)
	{
		return instance.spellCooldownImages[spellcardIndex].fillAmount > Mathf.Epsilon;
	}

	static IEnumerator Cooldown(Image cooldownImage, float duration)
	{
		float time = Time.time;

		do {
			cooldownImage.fillAmount = 1 - ((Time.time - time) / duration);
			yield return new WaitForEndOfFrame();
		} while (Time.time - time < duration);
		cooldownImage.fillAmount = 0;
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}
}
