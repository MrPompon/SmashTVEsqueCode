using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_Spawn : GameEvent
{
    public EntitySpawner spawner;
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (spawner != null)
        {
            spawner.Spawn();
        }
    }
}
