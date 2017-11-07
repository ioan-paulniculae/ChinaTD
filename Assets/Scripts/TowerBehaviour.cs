using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType {
	BASIC_TOWER,
	SPLASH_TOWER,
	ANTIAIR_TOWER
}

public class TowerBehaviour : MonoBehaviour {

    public float range;

	public TowerType type;
    public bool targetsGround = true;
    public bool targetsAir = false;

    public float fireRate;
    public Transform projectile;

    List<GameObject> targetList = new List<GameObject>();
    float cooldown;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<CircleCollider2D>().radius = range;
        cooldown = 0;
        targetList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {

        if (targetList.Count > 0)
            while ( targetList[targetList.Count - 1] == null)
            {
                targetList.RemoveAt(targetList.Count - 1);
            }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if(targetList.Count > 0 )
        {
            cooldown = fireRate;
            Transform proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileBehaviour>().target = targetList[0].transform;
            //shoot at first member of targetlist   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetsGround && other.tag == "Enemy")
        {
            targetList.Add(other.gameObject);
        }

        if (targetsAir && other.tag == "AirEnemy")
        {
            targetList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (targetsGround && other.tag == "Enemy")
        {
            targetList.Remove(other.gameObject);
        }

        if (targetsAir && other.tag == "AirEnemy")
        {
            targetList.Remove(other.gameObject);
        }
    }
}
