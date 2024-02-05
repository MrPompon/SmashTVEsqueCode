
using InAudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SO_WeaponData weaponData;

    public System.Action OnFire;
    public System.Action OnReloadStarted;
    public System.Action<RaycastHit2D> OnHitEnemy;
    public System.Action<RaycastHit2D> OnHitObstacle;
    public System.Action OnClipEmpty;
    public System.Action<Vector2> OnMiss;

    public LayerMask hitsLayer;
    public LayerMask obstacleLayer;
    public LayerMask enemiesLayer;

    protected bool usingDelayedFire = false;
    protected bool usingDelayedHitScan = false;
    protected virtual void Fire()
    {
        OnFire?.Invoke();
        if (weaponData.hitScanDelay <= 0)
        {
            for (int i = 0; i < weaponData.shotsPerAmmo; i++)
            {
                //RayForHits(transform.position, -transform.up, Random.Range(weaponData.minRange, weaponData.maxRange));
                    RayForHits(transform.position, CalculateSpread(WeaponForward(), weaponData.spread), Random.Range(weaponData.minRange, weaponData.maxRange));
            }
        }
        else
        {
            usingDelayedHitScan = true;
            StartCoroutine(RayForHits(Random.Range(weaponData.minRange, weaponData.maxRange), weaponData.hitScanDelay));
        }
    }
    protected void DelayedFire()
    {
        Fire();
        usingDelayedFire = false;
    }

    private IEnumerator RayForHits(float distance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 origin =transform.position;
        Vector3 dir = -transform.up;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, hitsLayer);

        if (hit.collider != null)
        {
            if ((enemiesLayer & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
            {
                HitEnemy(hit);
            }
            else if ((obstacleLayer & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
            {
                HitObstacle(hit);
            }
        }
        else
        {
            Miss(origin + dir * distance);
        }
        usingDelayedHitScan = false;
    }
    private void RayForHits(Vector3 origin, Vector3 dir, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, hitsLayer);

        if (hit.collider != null)
        {
        if((enemiesLayer & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
        {
            HitEnemy(hit);
        }
        else if ((obstacleLayer & 1 << hit.transform.gameObject.layer) == 1 << hit.transform.gameObject.layer)
        {
            HitObstacle(hit);
        }
        }
        else
        {
            Miss(origin + dir * distance);
        }
    }
    protected virtual void HitEnemy(RaycastHit2D hitEnemy)
    {
        OnHitEnemy?.Invoke(hitEnemy);
    }
    protected virtual void HitObstacle(RaycastHit2D hitObstacle)
    {
        OnHitObstacle?.Invoke(hitObstacle);
    }
    protected virtual void Miss(Vector2 point)
    {
        OnMiss?.Invoke(point);
    }
    protected Vector2 WeaponForward()
    {
        return -transform.up;
    }
    private Vector2 CalculateSpread(Vector2 forward,float spreadDeviation)
    {
        if (spreadDeviation == 0)
        {
            return forward;
        }

        return forward + new Vector2(Random.Range(-spreadDeviation, spreadDeviation), Random.Range(-spreadDeviation, spreadDeviation));
    }
 
}
