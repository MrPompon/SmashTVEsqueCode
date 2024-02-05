using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BaseStat", menuName ="Pomp/BaseStat")]
public class SO_BaseStats : ScriptableObject
{
    public float AttackingMovementSpeed { get { return movementSpeed * attackingMovementSpeedMultiplier; } }
    public int HP=3;
    public float movementSpeed=0.016f;
    public int damage = 1;

    public float aggroRange = 1.5f;

    public float attackRange =0.32f;
    public float attackCooldown = 2f;
    public float attackDelay = 0.2f;
    [Space(10)]
    public float attackHitBoxDuration = 0.1f;
    public float attackingMovementSpeedMultiplier = 1; //move this later probably -_-
    public float knockBackForce = 0;
}
