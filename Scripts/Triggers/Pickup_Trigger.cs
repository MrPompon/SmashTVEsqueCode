using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Pickup_Trigger : Trigger
{

    public PickupHandler.PickupType pickupType;

    protected override void Triggered(Collider2D colli)
    {
        PickupHandler handler = colli.GetComponentInChildren<PickupHandler>();
        if (handler != null)
        {
            handler.HandlePickup(this.pickupType);
        }
        base.Triggered(colli);
    }
}
