using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public UpgradeHandler upgradeHandler;
    public StatHandler statHandler;
    public enum PickupType
    {
        AMMO,
        CLIPSIZE,
        HEALTHPACK,

    }
    public void HandlePickup(PickupType type)
    {
        switch (type)
        {
            case PickupType.AMMO:
                break;
            case PickupType.CLIPSIZE:
                upgradeHandler.clipUpgrades++;
                break;
            case PickupType.HEALTHPACK:
                statHandler.Heal(2);
                break;
        }
    }
}