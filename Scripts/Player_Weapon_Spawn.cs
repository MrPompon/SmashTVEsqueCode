using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon_Spawn : Player_Weapon
{
    public GameObject toSpawn;
    protected override void Fire()
    {
        //base.Fire();
        OnFire?.Invoke();
        ApplyKickBack();
        ApplyScreenShake();

    }
    void SpawnObject()
    {

    }
}
