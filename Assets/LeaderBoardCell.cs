using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardCell : MonoBehaviour
{
	public Image	fillImage;
	public Text		pointText;
	public Text		nameText;

	public void UpdateProperties(int points, int maxPoints, string name)
	{
		Debug.Log("updated !");
		fillImage.fillAmount = (float)points / (float)maxPoints;

		pointText.text = points + "/" + maxPoints;

		nameText.text = name;
	}
}
