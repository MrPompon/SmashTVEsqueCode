using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    public int UpgradedClipAmmo { get { return clipUpgrades * ammoPerUpgrade; } }
    public int ammoPerUpgrade = 1;

    public int clipUpgrades = 0;
}
