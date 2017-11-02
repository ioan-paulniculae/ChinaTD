using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

	public int playerLives = 30;
	public UserInterfaceController uiCanvas;

	private BuildableTile[] buildableTiles;
	private bool buildModeEnabled = false;

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

	// Use this for initialization
	void Start () {
		// Get references to buildable tiles.
		buildableTiles = FindObjectsOfType (typeof(BuildableTile)) as BuildableTile[];

		// Update UI elements.
		uiCanvas.UpdateLivesText(playerLives);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerLives <= 0) {
			SceneManager.LoadScene ("Game Over", LoadSceneMode.Single);
		}
	}
}
