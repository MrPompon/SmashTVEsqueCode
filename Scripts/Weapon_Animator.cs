using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Animator : MonoBehaviour
{
    public Weapon weapon;
    public Animator animator;

    private void Awake()
    {
        if(animator!=null && weapon != null)
        {
            weapon.OnReloadStarted += ReloadStarted;

        }
    }
    private void ReloadStarted()
    {
        animator.SetTrigger("Reload");
    }
}
