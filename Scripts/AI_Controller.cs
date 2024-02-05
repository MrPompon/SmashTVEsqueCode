using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : Controller
{
    public float RotationSpeed;
    public DamageBox attackHitBox;
    public GameObject projectileToFire;
    public float projectileSpawnOffset = 0.08f; //fix these projectile stuff into an SO
    public float projectileSpeedForce = 30f;
    public bool runAfterAttack = false;

    public float runDuration = 1;
    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    private float startRunTime=0;
    private bool running = false;
    private bool canAttack = true;
    private bool isAttacking = false;
    private GameObject target;

    private void OnValidate()
    {
        if (attackHitBox == null)
        {
            attackHitBox = GetComponentInChildren<DamageBox>();
        }
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (target == null || statHandler.baseStats.movementSpeed <= 0 || Vector2.Distance(target.transform.position,this.transform.position)>statHandler.baseStats.aggroRange)
        {
            return;
        }

        float movementSpeed=0;
        if (statHandler.baseStats.attackingMovementSpeedMultiplier != 0)
        {
            if (isAttacking)
            {
                movementSpeed = statHandler.baseStats.AttackingMovementSpeed;
            }
            else
            {
                movementSpeed = statHandler.baseStats.movementSpeed;
            }
        }
        if (movementSpeed > 0)
        {
            animator.SetBool("Moving", true);
            Vector2 dir = Vector2.zero;
            //rigid2D.MovePosition(rigid2D.position + dir);
            if (running)
            {
                 dir= (( main.position- target.transform.position).normalized * movementSpeed);
            }
            else
            {
                dir = ((target.transform.position - main.position).normalized * movementSpeed);
            }
            rigid2D.AddForce(dir, ForceMode2D.Force);
            Rotate();
        }
        
        if (statHandler.baseStats.attackRange > 0 &&  attackHitBox != null && canAttack && !isAttacking)
        {
            HandleAttacking();
        }
        HandleRunning();
    }

    void HandleAttacking()
    {
        if (Vector2.Distance(target.transform.position, this.transform.position) <= statHandler.baseStats.attackRange)
        {
            DoAttack();
        }
        else
        {
            ResetAttack();
        }
    }
    void HandleRunning()
    {
        if (running)
        {
            if(Time.time > startRunTime + runDuration)
            {
                running = false;
            }
        }
    }
    void DoAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }
        isAttacking = true;
        canAttack = false;
      
        if (projectileToFire != null)
        {
            Invoke("SpawnProjectileTowardsPlayer", statHandler.baseStats.attackDelay);
        }
        else
        {
            Invoke("EnableHitBox", statHandler.baseStats.attackDelay);
        }

        Invoke("ResetAttack", statHandler.baseStats.attackCooldown + statHandler.baseStats.attackHitBoxDuration + 0.1f);
    }
    private void ResetAttack()
    {
        canAttack = true;
        isAttacking = false;
   
    }
    private void EnableHitBox()
    {
        attackHitBox.SetEnabled(true, statHandler.baseStats.attackHitBoxDuration);
        if (runAfterAttack)
        {
            running = true;
            startRunTime = Time.time + runDuration;
        }
    }
    private void SpawnProjectileTowardsPlayer()
    {
        if (target == null)
            return;
        GameObject newSpawn = Instantiate(projectileToFire as GameObject, this.transform.position+ -this.transform.up * projectileSpawnOffset, Quaternion.identity, null);
        Projectile proj= newSpawn.GetComponentInChildren<Projectile>();
        if (proj != null)
        {
            proj.Init(target.transform.position- this.transform.position , projectileSpeedForce);
        }
    }
    private void Rotate()
    {
        Vector2 dir = main.transform.position - target.transform.position;
        main.transform.up = dir;
    }
}
