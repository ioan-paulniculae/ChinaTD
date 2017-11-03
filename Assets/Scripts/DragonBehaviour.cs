using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : MonoBehaviour
{

    public float range;

    public bool targetsGround = true;
    public bool targetsAir = true;

    public float fireRate;
    public Transform projectile;

    List<GameObject> targetList = new List<GameObject>();
    float cooldown;

    public float duration = 10;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = range;
        cooldown = 0;
        targetList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        if (targetList.Count > 0)
            while (targetList[targetList.Count - 1] == null)
            {
                targetList.RemoveAt(targetList.Count - 1);
            }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (targetList.Count > 0)
        {
            cooldown = fireRate;
            Transform proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileBehaviour>().target = targetList[0].transform;
            //shoot at first member of targetlist   
        }

        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "AirEnemy")
        {
            targetList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "AirEnemy")
        {
            targetList.Remove(other.gameObject);
        }
    }
}
