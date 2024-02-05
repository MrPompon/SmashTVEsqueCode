using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAreaTracker2D : AreaObjectTracker
{
    public CollisionCheckType colliCheckType;
    public enum CollisionCheckType
    {
        LAYERMASK,
        TAG,
    }
    public string tagToCheck;

    public LayerMask trackedTransformsLayer;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (IsColliderValid(other))
            CallOnAreaEnter(other.transform);

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (IsColliderValid(other))
            CallOnAreaExit(other.transform);

    }
    bool IsColliderValid(Collider2D other)
    {
        switch (colliCheckType)
        {
            case CollisionCheckType.LAYERMASK:
                if (((1 << other.gameObject.layer) & trackedTransformsLayer) != 0)
                {
                    return true;
                }
                break;
            case CollisionCheckType.TAG:
                if (other.CompareTag(tagToCheck))
                    return true;
                break;
        }
        return false;
    }
}