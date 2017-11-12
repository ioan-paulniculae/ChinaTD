using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave {

	public EnemyWave(EnemySpawner.WaveType waveType, int enemyCount, EnemySpawner.EnemyWaveModifierType modifier) {
		this.waveType = waveType;
		this.enemyCount = enemyCount;
		this.modifier = modifier;
	}

	public EnemySpawner.WaveType waveType;
	public int enemyCount;
	public EnemySpawner.EnemyWaveModifierType modifier;
}
