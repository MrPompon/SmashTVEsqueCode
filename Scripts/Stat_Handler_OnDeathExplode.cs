using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Handler_OnDeathExplode : Stat_Handler_Component
{
    public LayerMask affectedLayers;
    public float radius = 0.032f;

    public void Awake()
    {
        statHandler.OnDeath += Explode;
    }
    public void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, radius, affectedLayers);

        foreach(Collider2D colli in hits)
        {
            StatHandler statHandler = colli.GetComponentInChildren<StatHandler>();

            if (statHandler != null && statHandler != this.statHandler)
            {
                  statHandler.Damage(colli.transform.position, this.statHandler.baseStats.damage);
            }
        }
    }
}
