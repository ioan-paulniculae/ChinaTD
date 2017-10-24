using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour {

	public void UpdateLivesText(int lives) {
		Transform infoPanelTransform = transform.Find ("infoPanel");
		Transform livesTextTransform = infoPanelTransform.Find ("livesText");
		Text t = livesTextTransform.GetComponent<Text> ();
		t.text = "Lives: " + lives;
	}

	public void UpdateNextWaveTimerText(float timeLeft) {
		Transform infoPanelTransform = transform.Find ("infoPanel");
		Transform nextWaveTextTransform = infoPanelTransform.Find ("nextWaveTimerText");
		Text t = nextWaveTextTransform.GetComponent<Text> ();
		t.text = "Next wave in: " + timeLeft + "s.";
	}

	public void UpdateCurrentWaveText(EnemyWave currentWave) {
		Transform infoPanelTransform = transform.Find ("infoPanel");
		Transform nextWaveTextTransform = infoPanelTransform.Find ("nextWaveTimerText");
		Text t = nextWaveTextTransform.GetComponent<Text> ();
		t.text = "Current wave: " + currentWave.enemyType.ToString();
	}

	public void SetNextWaveTypeText(string s) {
		Transform infoPanelTransform = transform.Find ("infoPanel");
		Transform nextWaveTextTransform = infoPanelTransform.Find ("nextWaveTypeText");
		Text t = nextWaveTextTransform.GetComponent<Text> ();
		t.text = s;
	}

	public void HideNextWaveTypeText() {
		SetNextWaveTypeText ("");
	}

	public void UpdateNextWaveTypeText(EnemyWave nextWave) {
		SetNextWaveTypeText ("Next wave: " + nextWave.enemyType.ToString ());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
