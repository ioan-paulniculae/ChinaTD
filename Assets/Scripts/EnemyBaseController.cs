using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseController : MonoBehaviour {

	public abstract void ApplyWaveModifier (EnemySpawner.EnemyWaveModifierType modifier);
	public abstract void setPath (GameObject[] pathPoints);
}
