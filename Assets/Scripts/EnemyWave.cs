using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave {

	public EnemyWave(EnemySpawner.EnemyType enemyType, int enemyCount) {
		this.enemyType = enemyType;
		this.enemyCount = enemyCount;
	}

	public EnemySpawner.EnemyType enemyType;
	public int enemyCount;
}
