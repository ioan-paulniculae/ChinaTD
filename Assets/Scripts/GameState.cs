using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState {

	public int persistentCurrency = 0;
	public List<PersistentUpgradeSerializable> persistentUpgrades;

	public GameState(int persistentCurrency, List<PersistentUpgradeSerializable> persistentUpgrades) {
		this.persistentCurrency = persistentCurrency;
        this.persistentUpgrades = persistentUpgrades;
	}

	public GameState() {
		this.persistentCurrency = 0;
		this.persistentUpgrades = new List<PersistentUpgradeSerializable> ();
	}
}
