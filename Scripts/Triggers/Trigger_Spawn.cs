using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Spawn : Trigger
{
    public EntitySpawner spawner;
    public void OnValidate()
    {
        if (spawner == null)
        {
            spawner = GetComponentInChildren<EntitySpawner>();
        }
    }
    protected override void Triggered(Collider2D colli)
    {
        spawner.Spawn();
        base.Triggered(colli);
    }
}
