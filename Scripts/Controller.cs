using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatHandler))]
public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform main;
    public Rigidbody2D rigid2D;
    public Animator animator;
    public StatHandler statHandler;
    
    protected virtual void Awake()
    {
        if (statHandler == null)
        {
            statHandler = GetComponentInParent<StatHandler>();
        }
    }

}
