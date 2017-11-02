using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	public float health = 5.0f;
	public int livesCost = 1;
	public float persistentCurrencyDropRate = 0.5f;
	public int persistentCurrencyMinDropAmount = 3;
	public int persistentCurrencyMaxDropAmount = 5;

	private float maxHealth;
	private HealthBarController healthBar;
	private GameplayManager gameplayManager;

	void Start() {
		maxHealth = health;
		healthBar = gameObject.GetComponentInChildren<HealthBarController> ();
		gameplayManager = FindObjectOfType<GameplayManager> ();
	}
		
	void OnDestroy() {
		
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
