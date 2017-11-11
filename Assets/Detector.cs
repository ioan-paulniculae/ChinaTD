using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    private float minDistance = 100;

    public GameObject shooter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);
            if (distance < minDistance)
            {
                shooter.GetComponent<EnemyShooterController>().target = collision.transform;
                minDistance = 100;
            }
        }
    }

}
