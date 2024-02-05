using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : Weapon
{
    public Player_Controller player_Controller;
    public UpgradeHandler upgradeHandler;
    public string fireKey;
    private int currentShots = 0;
    private bool didReload = false;
    private bool reloading = false;
    private float heldFireButtonDuration = 0f;
    public int ClipSize { get { if (upgradeHandler != null) { return weaponData.clipSize + upgradeHandler.UpgradedClipAmmo; } else { return weaponData.clipSize; } } }
    public int CurrentShots { get { return currentShots; } }
    private void Awake()
    {
        currentShots = ClipSize;
    }
    private void OnValidate()
    {
        if (player_Controller == null)
        {
            player_Controller = transform.parent.GetComponentInChildren<Player_Controller>();
        }
    }
    private void Update()
    {
        if (reloading || usingDelayedFire || usingDelayedHitScan)
        {
            return;
        }
        bool canFire = true;

        if (Input.GetButtonUp(fireKey))
        {
            ResetHeldDuration();

            if (weaponData.requiresAmmo && currentShots <= 0)
            {
                canFire = false;
            }
            if (canFire)
            {
         
                if (!didReload)
                {
                    currentShots -= weaponData.ammoDrain;
                    player_Controller.OnAnyInput?.Invoke();
                    if (weaponData.startUpDelay <= 0)
                    {
                        Fire();
                    }
                    else
                    {
                        Invoke("DelayedFire", weaponData.startUpDelay);
                    }
                 
                }
                didReload = false;
            }
            else if(!didReload)
            {
                OnClipEmpty();
            }
        }

        if (Input.GetButton(fireKey)) //maybe make it so when reload, shoot remaining clipsize aka bootleg shotgun, needs spread. 
        {
            if (currentShots != weaponData.clipSize)
            {
                heldFireButtonDuration += Time.deltaTime;
                if (heldFireButtonDuration >= weaponData.initReloadTime)
                {
                    ReloadStarted();
                }
            }
        }
    }
    protected override void HitEnemy(RaycastHit2D hitEnemy)
    {
        base.HitEnemy(hitEnemy);
        StatHandler statHandler = hitEnemy.transform.gameObject.GetComponentInChildren<StatHandler>();
        if (statHandler != null)
        {
            statHandler.Damage(hitEnemy.point, weaponData.damage);
            Rigidbody2D rigidbody2D = statHandler.GetComponentInParent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                if(weaponData.knockBackMode == SO_WeaponData.KnockBackMode.WeaponHolderComparison)
                {
                    rigidbody2D.AddForce((hitEnemy.transform.position - transform.position).normalized * weaponData.knockbackForce, ForceMode2D.Impulse);
                }
                else if(weaponData.knockBackMode == SO_WeaponData.KnockBackMode.WeaponHolderForward)
                {
                    rigidbody2D.AddForce((-transform.up).normalized * weaponData.knockbackForce, ForceMode2D.Impulse);
                }

            }
        }
    }
    protected override void HitObstacle(RaycastHit2D hitObstacle)
    {
        base.HitObstacle(hitObstacle);
        StatHandler statHandler = hitObstacle.transform.gameObject.GetComponentInChildren<StatHandler>();
        if (statHandler != null)
        {
            statHandler.Damage(hitObstacle.point, weaponData.damage);
            Rigidbody2D rigidbody2D = statHandler.GetComponentInParent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                if (weaponData.knockBackMode == SO_WeaponData.KnockBackMode.WeaponHolderComparison)
                {
                    rigidbody2D.AddForce((hitObstacle.transform.position - transform.position).normalized * weaponData.knockbackForce, ForceMode2D.Impulse);
                }
                else if (weaponData.knockBackMode == SO_WeaponData.KnockBackMode.WeaponHolderForward)
                {
                    rigidbody2D.AddForce((-transform.up).normalized * weaponData.knockbackForce, ForceMode2D.Impulse);
                }

            }
        }
    }
    protected override void Fire()
    {
        base.Fire();
        ApplyKickBack();
        ApplyScreenShake();
    }
    private void ResetHeldDuration()
    {
        heldFireButtonDuration = 0;
    }
    private void ReloadStarted()
    {
        reloading = true;
        ResetHeldDuration();// kinda bootleg if this wants to be used elsewhere.!
        OnReloadStarted?.Invoke();
        Invoke("Reload", weaponData.reloadDuration);
    }
    private void Reload()
    {
        reloading = false;
        didReload = true;
        currentShots = ClipSize;
    }
    protected void ApplyKickBack() //might be handled elsewhere. eg controllers
    {
        if(weaponData!=null && weaponData.kickBack!=0)
        player_Controller.rigid2D.AddForce(CalculateKickBack(), ForceMode2D.Impulse);
    }
    private Vector2 CalculateKickBack()
    {

        return player_Controller.transform.up*weaponData.kickBack;
    }
    protected void ApplyScreenShake()
    {
        if (weaponData.screenShake > 0)
        {
            if (ScreenShake.ScreenShaker != null)
            {
                ScreenShake.ScreenShaker.Shake(weaponData.screenShake,0.05f,-0.1f, 0.1f);
            }
 
        }
    }
}
