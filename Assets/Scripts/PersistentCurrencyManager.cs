using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCurrencyManager {

	private static PersistentCurrencyManager _instance;

	// TODO: load this from the persistent file.
	private int persistentCurrency = 0;

	public string persistentCurrencyName = "Diamonds";

	public static PersistentCurrencyManager instance {
		get {
			if (_instance == null)
				_instance = new PersistentCurrencyManager ();
			return _instance;
		}
	}

	public int getPersistentCurrency() {
		return persistentCurrency;
	}

	public void setPersistentCurrency(int newPersistentCurrency) {
		persistentCurrency = newPersistentCurrency;
	}

	/* Calculates if any persistent currency dropped from the enemy that was killed.
	 * Also increases the amount of currency the player has.
	 * Returns the amount of currency that was given. */
	public int EnemyKilled(EnemyStats enemyStats) {
		float roll = Random.Range (0.0f, 1.0f);

		int amountGiven = 0;
		if (roll <= enemyStats.persistentCurrencyDropRate) {
			amountGiven = Random.Range (enemyStats.persistentCurrencyMinDropAmount, enemyStats.persistentCurrencyMaxDropAmount + 1);
			persistentCurrency += amountGiven;
		}
		return amountGiven;
	}
}
