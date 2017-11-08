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
	bool hasAura;
	float damageMultiplier = 1.0f;	// this is used when instantiating projectiles.

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
			proj.GetComponent<ProjectileBehaviour> ().damage *= damageMultiplier;
            //shoot at first member of targetlist   
        }
    }

	public void ChangeColor(Color newColor) {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = newColor;
	}

	public void ApplyAura(PersistentUpgrade aura) {
		if (hasAura) {
			return;
		}

		hasAura = true;
		UpgradeType type = aura.info.type;
		float effect = aura.GetCurrentEffect ();

		switch (type) {
		case UpgradeType.DRAGON_DAMAGE_AURA:
			ApplyDamageUpgrade (effect, true);
			break;
		case UpgradeType.DRAGON_ATTACKSPEED_AURA:
			ApplyAttackSpeedUpgrade (effect, true);
			break;
		case UpgradeType.DRAGON_RANGE_AURA:
			ApplyRangeUpgrade (effect, true);
			break;
		}
	}

	public void RemoveAura(PersistentUpgrade aura) {
		if (!hasAura) {
			return;
		}

		hasAura = false;
		ChangeColor (Color.white);
		UpgradeType type = aura.info.type;
		float effect = aura.GetCurrentEffect ();

		switch (type) {
		case UpgradeType.DRAGON_DAMAGE_AURA:
			ApplyDamageUpgrade (effect, false);
			break;
		case UpgradeType.DRAGON_ATTACKSPEED_AURA:
			ApplyAttackSpeedUpgrade (effect, false);
			break;
		case UpgradeType.DRAGON_RANGE_AURA:
			ApplyRangeUpgrade (effect, false);
			break;
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
		ApplyDamageUpgrade (GetDamageUpgradeEffect(), true);
		ApplyAttackSpeedUpgrade (GetAttackSpeedUpgradeEffect(), true);
		ApplyRangeUpgrade (GetRangeUpgradeEffect(), true);
	}

	private void ApplyDamageUpgrade(float effect, bool increase) {
		if (increase) {
			damageMultiplier *= (1 + effect);
		} else {
			damageMultiplier /= (1 + effect);
		}
	}

	private void ApplyAttackSpeedUpgrade(float effect, bool increase) {
		if (increase) {
			fireRate /= (1 + effect);
		} else {
			fireRate *= (1 + effect);
		}
	}

	private void ApplyRangeUpgrade(float effect, bool increase) {
		if (increase) {
			range *= (1 + effect);
		} else {
			range /= (1 + effect);
		}
	}
}
