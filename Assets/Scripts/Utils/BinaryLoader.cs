using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinaryLoader
{
	public static string	patternFolder = Application.persistentDataPath + "/Patterns/";

	public static string	patternExtension = ".tsp"; //Touhou spellcard pattern

	static BinaryFormatter	formatter;

	[System.NonSerialized]
	static bool				pathBuilded = false;

	static BinaryLoader()
	{
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		formatter = new BinaryFormatter();
	}

    static T Deserialize< T >(string filePath)
    {
		T	ret;
        FileStream fs = null;

        if (!File.Exists(filePath))
		{
			Debug.LogError("requested filePath " + filePath + " does not exist");
            return default(T);
		}

        try
        {
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            ret = (T)formatter.Deserialize(fs);
            fs.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e + "\n\n");

            if (fs != null)
                fs.Close();
            ret = default(T);
        }

        return ret;
    }

    public static string Serialize< T >(T instance, string filePath, string folder = ".")
    {
		if (String.IsNullOrEmpty(filePath))
		{
			Debug.LogError("Attempt to serialize an empty-named object");
			return null;
		}

        FileStream fs = null;

		string dir = Path.GetDirectoryName(filePath);

		if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        try
        {
            fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(fs, instance);
			Debug.Log("File serialized at: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError(e + "\n\n");
			filePath = null;
        }

        if (fs != null)
            fs.Close();
		
		return filePath;
    }

	static string GetPatternDataPath(int patternIndex, int saveIndex)
	{
		return patternFolder + "s" + saveIndex + "-p" + patternIndex + patternExtension;
	}
	
	static string GetDefaultPatternDataPath(int patternIndex, int saveIndex)
	{
		return Application.streamingAssetsPath + "/Patterns/" + "s" + saveIndex + "-p" + patternIndex + patternExtension;
	}

	public static PatternData LoadPatternData(int patternIndex, int saveIndex)
	{
		PatternData pattern = Deserialize< PatternData >(GetPatternDataPath(patternIndex, saveIndex));

		if (pattern != null)
			return pattern;
		
		if (saveIndex != 0)
			return null;
		
		return Deserialize< PatternData >(GetDefaultPatternDataPath(patternIndex, saveIndex));
	}

	public static string SavePatternData(PatternData pattern, int patternIndex, int saveIndex)
	{
		string path = Serialize< PatternData >(pattern, GetPatternDataPath(patternIndex, saveIndex));
		if (path != null)
			return Path.GetFileNameWithoutExtension(path);
		return null;
	}

}
