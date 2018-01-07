using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour {

	public Text			timeUi;
	private	Text		timetext;
	public	float		time;
	[Space]
	public	Text		score;
	public	long		pts;
	private	Text		scoretext;

	// [Space]


	// Use this for initialization
	void Start () {
		timetext = timeUi.GetComponent<Text>();
		scoretext = score.GetComponent<Text>();
		Debug.Log(pts);

	}
	
	// Update is called once per frame
	void Update () {
		timetext.text = System.Math.Round(Time.timeSinceLevelLoad, 2).ToString("F2");
		scoretext.text = pts.ToString();
	}

	void FixedUpdate()
	{
		pts = pts - 187;
	}

	public	void AddPts(int i) {
		pts += i;
	}
}
