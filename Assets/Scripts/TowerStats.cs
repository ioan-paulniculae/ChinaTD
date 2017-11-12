using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerStats : MonoBehaviour {

	public float health = 5.0f;
	public int livesCost = 1;

	private float maxHealth;
	private HealthBarController healthBar;
	private GameplayManager gameplayManager;
	private AudioManager audioManager;

	void Start() {
		maxHealth = health;
		healthBar = gameObject.GetComponentInChildren<HealthBarController> ();
		gameplayManager = FindObjectOfType<GameplayManager> ();

		audioManager = FindObjectOfType<AudioManager> ();
		audioManager.TowerPlaced ();
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
			gameplayManager.TowerDestroyed (GetComponent<TowerBehaviour>());
            Tilemap towerMap = GameObject.Find("TowerMap").GetComponent<Tilemap>();
            towerMap.SetTile(towerMap.WorldToCell(gameObject.transform.position),null);
            Destroy(gameObject);
		}
	}

}
