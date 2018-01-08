using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
	static string saveIndexKey = "saveIndex";
	static string playerPseudoKey = "playerPseudo";
	static string levelKey = "levelkey";

	public static int GetCurrentSaveIndex()
	{
		return PlayerPrefs.GetInt(saveIndexKey, 0);
	}

	public static void SetCurrenSaveIndex(int saveIndex)
	{
		PlayerPrefs.SetInt(saveIndexKey, saveIndex);
		PlayerPrefs.Save();
	}

	public static string GetPlayerPseudo()
	{
		return PlayerPrefs.GetString(playerPseudoKey, null);
	}

	public static void SetPlayerPseudo(string pseudo)
	{
		PlayerPrefs.SetString(playerPseudoKey, pseudo);
		PlayerPrefs.Save();
	}

	public static void SetGameLevel(int level)
	{
		PlayerPrefs.SetInt(levelKey, level);
		PlayerPrefs.Save();
	}

	public static int GetGameLevel()
	{
		return PlayerPrefs.GetInt(levelKey, 0);
	}
}
