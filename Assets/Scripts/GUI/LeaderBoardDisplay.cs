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

	List< string > leaderboardCodes = new List< string >()
	{
		"5a4f9f3ad6026605287bf158",
		"5a528957d60245173091971e",
		"5a528974d60245173091977c",
		"5a528985d6024517309197af",
	};

	List< GameObject >		leaderboardCells = new List< GameObject >();

	string highScores;

	void OnEnable()
	{
		LoadLeaderboard(0);
	}

	public void LoadLeaderboard(int index)
	{
		StartCoroutine(GetScores(index));
	}
	
	IEnumerator GetScores(int index)
	{
		loadingBar.SetActive(true);

		highScores = "";
		WWW www = new WWW(dreamloWebserviceURL +  leaderboardCodes[index]  + "/pipe");
		yield return www;
		highScores = www.text;

		Debug.Log("highScores: " + highScores);

		string[] rows = highScores.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);

		//destroy all children:
		foreach (var cell in leaderboardCells)
			Destroy(cell);

		for (int i = 0; i < rows.Length; i++)
		{
			string[] values = rows[i].Split(new char[] {'|'}, System.StringSplitOptions.None);

			GameObject cellObject = GameObject.Instantiate(leaderBoardCellPrefab, leaderBoardTable.transform);
			leaderboardCells.Add(cellObject);

			Debug.Log("name: " + values[1]);

			var cell = cellObject.GetComponent< LeaderBoardCell >();
			
			try {
				cell.UpdateProperties(long.Parse(values[1]), 10000000000, values[0]);
			} catch (Exception e) {
				Debug.LogError(e);
				Destroy(cell);
			}
		}
		
		loadingBar.SetActive(false);

	}
	
}
