using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardCell : MonoBehaviour
{
	public Image	fillImage;
	public Text		pointText;
	public Text		nameText;

	public void UpdateProperties(long points, long maxPoints, string name)
	{
		fillImage.fillAmount = (float)points / (float)maxPoints;

		pointText.text = points.ToString();

		nameText.text = name;
	}
}
