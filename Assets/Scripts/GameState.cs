using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState {

	public int persistentCurrency = 0;
	public List<PersistentUpgradeSerializable> persistentUpgrades;
	public UpgradeType activeAura;

	public GameState(int persistentCurrency, List<PersistentUpgradeSerializable> persistentUpgrades, UpgradeType activeAura) {
		this.persistentCurrency = persistentCurrency;
        this.persistentUpgrades = persistentUpgrades;
		this.activeAura = activeAura;
	}

	public GameState() {
		this.persistentCurrency = 0;
		this.persistentUpgrades = new List<PersistentUpgradeSerializable> ();
		this.activeAura = UpgradeType.DRAGON_NO_AURA;
	}
}
