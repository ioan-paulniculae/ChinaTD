using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistentUpgradeSerializable {

	// The only information that actually needs to be saved.
	public UpgradeType type;
	public int level;

	public PersistentUpgradeSerializable(UpgradeType t, int level) {
		this.type = t;
		this.level = level;
	}
}
