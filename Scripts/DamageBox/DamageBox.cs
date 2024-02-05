using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public bool destroySelfOnHit = false;
    public bool disableCollisionOnDamageDealt = true;
    public Transform mainParent;

    public StatHandler stats;

    public LayerMask affectedLayer;
    public Collider2D colli;
    public bool startValue = false;

    private void OnValidate()
    {
        if (colli == null)
        {
            colli = GetComponentInChildren<Collider2D>();
        }
        if (stats == null)
        {
            stats = transform.parent.GetComponentInChildren<StatHandler>();
        }
    }
    private void Awake()
    {
        SetEnabled(startValue);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((affectedLayer & 1 << collision.transform.gameObject.layer) == 1 << collision.transform.gameObject.layer)
        {
           StatHandler statHandler = collision.gameObject.GetComponentInChildren<StatHandler>();
            if (statHandler != null)
            {
                if (disableCollisionOnDamageDealt)
                {
                    colli.enabled = false;
                }
                Damage(statHandler);
                if (this.stats.baseStats.knockBackForce != 0)
                {
                    ApplyKnockBack(statHandler);
                }
            }
            if (destroySelfOnHit)
            {
                if (this.stats != null)
                {
                    this.stats.Damage(this.stats.transform.position, 9999999);
                }
                else
                {
                    if (mainParent != null)
                    {
                        Destroy(mainParent.gameObject);
                    }
                    else
                    {
                        Destroy(this.transform.parent.gameObject);// super unsafe.
                    }

                }

            }
        }
    }
    /// <summary>
    /// will not work if object has multiple trigger colliders...
    /// </summary>
    /// <param name="value"></param>
    public void SetEnabled(bool value, float enabledDuration = 9998f)
    {
        colli.enabled = value;
        if (enabledDuration != 9998f)
        {
            Invoke("DisableHitBox", enabledDuration);
        }
    }
    private void DisableHitBox()
    {
        SetEnabled(false);
    }
    public void Damage(StatHandler target)
    {
        target.Damage(this.transform.position, this.stats.baseStats.damage);
    }
    public void ApplyKnockBack(StatHandler target)
    {
        StatHandler statHandler = target.transform.gameObject.GetComponentInChildren<StatHandler>();
        if (statHandler != null)
        {
            Rigidbody2D rigidbody2D = statHandler.GetComponentInParent<Rigidbody2D>();
            if (rigidbody2D != null)
            {

                    rigidbody2D.AddForce((-transform.up).normalized * this.stats.baseStats.knockBackForce, ForceMode2D.Impulse);
            }
        }
    }
}
