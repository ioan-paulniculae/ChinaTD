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

	[Header("Aura Effects")]
	public Color damageAuraColor;
	public Color attackSpeedAuraColor;
	public Color rangeAuraColor;

    private float duration;

    private HealthBarController healthBar;
    private GameplayManager gameplayManager;
	private PersistentUpgradesManager persistentUpgradesManager = PersistentUpgradesManager.instance;
	private GameObject renderObject;

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
		renderObject = transform.Find ("RenderObject").gameObject;

		ApplyAura ();
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
			
		/* Apply aura to nearby towers. */
		if (HasAura ()) {
			List<TowerBehaviour> allTowers = gameplayManager.GetTowers ();
			foreach (TowerBehaviour tower in allTowers) {
				if (Vector3.Distance (transform.position, tower.transform.position) <= range) {
					tower.ChangeColor (GetAuraColor ());
					tower.ApplyAura (GetActiveAura ());
				} else {
					tower.RemoveAura (GetActiveAura ());
				}
			}
		}
    }

	void OnDestroy() {
		// Remove aura from nearby towers.
		if (HasAura ()) {
			List<TowerBehaviour> allTowers = gameplayManager.GetTowers ();
			foreach (TowerBehaviour tower in allTowers) {
				tower.RemoveAura (GetActiveAura ());
			}
		}
	}
    
	private void ChangeScale(int scale) {
		renderObject.transform.localScale = new Vector3 (scale, scale, 1.0f);
	}

	private void ChangeColor(Color newColor) {
		renderObject.GetComponent<SpriteRenderer> ().color = newColor;
	}

	private bool HasAura() {
		return (persistentUpgradesManager.GetActiveAuraType () != UpgradeType.DRAGON_NO_AURA);
	}

	private PersistentUpgrade GetActiveAura() {
		return persistentUpgradesManager.GetActiveAura ();
	}

	private Color GetAuraColor() {
		switch (persistentUpgradesManager.GetActiveAuraType ()) {
		case UpgradeType.DRAGON_DAMAGE_AURA:
			return damageAuraColor;
		case UpgradeType.DRAGON_ATTACKSPEED_AURA:
			return attackSpeedAuraColor;
		case UpgradeType.DRAGON_RANGE_AURA:
			return rangeAuraColor;
		}

		// Fallback value.
		return Color.white;
	}

	private void ApplyAura() {
		if (HasAura()) {
			// Change dragon's scale.
			ChangeScale(5);

			// Change dragon's color.
			ChangeColor (GetAuraColor ());
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
