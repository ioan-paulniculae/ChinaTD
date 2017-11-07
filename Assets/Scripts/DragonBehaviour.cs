using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragonBehaviour : MonoBehaviour
{
    private Ray ray;
    private Vector3Int mousePos;
    private Tilemap towerMap;
    private bool moving;
    private Vector3 moveTarget;
    private List<GameObject> targetList = new List<GameObject>();
    private float cooldown;

    public float range;
    public float speed = 1.0f;
    public float fireRate;
    public bool targetsGround = true;
    public bool targetsAir = true;
    public Transform projectile;

    public float maxDuration = 10;
    private float duration;

    private HealthBarController healthBar;
    private GameplayManager gameplayManager;

    // Use this for initialization
    void Start()
    {
        moving = false;
        gameObject.GetComponent<CircleCollider2D>().radius = range;
        cooldown = 0;
        targetList = new List<GameObject>();
        towerMap = GameObject.Find("TowerMap").GetComponent<Tilemap>();

        duration = maxDuration;
        healthBar = gameObject.GetComponentInChildren<HealthBarController>();
        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        MoveBehaviour();

        if (targetList.Count > 0)
            while (targetList[targetList.Count - 1] == null)
            {
                targetList.RemoveAt(targetList.Count - 1);
            }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (targetList.Count > 0 && !moving)
        {
            moveTarget = targetList[0].transform.position;
            LookAtTarget();
            cooldown = fireRate;
            Transform proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileBehaviour>().target = targetList[0].transform;
            //shoot at first member of targetlist   
        }

        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void UpdateHealthBar()
    {
        duration -= Time.deltaTime;
        float newScale = duration / maxDuration;
        healthBar.setCurrentHealth(newScale);
    }

    private void MoveBehaviour()
    {
        if (Input.GetKeyUp("mouse 0"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            mousePos = towerMap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));

            if (!towerMap.HasTile(mousePos))
            {
                moving = true;
                moveTarget = new Vector3(ray.origin.x, ray.origin.y, 0);
            }
        }

        if (moving)
        {
            LookAtTarget();
            MoveTowardsTarget();
        }
    }

    private void LookAtTarget()
    {
        Vector3 direction = moveTarget - transform.position;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

    }

    private void MoveTowardsTarget()
    {
        if (moveTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
        }
        if (transform.position == moveTarget)
        {
            moving = false;
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
