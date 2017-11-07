using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	public static PersistentCurrencyManager persistentCurrencyManager = PersistentCurrencyManager.instance;
	public static PersistentUpgradesManager persistentUpgradesManager = PersistentUpgradesManager.instance;
    public static string saveName = "/savedGame.gd";

	public static void Save() {
		GameState newState = new GameState (persistentCurrencyManager.GetPersistentCurrency (), persistentUpgradesManager.GetPersistentUpgrades());
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + saveName);
		bf.Serialize (file, newState);
		file.Close ();
	}

	public static void Load() {
		if (File.Exists(Application.persistentDataPath + saveName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + saveName, FileMode.Open);
			GameState gameState = (GameState)bf.Deserialize(file);
			persistentCurrencyManager.SetPersistentCurrency(gameState.persistentCurrency);
            persistentUpgradesManager.SetPersistentUpgrades(gameState.persistentUpgrades);
			file.Close();
		}
	}

	public static void DeleteSave() {
		if (File.Exists (Application.persistentDataPath + saveName)) {
			File.Delete (Application.persistentDataPath + saveName);
		}
	}
}
