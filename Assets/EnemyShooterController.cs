using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : MonoBehaviour
{

    public float speed = 1.0f;
    public float rotationSpeed = 10.0f;
    public Transform target;
    public Transform baseTarget;
    public float range = 5;
    public float shootingRate = 1;
    public Transform projectile;

    private float cooldown;
    private bool shooting = false;

    private void moveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void lookAtTarget()
    {
        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    // Use this for initialization
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = baseTarget;
            shooting = false;
        }
        shooting = (Vector3.Distance(transform.position, target.transform.position) <= range);

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        } else if( shooting && target != baseTarget)
		{
			Transform proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
			proj.GetComponent<ProjectileBehaviour>().target = target;
			cooldown = shootingRate;
		} else
        {
            moveTowardsTarget();
            lookAtTarget();
        }

    }
}
