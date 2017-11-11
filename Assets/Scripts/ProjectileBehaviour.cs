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
    public float splashRange = 1.0f;

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
        //Debug.Log(enemy.name);
        if (enemy.GetComponent<EnemyStats>())
            enemy.GetComponent<EnemyStats>().ReceiveDamage(damage);
        else
            enemy.GetComponent<TowerStats>().ReceiveDamage(damage);
    }

    private void LookAtTarget() {
        if (target != null)
        {
            if ( target.gameObject.tag == "Tower")
            {
                if(Vector3.Distance(transform.position, target.transform.position) < 0.3)
                {
                    DamageEnemy(target.gameObject);
                    Destroy(gameObject);
                }
            }
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

	private bool CanTarget(string tag) {
		if (targetsGround && tag == "Enemy") {
			return true;
		} else if (targetsAir && tag == "AirEnemy") {
			return true;
		} else {
			return false;
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target) {
            if (splashDamage)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
                foreach (Collider2D coll in hitColliders)
                {
                    if (CanTarget(coll.gameObject.tag))
                    {
                        DamageEnemy(coll.gameObject);
                    }
                }
            }
            // For single target projectiles, damage the enemy it collided with.
            else
            {
                DamageEnemy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
