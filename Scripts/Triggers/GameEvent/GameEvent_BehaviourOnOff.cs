using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_BehaviourOnOff : GameEvent
{
    public Behaviour goBehaviour;
    public bool value;
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (goBehaviour != null)
        {
            goBehaviour.enabled =value;
        }
    }
}
