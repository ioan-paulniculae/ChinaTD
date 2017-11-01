using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public Transform target;
    public float speed;
    public float damage = 2.0f;

    public bool targetsGround = true;
    public bool targetsAir = false;

    public bool splashDamage = false;
    public float splashRange = 30.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LookAtTarget();
        MoveTowardsTarget();
    }

    private void DamageEnemy(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        enemyStats.ReceiveDamage(damage);
    }

    private void LookAtTarget() {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else {
            // If the enemy is already dead, the projectile disappears?
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (targetsGround && collision.gameObject.tag == "Enemy")
        {
            CollideWithEnemy(collision);
        }

        if (targetsAir && collision.gameObject.tag == "AirEnemy")
        {
            CollideWithEnemy(collision);
        }

    }

    private void CollideWithEnemy(Collision2D collision)
    {
        // Damage the enemy.
        if (splashDamage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRange);
            foreach (Collider coll in hitColliders)
            {
                DamageEnemy(coll.gameObject);
            }
        }
        else
        {
            DamageEnemy(collision.gameObject);
        }

        // Destroy the projectile.
        Destroy(gameObject);
    }
}
