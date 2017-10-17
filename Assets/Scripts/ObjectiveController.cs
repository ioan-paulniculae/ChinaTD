using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour {

	public GameplayManager gameplayManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			GameObject enemyObject = coll.gameObject;
			EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats> ();

			gameplayManager.substractLives (enemyStats.livesCost);
			Object.Destroy (enemyObject);
		}
	}
}
