using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

    public static PersistentUpgradesManager persistentUpgradesManager = PersistentUpgradesManager.instance;
    public UpgradeType upgradeType;

	private PersistentUpgrade upgrade;
	private Button button;
    private Transform upgradeNameObject;
	private Transform costObject;
	private Transform currentLevelObject;
	private Transform currentEffectObject;

	void UpdateTexts() {
		Text upgradeNameText = upgradeNameObject.GetComponent<Text> ();
		upgradeNameText.text = upgrade.name;

		Text costText = costObject.GetComponent<Text> ();
		costText.text = upgrade.GetCurrentCost() + " " + PersistentCurrencyManager.persistentCurrencyName;

		Text currentLevelText = currentLevelObject.GetComponent<Text> ();
		currentLevelText.text = "Level: " + upgrade.info.level;

		Text currentEffectText = currentEffectObject.GetComponent<Text> ();
		currentEffectText.text = "Effect: +" + (upgrade.GetCurrentEffect () * 100) + "% " + upgrade.effectName;
	}

    void Start() {
		upgrade = persistentUpgradesManager.GetUpgrade (upgradeType);
		button = transform.Find ("Button").GetComponent<Button> ();
		button.onClick.AddListener (OnClick);

		upgradeNameObject = transform.Find("UpgradeName");
		costObject = transform.Find("Button").Find ("Cost");
		currentLevelObject = transform.Find ("Button").Find("CurrentLevel");   
		currentEffectObject = transform.Find ("CurrentEffect");

		UpdateTexts ();
    }

	void Update() {
		if (!persistentUpgradesManager.CanAfford (upgrade)) {
			button.interactable = false;
		} else {
			button.interactable = true;
		}
		UpdateTexts ();
	}

	void OnClick() {
		persistentUpgradesManager.PurchaseUpgrade (upgrade);
	}
}
