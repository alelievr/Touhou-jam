using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeaderBoardDisplay : MonoBehaviour
{

	public GameObject		leaderBoardCellPrefab;
	public GameObject		loadingBar;
	public GameObject		leaderBoardTable;

	string dreamloWebserviceURL = "http://dreamlo.com/lb/";
	public string publicCode = "5a4f9f3ad6026605287bf158";

	string highScores;

	void OnEnable()
	{
		loadingBar.SetActive(true);
		StartCoroutine(GetScores());
	}
	
	IEnumerator GetScores()
	{
		highScores = "";
		WWW www = new WWW(dreamloWebserviceURL +  publicCode  + "/pipe");
		yield return www;
		highScores = www.text;

		Debug.Log("highScores: " + highScores);

		string[] rows = highScores.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < rows.Length; i++)
		{
			string[] values = rows[i].Split(new char[] {'|'}, System.StringSplitOptions.None);

			GameObject cellObject = GameObject.Instantiate(leaderBoardCellPrefab, leaderBoardTable.transform);

			var cell = cellObject.GetComponent< LeaderBoardCell >();
			
			try {
				cell.UpdateProperties(int.Parse(values[1]), 10000, values[0]);
			} catch (Exception e){
				Debug.LogError(e);
				Destroy(cell);
			}
		}
		
		loadingBar.SetActive(false);

	}
	
}
