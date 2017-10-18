using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public EnemyMovementController[] enemyPrefabs;
	public GameObject objective;
	public GameObject[] spawners;

	public float spawnTimer = 2.0f;
	public float enemiesToSpawn = 2;

	private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

	void spawnEnemy() {
		foreach (GameObject spawner in spawners) {
			if (enemyPrefabs.Length > 0) {
				for (int i = 0; i < enemiesToSpawn; i++) {
					EnemyMovementController newEnemy = Instantiate (enemyPrefabs [0]);
					Vector3 spawnerPosition = spawner.transform.position;

					newEnemy.transform.position = spawnerPosition;
					newEnemy.transform.Translate (Vector3.back * 0.5f);
					newEnemy.setPath (spawner.GetComponent<SpawnerTile> ().pathPoints);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= spawnTimer) {
			elapsedTime = 0.0f;
			spawnEnemy ();
		}
	}
}
