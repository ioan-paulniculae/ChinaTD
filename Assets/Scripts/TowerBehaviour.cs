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

	/* base cost: i.e. 50 gold */
	public int sessionCurrencyCost = 50;

	/* rate at which cost grows with additional towers. i.e. 5 more gold per level (will have exponential scaling based on this) */
	public int sessionCurrencyAdditionalCostPerTower = 10;

    List<GameObject> targetList = new List<GameObject>();
    float cooldown;
	PersistentUpgradesManager persistentUpgradesManager = PersistentUpgradesManager.instance;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<CircleCollider2D>().radius = range;
        cooldown = 0;
        targetList = new List<GameObject>();
		ApplyPersistentUpgrades ();
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

	private float GetDamageUpgradeEffect() {
		switch (type) {
		case TowerType.BASIC_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.BASIC_TOWER_DAMAGE).GetCurrentEffect();
		case TowerType.SPLASH_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.SPLASH_TOWER_DAMAGE).GetCurrentEffect();
		case TowerType.ANTIAIR_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.ANTIAIR_TOWER_DAMAGE).GetCurrentEffect();
		}

		// Fallback.
		return 0;
	}

	private float GetAttackSpeedUpgradeEffect() {
		switch (type) {
		case TowerType.BASIC_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.BASIC_TOWER_ATTACKSPEED).GetCurrentEffect();
		case TowerType.SPLASH_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.SPLASH_TOWER_ATTACKSPEED).GetCurrentEffect();
		case TowerType.ANTIAIR_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.ANTIAIR_TOWER_ATTACKSPEED).GetCurrentEffect();
		}

		// Fallback.
		return 0;
	}

	private float GetRangeUpgradeEffect() {
		switch (type) {
		case TowerType.BASIC_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.BASIC_TOWER_RANGE).GetCurrentEffect();
		case TowerType.SPLASH_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.SPLASH_TOWER_RANGE).GetCurrentEffect();
		case TowerType.ANTIAIR_TOWER:
			return persistentUpgradesManager.GetUpgrade(UpgradeType.ANTIAIR_TOWER_RANGE).GetCurrentEffect();
		}

		// Fallback.
		return 0;
	}

	private void ApplyPersistentUpgrades() {
		ApplyDamageUpgrade ();
		ApplyAttackSpeedUpgrade ();
		ApplyRangeUpgrade ();
	}

	private void ApplyDamageUpgrade() {
		ProjectileBehaviour projectileBehaviour = projectile.GetComponent<ProjectileBehaviour> ();
		projectileBehaviour.damage *= (1 + GetDamageUpgradeEffect ());
	}

	private void ApplyAttackSpeedUpgrade() {
		fireRate /= (1 + GetAttackSpeedUpgradeEffect ());
	}

	private void ApplyRangeUpgrade() {
		range *= (1 + GetRangeUpgradeEffect ());
	}
}
