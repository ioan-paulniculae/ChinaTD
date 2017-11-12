using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    private float minDistance = 100;
	private GameplayManager gameplayManager;

    public GameObject shooter;

    // Use this for initialization
    void Start () {
		gameplayManager = FindObjectOfType<GameplayManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		/* Alternate behaviour: always go to nearest tower. */
		List<TowerBehaviour> towers = gameplayManager.GetTowers ();
		TowerBehaviour closestTower = FindClosestTower (towers);
		if (closestTower) {
			shooter.GetComponent<EnemyShooterController> ().target = closestTower.transform;
		}
	}

	private TowerBehaviour FindClosestTower(List<TowerBehaviour> towers) {
		TowerBehaviour closestTower = null;
		float currentMinDistance = 1000;

		foreach (TowerBehaviour tower in towers) {
			float distance = Vector3.Distance (transform.position, tower.transform.position);
			if (distance < currentMinDistance) {
				currentMinDistance = distance;
				closestTower = tower;
			}
		}

		return closestTower;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
//        if (collision.gameObject.tag == "Tower")
//        {
//            float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);
//            if (distance < minDistance)
//            {
//                shooter.GetComponent<EnemyShooterController>().target = collision.transform;
//                minDistance = 100;
//            }
//        }
    }

}
