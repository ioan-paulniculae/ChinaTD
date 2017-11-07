using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

	public int playerLives = 30;
	public int startingSessionCurrency = 200;
	public UserInterfaceController uiCanvas;
	public SessionCurrencyManager sessionCurrencyManager;

	private BuildableTile[] buildableTiles;
	private bool buildModeEnabled = false;
	private PersistentCurrencyManager persistentCurrencyManager;

	/* Keeps track of currently active towers. */
	private Dictionary<TowerType, int> towersBuilt = new Dictionary<TowerType, int>();

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

		// Notify the session currency manager of an enemy kill.
		int sessionCurrencyGiven = sessionCurrencyManager.EnemyKilled(enemyStats);
	}

	public void TowerBuilt(TowerBehaviour tower) {
		// Update session currency.
		sessionCurrencyManager.SubstractSessionCurrency(GetSessionCurrencyCostForTower(tower));

		int oldValue;
		towersBuilt.TryGetValue (tower.type, out oldValue);
		towersBuilt [tower.type] = oldValue + 1;
	}

	public int GetSessionCurrencyCostForTower(TowerBehaviour tower) {
		// Based on the tower's base cost and the number of towers, returns the currency cost.
		int towersCount;
		towersBuilt.TryGetValue (tower.type, out towersCount);

		// TODO: maybe work on this formula?
		return tower.sessionCurrencyCost + (int)(towersCount * Mathf.Sqrt (towersCount) * tower.sessionCurrencyAdditionalCostPerTower);
	}

	public bool CanAffordTower(TowerBehaviour tower) {
		return sessionCurrencyManager.CanAfford (GetSessionCurrencyCostForTower (tower));
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

		// Get a reference to the currency managers.
		persistentCurrencyManager = PersistentCurrencyManager.instance;

		// Initialize session currency.
		sessionCurrencyManager.SetSessionCurrency(startingSessionCurrency);

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

		// Update session currency text.
		uiCanvas.SetSessionCurrencyText(SessionCurrencyManager.sessionCurrencyName,
			sessionCurrencyManager.GetSessionCurrency());
	}
}
