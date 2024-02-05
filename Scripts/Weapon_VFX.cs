using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_VFX : MonoBehaviour
{
    public Weapon weapon;
    public GameObject fireVFX;
    public GameObject bulletHoleVFX;
    public List<GameObject> bulletHitSolidVFX;
    public bool vfxOnMiss = true;
    private void Awake()
    {
        weapon.OnFire += Fired;
        weapon.OnMiss += Missed;
        weapon.OnHitObstacle += HitObstacle;
    }
    private void Fired()
    {
        if (fireVFX != null)
        {
            GameObject newVFX= Instantiate(fireVFX as GameObject, weapon.transform.position, Quaternion.identity, null);

        }
    }
    private void HitObstacle(RaycastHit2D hit)
    {
        Vector2 spawnPoint = hit.point;
        //if (hit.collider != null)
        //{
        //    spawnPoint = RandomPointInBounds(hit.collider.bounds);
        //}

        if (bulletHitSolidVFX.Count > 0)
        {
            int rnd = Random.Range(0, bulletHitSolidVFX.Count);
            GameObject vfx = bulletHitSolidVFX[rnd];
            GameObject newVFX = Instantiate(vfx as GameObject, spawnPoint, Quaternion.identity,null); //unsafe.
        }
    }
    private void Missed(Vector2 hitPos)
    {
        if (vfxOnMiss != true)
            return; 

        if (bulletHoleVFX != null)
        {
            GameObject newVFX = Instantiate(bulletHoleVFX as GameObject, hitPos, Quaternion.identity, null);
        }
        if (bulletHitSolidVFX != null && bulletHitSolidVFX.Count>0)
        {
            GameObject rndObj = bulletHitSolidVFX[Random.Range(0, bulletHitSolidVFX.Count)];
            GameObject newVFX = Instantiate(rndObj as GameObject, hitPos, Quaternion.identity, null);
        }
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
