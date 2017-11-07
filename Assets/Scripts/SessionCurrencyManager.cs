using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionCurrencyManager : MonoBehaviour {

	public static string sessionCurrencyName = "Gold";

	private int sessionCurrency = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetSessionCurrency() {
		return sessionCurrency;
	}

	public void SetSessionCurrency(int currency) {
		sessionCurrency = currency;
	}

	public bool CanAfford(int currency) {
		return (sessionCurrency >= currency);
	}

	public void SubstractSessionCurrency(int currency) {
		sessionCurrency -= currency;
	}

	/* Calculates if any session currency dropped from the enemy that was killed.
	 * Also increases the amount of currency the player has.
	 * Returns the amount of currency that was given. */
	public int EnemyKilled(EnemyStats enemyStats) {
		float roll = Random.Range (0.0f, 1.0f);

		int amountGiven = 0;
		if (roll <= enemyStats.sessionCurrencyDropRate) {
			amountGiven = Random.Range (enemyStats.sessionCurrencyMinDropAmount, enemyStats.sessionCurrencyMaxDropAmount + 1);
			sessionCurrency += amountGiven;
		}
		return amountGiven;
	}
}
