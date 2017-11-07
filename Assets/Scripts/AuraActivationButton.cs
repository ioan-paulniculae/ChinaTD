using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraActivationButton : MonoBehaviour {

	public UpgradeType auraType;

	PersistentUpgradesManager persistentUpgradesManager = PersistentUpgradesManager.instance;
	Transform textTransform;
	Button button;

	void RedrawButtonState() {
		if (persistentUpgradesManager.GetActiveAura () == auraType) {
			button.interactable = false;
			textTransform.GetComponent<Text> ().text = "Active";
		} else if (persistentUpgradesManager.GetUpgrade (auraType).info.level == 0) {
			button.interactable = false;
			textTransform.GetComponent<Text> ().text = "Not purchased";
		} else {
			button.interactable = true;
			textTransform.GetComponent<Text> ().text = "Activate";
		}
	}

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (OnClick);
		textTransform = transform.Find ("Text");
	}
	
	// Update is called once per frame
	void Update () {
		RedrawButtonState ();
	}

	void OnClick() {
		persistentUpgradesManager.SetActiveAura (auraType);
	}
}
