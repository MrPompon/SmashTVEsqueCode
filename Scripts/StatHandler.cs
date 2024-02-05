using InAudioSystem.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public SO_BaseStats baseStats;

    public int CurrentHP { get { return currentHp; } }
    private int currentHp;
    private bool isAlive=true;

    //public System.Action<int> OnDamageTaken;
    public System.Action<Vector2, int> OnDamageTakenPoint;
    public System.Action OnDeath;
    public void Awake()
    {
        currentHp = baseStats.HP;
    }
    public void ResetHP()
    {
        currentHp = baseStats.HP;
    }
    public void Heal(int health)
    {
        currentHp += health;
    }
    public void Damage(Vector2 point,int damage)
    {
        if (isAlive)
        {
            currentHp -= damage;
            OnDamageTakenPoint?.Invoke(point,damage);

            DeathCheck();

        }
    }
    //public void Damage(int damage)
    //{
    //    currentHp -= damage;
    //    OnDamageTaken(damage);
    //    DeathCheck();
    //}
    
    private void DeathCheck()
    {
        if (currentHp <= 0)
        {
            Death();
        }
    }
    protected virtual void Death()
    {
        isAlive = false;
        OnDeath?.Invoke();
        if (this.transform.parent != null)
        {
            Destroy(this.transform.parent.gameObject); //DANGEROUS. DO FOR NOW

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
