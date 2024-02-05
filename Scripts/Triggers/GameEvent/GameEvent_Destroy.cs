using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_Destroy : GameEvent
{
    public GameObject toDestroy;
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (toDestroy != null)
        {
            Destroy(toDestroy);
        }
    }
}
