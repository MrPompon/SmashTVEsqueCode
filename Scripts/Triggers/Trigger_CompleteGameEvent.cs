using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_CompleteGameEvent : Trigger
{
    public Game_EventManager.Event_Completion eventCompletion;
    protected override void Triggered(Collider2D collision)
    {
        base.Triggered(collision);
        Game_EventManager.CompleteEvent(eventCompletion);
    }
}
