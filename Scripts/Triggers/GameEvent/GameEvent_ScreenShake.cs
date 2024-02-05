using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_ScreenShake : GameEvent
{
    public bool useDefault = true;
    public float duration=1;
    public float minMagnitude= -0.5f, maxMagnitude = 0.5f;
    public float decreaseFactor=0.01f;
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (useDefault)
        {
            ScreenShake.ScreenShaker.Shake(1f);
        }
        else
        {
            ScreenShake.ScreenShaker.Shake(duration, decreaseFactor, minMagnitude,maxMagnitude);
        }
       
    }
}
