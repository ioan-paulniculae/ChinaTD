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

	public enum EnemyWaveModifierType {
		NONE,
		SWARMING,
		HARDENED,
		AGILE
	}
	private static EnemyWaveModifierType[] modifierTypes = new EnemyWaveModifierType[]{EnemyWaveModifierType.NONE,
		EnemyWaveModifierType.SWARMING, EnemyWaveModifierType.HARDENED, EnemyWaveModifierType.AGILE};

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

	public GameplayManager gameplayManager;
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

	[Tooltip("Minimum number of waves until a special wave spawns.")]
	public int minSpecialWaveInterval = 3;

	[Tooltip("Maximum number of waves until a special wave spawns.")]
	public int maxSpecialWaveInterval = 5;
	[Space]

	[Tooltip("A preset list of waves for the beginning of the game.")]
	public List<EnemyWave> presetEnemyWaves;

	private float spawnTimer = 2.0f;
	private int specialWaveCountdown = 0;
	private EnemyWave currentWave;
	private EnemyWave nextWave;

	/* Prepare first wave. */
	void Start () {
		nextWave = GetNextWave ();
		uiCanvas.UpdateNextWaveTypeText (nextWave);
		StartCoroutine (StartNextWave (gameStartTimer));
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
			EnemyWaveModifierType modifierType = EnemyWaveModifierType.NONE;

			/* Add a modifier if the special wave countdown is up. */
			specialWaveCountdown -= 1;
			if (specialWaveCountdown <= 0) {
				specialWaveCountdown = Random.Range (minSpecialWaveInterval, maxSpecialWaveInterval + 1);

				int randomIndex = Random.Range (1, modifierTypes.Length);
				modifierType = modifierTypes [randomIndex];
			}

			newWave = new EnemyWave (randomEntry.enemyType, groupCount, modifierType);
		}

		return newWave;
	}

	/* Spawns a group of enemies and sets their waypoints. */
	void SpawnEnemyGroup(EnemyPrefabEntry enemyPrefabEntry, EnemyWaveModifierType waveModifier) {
		int groupSize = enemyPrefabEntry.groupSize;

		/* Apply SWARMING wave modifier, if necessary. */
		if (waveModifier == EnemyWaveModifierType.SWARMING) {
			groupSize *= 2;
		}
			
		/* Spawn group of enemies. */
		foreach (GameObject spawner in spawners) {
			for (int i = 0; i < groupSize; i++) {
				EnemyMovementController newEnemy = Instantiate (enemyPrefabEntry.enemyPrefab);
				Vector3 spawnerPosition = spawner.transform.position;

				newEnemy.ApplyWaveModifier (waveModifier);
				newEnemy.transform.position = spawnerPosition;
				newEnemy.transform.Translate (Vector3.back * 0.5f);
				newEnemy.setPath (spawner.GetComponent<SpawnerTile> ().pathPoints);
			}
		}
	}

	/* Waits for a number of seconds then starts spawning a wave. */
	IEnumerator StartNextWave(float seconds) {
		spawnTimer = seconds;

		/* Waits for the given number of seconds. */
		while (spawnTimer > 0) {
			uiCanvas.UpdateNextWaveTimerText (spawnTimer);
			yield return new WaitForSeconds (1.0f);
			spawnTimer -= 1.0f;
		}

		/* Updates the UI texts. */
		currentWave = nextWave;
		uiCanvas.UpdateCurrentWaveText (currentWave);
		uiCanvas.HideNextWaveTypeText ();
		gameplayManager.WaveSpawned ();

		/* Spawn the next wave. */
		StartCoroutine (SpawnEnemyRoutine (currentWave));
	}

	/* Spawns a wave of enemies and starts the timer for the next wave. */
	IEnumerator SpawnEnemyRoutine(EnemyWave enemyWave) {
		EnemyPrefabEntry enemyPrefabEntry = GetEnemyPrefabEntryForWave (enemyWave);

		for (int i = 0; i < enemyWave.enemyCount; i++) {
			SpawnEnemyGroup (enemyPrefabEntry, enemyWave.modifier);
			yield return new WaitForSeconds (enemyPrefabEntry.timeBetweenGroups);
		}

		/* Enemies finished spawning. Now we can start the timer for the next wave. */
		nextWave = GetNextWave ();
		uiCanvas.UpdateNextWaveTypeText (nextWave);
		StartCoroutine (StartNextWave (Random.Range((int)minWaveInterval, (int)maxWaveInterval)));
	}
}
