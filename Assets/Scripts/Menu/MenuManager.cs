using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private PersistentCurrencyManager persistentCurrencyManager;

	// Use this for initialization
	void Start () {
		// Get a persistent currency manager instance.
		persistentCurrencyManager = PersistentCurrencyManager.instance;

		// Load the saved game state.
		SaveLoad.Load ();

		// Set the persistent currency text.
		SetPersistentCurrencyText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SetPersistentCurrencyText() {
		GameObject persistentCurrencyTextObject = GameObject.Find ("PersistentCurrencyText");
		Text t = persistentCurrencyTextObject.transform.GetComponent<Text> ();
		t.text = persistentCurrencyManager.persistentCurrencyName + ": " + persistentCurrencyManager.getPersistentCurrency ();
	}

	public void LoadGameScene() {
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
	}

	public void LoadMenuScene() {
		SceneManager.LoadScene ("Main Menu", LoadSceneMode.Single);
	}
}
