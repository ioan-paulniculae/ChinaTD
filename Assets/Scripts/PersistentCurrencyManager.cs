using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCurrencyManager {

	private static PersistentCurrencyManager _instance;

	private int persistentCurrency = 0;

	public static string persistentCurrencyName = "Diamonds";

	public static PersistentCurrencyManager instance {
		get {
			if (_instance == null)
				_instance = new PersistentCurrencyManager ();
			return _instance;
		}
	}

	public int GetPersistentCurrency() {
		return persistentCurrency;
	}

	public void SetPersistentCurrency(int newPersistentCurrency) {
		persistentCurrency = newPersistentCurrency;
	}

	public void SubstractPersistentCurrency(int toSubstract) {
		persistentCurrency -= toSubstract;
	}

	public void AddPersistentCurrency(int toAdd) {
		persistentCurrency += toAdd;
	}

	public void ResetPersistentCurrency() {
		persistentCurrency = 0;
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
