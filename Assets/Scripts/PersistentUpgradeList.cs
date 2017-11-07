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
            5,
            2,
            0.05f,
            0.01f,
            "Damage"
        ),
        new PersistentUpgrade(
            new PersistentUpgradeSerializable(UpgradeType.BASIC_TOWER_ATTACKSPEED, 0),
            "Basic Tower Attack Speed",
            "Increases attack speed of basic towers.",
            null,
            500,
            2,
            0.05f,
            0.01f,
            "Attack Speed"
        ),
        new PersistentUpgrade(
            new PersistentUpgradeSerializable(UpgradeType.BASIC_TOWER_RANGE, 0),
            "Basic Tower Range",
            "Increases range of basic towers.",
            null,
            5,
            2,
            0.05f,
            0.01f,
            "Range"
        ),

    };
}
