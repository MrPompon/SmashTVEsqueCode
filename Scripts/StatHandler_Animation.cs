using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatHandler_Animation : Stat_Handler_Component
{
    public Animator animator;

    private void Awake()
    {
        statHandler.OnDamageTakenPoint += Damaged;
    }
    private void Damaged(Vector2 point, int damage)
    {
        if (animator != null)
        {
            animator.SetTrigger("Damaged");
        }
    }
}
