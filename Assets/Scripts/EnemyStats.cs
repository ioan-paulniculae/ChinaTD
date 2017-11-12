using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	public float health = 5.0f;
	public int livesCost = 1;
	public bool isBoss = false;

	/* Persistent currency drop rates. */
	[Header("Persistent currency")]
	public float persistentCurrencyDropRate = 0.5f;
	public int persistentCurrencyMinDropAmount = 3;
	public int persistentCurrencyMaxDropAmount = 5;

	/* Session currency drop rates. */
	[Header("Session currency")]
	public float sessionCurrencyDropRate = 1.0f;
	public int sessionCurrencyMinDropAmount = 10;
	public int sessionCurrencyMaxDropAmount = 20;

	private float maxHealth;
	private HealthBarController healthBar;
	private GameplayManager gameplayManager;
	private AudioManager audioManager;

	void Start() {
		maxHealth = health;
		healthBar = gameObject.GetComponentInChildren<HealthBarController> ();
		gameplayManager = FindObjectOfType<GameplayManager> ();
		ApplyDifficultyMultiplier ();
	}
		
	void OnDestroy() {
		if (isBoss) {
			audioManager = FindObjectOfType<AudioManager> ();
			audioManager.BossDead ();
		}
	}

	public void ApplyDifficultyMultiplier() {
		health *= gameplayManager.GetCurrentDifficultyMultiplier ();
	}

	private void UpdateHealthBar() {
		float newScale = health / maxHealth;
		healthBar.setCurrentHealth (newScale);
	}

	public void ReceiveDamage(float damage)
	{
		health -= damage;
		UpdateHealthBar ();
		if (health <= 0)
		{
			gameplayManager.EnemyKilled (this);
			Destroy(gameObject);
		}
	}

}
