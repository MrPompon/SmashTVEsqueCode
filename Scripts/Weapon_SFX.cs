using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_SFX : MonoBehaviour
{
    public Weapon weapon;

    public InAudioNode sfxGunFire;
    public InAudioNode sfxGunHitObstacle;
    public InAudioNode sfxGunMiss;
    public InAudioNode sfxReload;
    public InAudioNode sfxAmmoEmpty;
    private void Awake()
    {
        weapon.OnFire += Fired;
        weapon.OnMiss += Missed;
        weapon.OnHitObstacle += HitObstacle;
        weapon.OnReloadStarted += ReloadStarted ;
        weapon.OnClipEmpty += ClipEmpty;
    }
    void ClipEmpty()
    {
        if (sfxAmmoEmpty != null)
        {
            InAudio.PlayAtPosition(this.gameObject, sfxAmmoEmpty, this.transform.position);
        }
    }
    void Fired()
    {
        if (sfxGunFire != null)
        {
            InAudio.PlayAtPosition(this.gameObject, sfxGunFire, this.transform.position);
        }
    }
    void ReloadStarted()
    {
        if (sfxReload != null)
        {
            InAudio.PlayAtPosition(this.gameObject, sfxReload, this.transform.position);
        }
    }
    void Missed(Vector2 pos)
    {
        if (sfxGunMiss != null)
        {
            InAudio.PlayAtPosition(this.gameObject, sfxGunMiss, pos);
        }
    }
    void HitObstacle(RaycastHit2D hit)
    {
        if (sfxGunMiss!=null)
        {
            InAudio.PlayAtPosition(this.gameObject, sfxGunMiss, hit.collider.transform.position);
        }
    }
}
