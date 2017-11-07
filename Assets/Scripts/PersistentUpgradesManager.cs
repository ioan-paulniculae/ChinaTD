using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentUpgradesManager {

    private static PersistentUpgradesManager _instance;
	private PersistentCurrencyManager persistentCurrencyManager = PersistentCurrencyManager.instance;

	private List<PersistentUpgradeSerializable> purchasedPersistentUpgrades = new List<PersistentUpgradeSerializable>();
	private List<PersistentUpgrade> persistentUpgradesList = PersistentUpgradeList.upgrades;

    public static PersistentUpgradesManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new PersistentUpgradesManager();
            return _instance;
        }
    }

    public List<PersistentUpgradeSerializable> GetPersistentUpgrades() {
        return purchasedPersistentUpgrades;
    }

    public void SetPersistentUpgrades(List<PersistentUpgradeSerializable> newPersistentUpgrades) {
		if (newPersistentUpgrades != null) {
			purchasedPersistentUpgrades = newPersistentUpgrades;

			// Synchronize loaded ugrades with the main list.
			foreach (PersistentUpgradeSerializable loadedUpgrade in purchasedPersistentUpgrades) {
				PersistentUpgrade upgrade = GetUpgrade (loadedUpgrade.type);
				upgrade.info.level = loadedUpgrade.level;
			}
		} else {
			purchasedPersistentUpgrades = new List<PersistentUpgradeSerializable> ();

			// Reset the main list of upgrades.
			foreach (PersistentUpgrade upgrade in persistentUpgradesList) {
				upgrade.info.level = 0;
			}
		}
    }

    public PersistentUpgrade GetUpgrade(UpgradeType upgradeType) {
        foreach (PersistentUpgrade upgrade in persistentUpgradesList) {
            if (upgrade.info.type == upgradeType) {
                return upgrade;
            }
        }
        return null;
    }

	public bool CanAfford(PersistentUpgrade upgrade) {
		return (persistentCurrencyManager.GetPersistentCurrency () > upgrade.GetCurrentCost ());
	}

	public void UpdatePurchasedPersistentUpgrades(UpgradeType upgradeType, int level) {
		// If the upgrade is in the list, just update the level.
		foreach (PersistentUpgradeSerializable upgrade in purchasedPersistentUpgrades) {
			if (upgrade.type == upgradeType) {
				upgrade.level = level;
				return;
			}
		}

		// If it's not already in the list, add it now.
		purchasedPersistentUpgrades.Add(new PersistentUpgradeSerializable(upgradeType, level));
	}

	public void ResetPersistentUpgrades() {
		SetPersistentUpgrades (null);
	}

	public void PurchaseUpgrade(PersistentUpgrade upgrade) {
		// Update player currency.
		persistentCurrencyManager.SubstractPersistentCurrency(upgrade.GetCurrentCost());

		// Update upgrade level.
		upgrade.info.level += 1;

		// Save new state.
		UpdatePurchasedPersistentUpgrades(upgrade.info.type, upgrade.info.level);
		SaveLoad.Save ();
	}
}
