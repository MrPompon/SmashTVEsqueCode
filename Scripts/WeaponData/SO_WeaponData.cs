using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStat", menuName = "Pomp/WeaponStat")]
public class SO_WeaponData : ScriptableObject
{
    public float minRange=5f, maxRange=6f;
    public int damage =1;
    public float knockbackForce=5f;
    public KnockBackMode knockBackMode = KnockBackMode.WeaponHolderComparison;

    public float spread = 0;
    public float startUpDelay = 0;
    public float hitScanDelay =0;

    public bool requiresAmmo = false;
    public int shotsPerAmmo = 1;
    public int ammoDrain = 1;
    public int clipSize = 7;
    public float initReloadTime = 1f;
    public float reloadDuration = 2.2f;

    public float kickBack = 0f;
    public float screenShake = 0f;

    public enum KnockBackMode
    {
        WeaponHolderComparison,
        WeaponHolderForward,
    }
}
