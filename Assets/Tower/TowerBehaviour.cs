using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour {

    public float range;

    CircleCollider2D collider;
    public float fireRate;
    public GameObject projectile;

    List<GameObject> targetList = new List<GameObject>();
    float cooldown;

    // Use this for initialization
    void Start () {
        collider = gameObject.GetComponent<CircleCollider2D>();
        collider.radius = range;
        cooldown = 0;
        targetList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if( cooldown > 0)
            cooldown -= Time.deltaTime;
        if(targetList.Count > 0 )
        {
            cooldown = fireRate;
            //shoot at targetList   
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
