using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

	private GameplayManager gameplayManager;

	// Use this for initialization
	void Start () {
		gameplayManager = FindObjectOfType<GameplayManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("b")) {
			if (gameplayManager.getBuildModeEnabled ()) {
				gameplayManager.exitBuildMode ();
			} else {
				gameplayManager.enterBuildMode ();
			}
		}
	}
}
