using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

	public int playerLives = 30;
	public UserInterfaceController uiCanvas;

	private BuildableTile[] buildableTiles;
	private bool buildModeEnabled = false;
	private PersistentCurrencyManager persistentCurrencyManager;

	public bool getBuildModeEnabled() {
		return buildModeEnabled;
	}

	public void enterBuildMode() {
		foreach (BuildableTile tile in buildableTiles) {
			tile.displayBuildMode ();
			buildModeEnabled = true;
		}
	}

	public void exitBuildMode() {
		foreach (BuildableTile tile in buildableTiles) {
			tile.hideBuildMode ();
			buildModeEnabled = false;
		}
	}

	public void substractLives(int livesToSubstract) {
		playerLives -= livesToSubstract;
		uiCanvas.UpdateLivesText (playerLives);
	}

	public void EnemyKilled(EnemyStats enemyStats) {
		// Notify the persistent currency manager of an enemy kill.
		int persistentCurrencyGiven = persistentCurrencyManager.EnemyKilled(enemyStats);
	}

	private void GameOver() {
		// Save the game state (persistent currency).
		SaveLoad.Save();
	}

	// Use this for initialization
	void Start () {
		// Get references to buildable tiles.
		buildableTiles = FindObjectsOfType (typeof(BuildableTile)) as BuildableTile[];

		// Update UI elements.
		uiCanvas.UpdateLivesText(playerLives);

		// Get a reference to the persistent currency manager singleton.
		persistentCurrencyManager = PersistentCurrencyManager.instance;

		// Load the saved game state (useful for running the game directly through the main scene).
		SaveLoad.Load();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerLives <= 0) {
			GameOver ();
			SceneManager.LoadScene ("Game Over", LoadSceneMode.Single);
		}

		// Update persistent currency text.
		uiCanvas.SetPersistentCurrencyText(PersistentCurrencyManager.persistentCurrencyName,
			persistentCurrencyManager.GetPersistentCurrency());
	}
}
