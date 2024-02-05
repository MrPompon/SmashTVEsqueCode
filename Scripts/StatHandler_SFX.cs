using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler_SFX : Stat_Handler_Component
{
    public InAudioNode damageTakenSound;
    public InAudioNode isHitSound;//not really implemented but damage taken needs damage"type"....
    public InAudioNode deathSound;
    public void Awake()
    {
        statHandler.OnDamageTakenPoint += DamageTaken;
        statHandler.OnDeath += Death;
    }
    public void Death()
    {
        if (deathSound != null)
        {
            InAudio.PlayPersistent(this.transform.position, deathSound);
        }
    }
    public void DamageTaken(Vector2 point, int damage)
    {
        if (damageTakenSound != null)
        {
            InAudio.Play(this.gameObject, damageTakenSound);
        }
        if (isHitSound != null)
        {
            InAudio.PlayAtPosition(this.gameObject,isHitSound, point);
        }
    }
}
