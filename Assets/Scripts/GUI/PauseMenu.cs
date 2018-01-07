using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;

	bool		paused = false;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
			TogglePause();
	}

	public void TogglePause()
	{
		if (paused)
			UnPause();
		else
			Pause();
	}

	public void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
		paused = true;
	}

	public void UnPause()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}
}
