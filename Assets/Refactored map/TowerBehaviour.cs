using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour {

    public float range;
    
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
            proj.GetComponent<ProjectileBehaviour>().target = targetList[targetList.Count-1].transform;
            //shoot at last member of targetlist   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        targetList.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        targetList.Remove(collision.gameObject);
    }
}
