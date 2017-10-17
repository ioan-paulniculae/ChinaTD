using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour {

	public void updateLivesText(int lives) {
		Transform infoPanelTransform = transform.Find ("infoPanel");
		Transform livesTextTransform = infoPanelTransform.Find ("livesText");
		Text t = livesTextTransform.GetComponent<Text> ();
		t.text = "Lives: " + lives;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
