using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public enum EnemyType {
		GROUND,
		SWARM,
		FLYING,
		BOSS
	}

	public enum ModifierType {
		SWARMING,
		RESISTANT,
		AGILE
	}

	[System.Serializable]
	public class EnemyPrefabEntry
	{
		public EnemyType enemyType;
		public EnemyMovementController enemyPrefab;
		public int groupSize;
		public float timeBetweenGroups;
		public int minGroupNumber;
		public int maxGroupNumber;
	}

	public UserInterfaceController uiCanvas;
	public EnemyPrefabEntry[] enemyPrefabEntries;
	public GameObject[] spawners;

	[Header("Wave Timers")]
	[Tooltip("Seconds until the first wave spawns.")]
	public float gameStartTimer = 10.0f;

	[Tooltip("Minimum amount of time between waves.")]
	public float minWaveInterval = 6.0f;

	[Tooltip("Maximum amount of time between waves.")]
	public float maxWaveInterval = 10.0f;
	[Space]

	[Tooltip("A preset list of waves for the beginning of the game.")]
	public List<EnemyWave> presetEnemyWaves;

	private float spawnTimer = 2.0f;

	/* Prepare first wave. */
	void Start () {
		StartCoroutine (StartWave (gameStartTimer, GetNextWave()));
	}

	/* To be used for debug purposes mainly. */
	public void ForceNextWave() {
		spawnTimer = 0.0f;
	}
		
	EnemyPrefabEntry GetEnemyPrefabEntryForWave(EnemyWave wave) {
		foreach(EnemyPrefabEntry entry in enemyPrefabEntries) {
			if (entry.enemyType == wave.enemyType) {
				return entry;
			}
		}

		return null;
	}

	/* Gets the next wave. If there are preset waves left, picks the next one.
	 * Otherwise, returns a random wave. */
	EnemyWave GetNextWave() {
		EnemyWave newWave;

		if (presetEnemyWaves.Count > 0) {
			newWave = presetEnemyWaves [0];
			presetEnemyWaves.RemoveAt (0);
		} else {
			/* Generate a random wave. */
			EnemyPrefabEntry randomEntry = enemyPrefabEntries[Random.Range(0, enemyPrefabEntries.Length)];
			int groupCount = Random.Range (randomEntry.minGroupNumber, randomEntry.maxGroupNumber + 1);
			newWave = new EnemyWave (randomEntry.enemyType, groupCount);
		}

		return newWave;
	}

	/* Spawns a group of enemies and sets their waypoints. */
	void SpawnEnemyGroup(EnemyPrefabEntry enemyPrefabEntry) {
		foreach (GameObject spawner in spawners) {
			for (int i = 0; i < enemyPrefabEntry.groupSize; i++) {
				EnemyMovementController newEnemy = Instantiate (enemyPrefabEntry.enemyPrefab);
				Vector3 spawnerPosition = spawner.transform.position;

				newEnemy.transform.position = spawnerPosition;
				newEnemy.transform.Translate (Vector3.back * 0.5f);
				newEnemy.setPath (spawner.GetComponent<SpawnerTile> ().pathPoints);
			}
		}
	}

	/* Waits for a number of seconds then starts spawning a wave. */
	IEnumerator StartWave(float seconds, EnemyWave enemyWave) {
		spawnTimer = seconds;

		/* Waits for the given number of seconds. */
		while (spawnTimer > 0) {
			uiCanvas.UpdateNextWaveText (spawnTimer);
			yield return new WaitForSeconds (1.0f);
			spawnTimer -= 1.0f;
		}
		uiCanvas.UpdateNextWaveText (0.0f);

		/* Spawn the next wave. */
		StartCoroutine (SpawnEnemyRoutine (enemyWave));
	}

	/* Spawns a wave of enemies and starts the timer for the next wave. */
	IEnumerator SpawnEnemyRoutine(EnemyWave enemyWave) {
		EnemyPrefabEntry enemyPrefabEntry = GetEnemyPrefabEntryForWave (enemyWave);

		for (int i = 0; i < enemyWave.enemyCount; i++) {
			SpawnEnemyGroup (enemyPrefabEntry);
			yield return new WaitForSeconds (enemyPrefabEntry.timeBetweenGroups);
		}

		/* Enemies finished spawning. Now we can start the timer for the next wave. */
		StartCoroutine (StartWave (Random.Range((int)minWaveInterval, (int)maxWaveInterval), GetNextWave()));
	}
}
