using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
	static string saveIndexKey = "saveIndex";

	public static int GetCurrentSaveIndex()
	{
		return PlayerPrefs.GetInt(saveIndexKey, 0);
		PlayerPrefs.Save();
	}

	public static void SetCurrenSaveIndex(int saveIndex)
	{
		PlayerPrefs.SetInt(saveIndexKey, saveIndex);
		PlayerPrefs.Save();
	}
}
