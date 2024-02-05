using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObjectTracker : Tracker
{
    public System.Action<Transform> OnAreaEnter, OnAreaExit;
    

    protected void LocalOnAreaEnter(Transform entered)
    {
        if(!trackedTransforms.Contains(entered))
        trackedTransforms.Add(entered);
    }
    protected void LocalOnAreaExit(Transform exited)
    {
        if (trackedTransforms.Contains(exited))
            trackedTransforms.Remove(exited);

    }
    public void CallOnAreaEnter(Transform enter)
    {
        LocalOnAreaEnter(enter);

        OnAreaEnter?.Invoke(enter);
    }
    public void CallOnAreaExit(Transform exit)
    {
        LocalOnAreaExit(exit);

        OnAreaExit?.Invoke(exit);
    }
 
}
