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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ShooterEnemy")
        {
            EnemyShooterController controller = collision.gameObject.GetComponent<EnemyShooterController>();
            if (controller.target == controller.baseTarget)
            {
                GameObject enemyObject = collision.gameObject;
                EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats>();

                gameplayManager.substractLives(enemyStats.livesCost);
                Object.Destroy(enemyObject);
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "AirEnemy")
        {
            GameObject enemyObject = collision.gameObject;
            EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats>();

            gameplayManager.substractLives(enemyStats.livesCost);
            Object.Destroy(enemyObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ShooterEnemy")
        {
            EnemyShooterController controller = collision.gameObject.GetComponent<EnemyShooterController>();
            if (controller.target == controller.baseTarget)
            {
                GameObject enemyObject = collision.gameObject;
                EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats>();

                gameplayManager.substractLives(enemyStats.livesCost);
                Object.Destroy(enemyObject);
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "AirEnemy")
        {
            GameObject enemyObject = collision.gameObject;
            EnemyStats enemyStats = enemyObject.GetComponent<EnemyStats>();

            gameplayManager.substractLives(enemyStats.livesCost);
            Object.Destroy(enemyObject);
        }
    }
}
