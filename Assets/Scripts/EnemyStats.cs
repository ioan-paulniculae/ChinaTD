using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	public float health = 5.0f;
	public int livesCost = 1;

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

	void OnDestroy() {

	}
}
