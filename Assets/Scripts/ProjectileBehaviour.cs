using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public Transform target;
    public float speed;
    public float damage = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        } else {
            // If the enemy is already dead, the projectile disappears?
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Destroy the projectile.
            Destroy(gameObject);

            // Damage the enemy.
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            enemyStats.ReceiveDamage(damage);
        }

    }
}
