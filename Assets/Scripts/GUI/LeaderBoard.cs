using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
	public InputField	username;

	GM			gameManager;

	List< string > privateCodes = new List< string >()
	{
		"JgsR0z-CtE28zoNS_8LBMAkGE2emxeGUOsrxExjTgbSQ",
		"SQqS_-vyo0S3Xr26I0HqJQseD_EXLSvkaASO0ipj6DAQ",
		"a5WKkg7zN0GkNe5bmhvMhAQpL_6qkYlU2mFRN1GUQysA",
		"KHyDD7mK_k62vsxomFwUVQzhmp7XjzPkmFeYHm1_pnDw",
	};

	void Start()
	{
		gameManager = FindObjectOfType< GM >();
	}
	
	string dreamloWebserviceURL = "http://dreamlo.com/lb/";

	public void AddEntry()
	{
		AddEntryLong(username.text, Global.GetGameLevel(), gameManager.pts);

		SceneTransition.instance.LoadScene("Levels");
	}

	public void AddEntryLong(string pseudo, int index, long points)
	{
		StartCoroutine(AddScoreWithPipe(index, pseudo, points));
	}
	
	IEnumerator AddScoreWithPipe(int level, string playerName, long totalScore)
	{
		playerName = Clean(playerName);
		
		WWW www = new WWW(dreamloWebserviceURL + privateCodes[level] + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString());
		yield return www;
		// highScores = www.text;
	}

	string Clean(string s)
	{
		s = s.Replace("/", "");
		s = s.Replace("|", "");
		return s;
		
	}
}
