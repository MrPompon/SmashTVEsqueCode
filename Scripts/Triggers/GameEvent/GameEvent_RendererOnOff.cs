using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_RendererOnOff : GameEvent
{
    public Renderer rend;
    public bool value;
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (rend != null)
        {
            rend.enabled = value;
        }
    }
}
