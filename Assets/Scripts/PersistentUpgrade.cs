using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentUpgrade {

    public PersistentUpgradeSerializable info; // gives us the upgrade type and the level.

	public string name;
    public string description;
    public Image icon;
    public int baseCost;                // i.e. 100 diamonds
    public int additionalCostPerLevel; // i.e. 10 more diamonds per level added to the base cost (will probably have an exponential there)
    public float baseEffect;            // i.e. 0.05 means a 5% increase
    public float effectIncrement;       // i.e. 0.01 means an extra 1% increase per level
    public string effectName;          // used to display dynamic information about the upgrade (i.e. "+5% damage")

    public PersistentUpgrade(PersistentUpgradeSerializable info, string name, string description, Image icon, int baseCost, int additionalCostPerLevel, float baseEffect, float effectIncrement, string effectName) {
        this.info = info;
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.baseCost = baseCost;
        this.additionalCostPerLevel = additionalCostPerLevel;
        this.baseEffect = baseEffect;
        this.effectIncrement = effectIncrement;
        this.effectName = effectName;
    }

	public float GetCurrentEffect() {
		return baseEffect + effectIncrement * info.level;
	}

	public int GetCurrentCost() {
		// TODO: work on this formula?
		return baseCost + (int)(additionalCostPerLevel * (info.level * Mathf.Sqrt(info.level)));
	}
}
