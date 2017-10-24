using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave {

	public EnemyWave(EnemySpawner.EnemyType enemyType, int enemyCount, EnemySpawner.EnemyWaveModifierType modifier) {
		this.enemyType = enemyType;
		this.enemyCount = enemyCount;
		this.modifier = modifier;
	}

	public EnemySpawner.EnemyType enemyType;
	public int enemyCount;
	public EnemySpawner.EnemyWaveModifierType modifier;
}
