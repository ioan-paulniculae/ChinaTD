using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

	private GameplayManager gameplayManager;
	private EnemySpawner enemySpawner;

	// Use this for initialization
	void Start () {
		gameplayManager = FindObjectOfType<GameplayManager> ();
		enemySpawner = FindObjectOfType<EnemySpawner> ();
	}
	
	// Update is called once per frame
	void Update () {

		/* Debug keybinding: show build mode. */
		if (Input.GetKeyDown ("b")) {
			if (gameplayManager.getBuildModeEnabled ()) {
				gameplayManager.exitBuildMode ();
			} else {
				gameplayManager.enterBuildMode ();
			}
		}

		/* Debug keybinding: kill all enemies. */
		if (Input.GetKeyDown ("k")) {
			EnemyStats[] enemies = FindObjectsOfType<EnemyStats> ();
			foreach(EnemyStats enemy in enemies) {
				Destroy (enemy.gameObject);
			}
		}

		/* Debug keybinding: force next wave. */
		if (Input.GetKeyDown ("n")) {
			enemySpawner.ForceNextWave ();
		}
	}
}
