using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	public float health = 5.0f;
	public int livesCost = 1;

	private float maxHealth;
	private HealthBarController healthBar;

	void Start() {
		maxHealth = health;
		healthBar = gameObject.GetComponentInChildren<HealthBarController> ();
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
			Destroy(gameObject);
		}
	}

}
