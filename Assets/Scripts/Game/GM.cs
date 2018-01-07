using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

	public Text			timeUi;
	private	Text		timetext;
	public	float		time;
	[Space]
	public	Text		score;
	public	long		pts;
	private	Text		scoretext;

	[Space]
	public	Text		Highscore;
	public	long		Highpts;
	private	Text		Highscoretext;

	// Use this for initialization
	void Start () {
		timetext = timeUi.GetComponent<Text>();
		scoretext = score.GetComponent<Text>();
		Highscoretext = Highscore.GetComponent<Text>();
		// Debug.Log(PlayerPrefs.GetString("HighScore" + SceneManager.GetActiveScene().name));
		if (PlayerPrefs.GetString("HighScore" + SceneManager.GetActiveScene().name) == "")
		{
			Highpts = 0;
			PlayerPrefs.SetString("HighScore" + SceneManager.GetActiveScene().name, "0");
		}
		else 
			Highpts = long.Parse(PlayerPrefs.GetString("HighScore" + SceneManager.GetActiveScene().name));
		Highscoretext.text = Highpts.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		timetext.text = System.Math.Round(Time.timeSinceLevelLoad, 2).ToString("F2");
		scoretext.text = pts.ToString();
	}

	void FixedUpdate()
	{
		pts = pts - 37987;
	}

	public	void AddPts(int i) {
		pts += i;
	}
	public	void Win(bool victory) {
		if (long.Parse(PlayerPrefs.GetString("HighScore" + SceneManager.GetActiveScene().name)) < pts)
			PlayerPrefs.SetString("HighScore" + SceneManager.GetActiveScene().name, pts.ToString());
	}
}
