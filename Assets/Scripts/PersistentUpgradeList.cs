using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistentUpgradeList {

    public static List<PersistentUpgrade> upgrades = new List<PersistentUpgrade> {

        /* Basic tower upgrades. */
        new PersistentUpgrade(
            new PersistentUpgradeSerializable(UpgradeType.BASIC_TOWER_DAMAGE, 0),
            "Basic Tower Damage",
            "Increases damage of basic towers.",
            null,
            5,			/* base diamonds cost */
            2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
            "Damage"
        ),
        new PersistentUpgrade(
            new PersistentUpgradeSerializable(UpgradeType.BASIC_TOWER_ATTACKSPEED, 0),
            "Basic Tower Attack Speed",
            "Increases attack speed of basic towers.",
            null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
            "Attack Speed"
        ),
        new PersistentUpgrade(
            new PersistentUpgradeSerializable(UpgradeType.BASIC_TOWER_RANGE, 0),
            "Basic Tower Range",
            "Increases range of basic towers.",
            null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
            "Range"
        ),

		/* Splash tower upgrades. */
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.SPLASH_TOWER_DAMAGE, 0),
			"Splash Tower Damage",
			"Increases damage of splash towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Damage"
		),
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.SPLASH_TOWER_ATTACKSPEED, 0),
			"Splash Tower Attack Speed",
			"Increases attack speed of splash towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Attack Speed"
		),
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.SPLASH_TOWER_RANGE, 0),
			"Splash Tower Range",
			"Increases range of splash towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Range"
		),

		/* Antiair tower upgrades. */
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.ANTIAIR_TOWER_DAMAGE, 0),
			"Anti-Air Tower Damage",
			"Increases damage of anti-air towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Damage"
		),
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.ANTIAIR_TOWER_ATTACKSPEED, 0),
			"Anti-Air Tower Attack Speed",
			"Increases attack speed of anti-air towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Attack Speed"
		),
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.ANTIAIR_TOWER_RANGE, 0),
			"Anti-Air Tower Range",
			"Increases range of anti-air towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Range"
		),

		/* Dragon upgrades. */
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.DRAGON_DAMAGE_AURA, 0),
			"Dragon Damage Aura",
			"Dragon increases damage of nearby towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Damage to nearby towers"
		),
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.DRAGON_ATTACKSPEED_AURA, 0),
			"Dragon Attack Speed Aura",
			"Dragon increases attack speed of nearby towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Attack Speed to nearby towers"
		),
		new PersistentUpgrade(
			new PersistentUpgradeSerializable(UpgradeType.DRAGON_RANGE_AURA, 0),
			"Dragon Range Aura",
			"Dragon increases range of nearby towers.",
			null,
			5,			/* base diamonds cost */
			2,			/* diamonds cost increase per level */
			0.05f,		/* base effect (0.05 is 5%) */
			0.01f,      /* effect increment (0.01 is an additional 1% per level) */
			"Range to nearby towers"
		),
    };
}
