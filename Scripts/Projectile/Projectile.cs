using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigid;
    
    private Vector3 direction;
    private float speedForce;
    public void Init(Vector3 direction, float speedForce)
    {
        this.direction = direction;
        this.speedForce = speedForce;
    }
    public void FixedUpdate()
    {
        rigid.AddForce(direction * speedForce, ForceMode2D.Force);
    }
}
