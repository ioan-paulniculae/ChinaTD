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

	public void UpdateNextWaveText(float timeLeft) {
		Transform infoPanelTransform = transform.Find ("infoPanel");
		Transform nextWaveTextTransform = infoPanelTransform.Find ("nextWaveText");
		Text t = nextWaveTextTransform.GetComponent<Text> ();
		t.text = "Next wave in: " + timeLeft + "s.";
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
