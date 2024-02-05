using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_Create : GameEvent
{
    public GameObject toCreate;
    public Transform creationTransformPosition;
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if(creationTransformPosition != null)
        {
            GameObject newSpawn = Instantiate(toCreate as GameObject, creationTransformPosition.position, Quaternion.identity, null);
        }
        else
        {
            GameObject newSpawn = Instantiate(toCreate as GameObject, this.transform.position, Quaternion.identity, null);
        }
    }
}
