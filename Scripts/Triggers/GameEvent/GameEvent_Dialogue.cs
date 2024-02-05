using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvent_Dialogue : GameEvent
{
    public TextActivator textActivator;
    private void OnValidate()
    {
        if (textActivator == null)
        {
            textActivator = GetComponent<TextActivator>();
        }
    }
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (textActivator != null)
        {
            textActivator.ActivateDialogue();
        }
    }
}
