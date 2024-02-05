using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    protected List<Transform> trackedTransforms = new List<Transform>();
   
    public List<Transform> GetTrackedTransformsRemoveNulls()
    {
        RemoveNulls();
        return trackedTransforms;
    }
    void RemoveNulls()
    {
        for (int i = trackedTransforms.Count-1;i >= 0; i--)
        {
            if (trackedTransforms[i] == null)
            {
                trackedTransforms.RemoveAt(i);
            }
        }
    }
}
