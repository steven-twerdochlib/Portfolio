using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class SaveLoadScript : MonoBehaviour {

    public static SaveLoadScript SaveLoadFeature;
//	public List<int> MapArrayList;
//	public List<int> LastEnemyDirections;
//	public int LastPlayerDirection;
//	public int NumOfEnemies;
	public int Level;
	public int NumOfTurnsLeftForSuperPower;
//	public bool PowerReady;
//	public bool SceneLoaded;

	void Awake () 
	{
		if(SaveLoadFeature == null)
		{
			DontDestroyOnLoad (gameObject);
			SaveLoadFeature = this;
		}
		else if (SaveLoadFeature != this)
		{
			Destroy (gameObject);
		}
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Level: " + Level);
        GUI.Label(new Rect(10, 40, 150, 30), "Superpower ready in: " + NumOfTurnsLeftForSuperPower);
    }

	public void Save() 
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "PlayerData.dat");
		PlayerData Data = new PlayerData();
//		Data.MapArrayList = MapArrayList;
//		Data.LastEnemyDirections = LastEnemyDirections;
//		Data.NumOfEnemies = NumOfEnemies;
		Data.Level = Level;
		Data.NumOfTurnsLeftForSuperPower = NumOfTurnsLeftForSuperPower;
//		Data.PowerReady = PowerReady;

		bf.Serialize(file, Data);
		file.Close();
	}

	public void Load() 
	{
		if(File.Exists(Application.persistentDataPath + "PlayerData.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "PlayerData.dat", FileMode.Open);
			PlayerData Data = (PlayerData)bf.Deserialize (file);
			file.Close();

//			MapArrayList = Data.MapArrayList;
//			LastEnemyDirections = Data.LastEnemyDirections;
//			NumOfEnemies = Data.NumOfEnemies;
			Level = Data.Level;
			NumOfTurnsLeftForSuperPower = Data.NumOfTurnsLeftForSuperPower;
//			PowerReady = Data.PowerReady;
//			SceneLoaded = true;
		}
	}
}

[Serializable]
class PlayerData
{
//	public List<int> MapArrayList;
//	public List<int> LastEnemyDirections;
//	public int LastPlayerDirection;
//	public int NumOfEnemies;
	public int Level;
	public int NumOfTurnsLeftForSuperPower;
//	public bool PowerReady;
//	public static bool SceneLoaded;
}
