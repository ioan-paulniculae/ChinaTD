using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour {

	public float currentHealthPercentage = 1.0f;

	private Transform fullHealthBarTransform;

	public void setCurrentHealth(float newValue) {
		currentHealthPercentage = newValue;
	}

	// Use this for initialization
	void Start () {
		fullHealthBarTransform = transform.Find ("FullHealthBar");
	}
	
	// Update is called once per frame
	void Update () {
		fullHealthBarTransform.localScale = new Vector3 (currentHealthPercentage, 1.0f);
	}
}
