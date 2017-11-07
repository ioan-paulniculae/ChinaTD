using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private PersistentCurrencyManager persistentCurrencyManager;
	private PersistentUpgradesManager persistentUpgradesManager;

	// Use this for initialization
	void Start () {
		// Get instances for the currency and upgrades managers.
		persistentCurrencyManager = PersistentCurrencyManager.instance;
		persistentUpgradesManager = PersistentUpgradesManager.instance;

		// Load the saved game state.
		SaveLoad.Load ();

		// Set the persistent currency text.
		SetPersistentCurrencyText ();
	}
	
	// Update is called once per frame
	void Update () {
		SetPersistentCurrencyText ();
	}

	private void SetPersistentCurrencyText() {
		GameObject persistentCurrencyTextObject = GameObject.Find ("PersistentCurrencyText");
		Text t = persistentCurrencyTextObject.transform.GetComponent<Text> ();
		t.text = PersistentCurrencyManager.persistentCurrencyName + ": " + persistentCurrencyManager.GetPersistentCurrency ();
	}

	public void CheatButton() {
		persistentCurrencyManager.AddPersistentCurrency (1000);
		SaveLoad.Save ();
	}

	public void LoadGameScene() {
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
	}

	public void LoadMenuScene() {
		SceneManager.LoadScene ("Main Menu", LoadSceneMode.Single);
	}

    public void LoadUpgradesScene() {
        SceneManager.LoadScene("PersistentUpgrades", LoadSceneMode.Single);
    }

	public void DeleteSave() {
		SaveLoad.DeleteSave ();
		persistentCurrencyManager.ResetPersistentCurrency ();
		persistentUpgradesManager.ResetPersistentUpgrades ();
	}
}
