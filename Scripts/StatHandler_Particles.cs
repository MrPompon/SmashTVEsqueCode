using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler_Particles : Stat_Handler_Component
{
    public List<GameObject> bulletHoleVFX;
    public GameObject bulletHitVFX;
    public GameObject deadVFX;
    public GameObject deathExplosionVFX;
    public Collider2D colli;
    public List<GameObject> gorePieces;
    public int minGorePieces = 2;
    public int maxGorePieces = 4;
    public GameObject bloodFountain;
    public GameObject bloodHitSpray;

    public void Awake()
    {
        statHandler.OnDamageTakenPoint += DamageTaken;
        statHandler.OnDeath += OnDeath;
    }
    void DamageTaken(Vector2 point, int damage)
    {
        if (damage <= 0)
            return;

        Vector2 spawnPoint = point;
        if (colli != null)
        {
            spawnPoint = RandomPointInBounds(colli.bounds);
        }

        if (bulletHoleVFX.Count > 0)
        {
            int rnd = Random.Range(0, bulletHoleVFX.Count);
            GameObject vfx = bulletHoleVFX[rnd];
            GameObject newVFX = Instantiate(vfx as GameObject, spawnPoint, Quaternion.identity, transform.parent);
        }
        if (bulletHitVFX != null)
        {
            if (damage == 0)
                return;
            GameObject newVFX = Instantiate(bulletHitVFX as GameObject, spawnPoint, Quaternion.identity, transform.parent);
        }
        if(bloodHitSpray != null)
        {
            if (damage == 0)
                return;
            GameObject newVFX = Instantiate(bloodHitSpray as GameObject, spawnPoint, Quaternion.identity, transform.parent);
        }
        int rndGore = Random.Range(0, 100);
        if (rndGore > 90)
            SpawnGorePiece(1, 1);
    }
    void OnDeath()
    {
        if (deadVFX != null)
        {
            GameObject newVFX = Instantiate(deadVFX as GameObject, transform.parent.position, transform.parent.rotation, null); 
        }
        if (deathExplosionVFX != null)
        {
            GameObject newVFX = Instantiate(deathExplosionVFX as GameObject, transform.parent.position, transform.parent.rotation, null);
        }
        SpawnGorePiece(minGorePieces, maxGorePieces);
        SpawnBloodFountain();
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    void SpawnGorePiece(int min, int max)
    {
        if (gorePieces != null)
        {
            if (gorePieces.Count > 0)
            {
                int count = Random.Range(min, max);
                for (int i = 0; i < count; i++)
                {
                    int rnd = Random.Range(0, gorePieces.Count);
                    GameObject gore = gorePieces[rnd];
                    GameObject newGore = Instantiate(gore as GameObject, transform.parent.position, transform.parent.rotation, null);

                    if (newGore != null)
                    {
                        if (newGore.GetComponent<Rigidbody2D>() != null)
                        {
                            Rigidbody2D rigid;
                            rigid = newGore.GetComponent<Rigidbody2D>();
                            if (rigid != null)
                            {
                                rigid.AddForce(Random.insideUnitCircle * Random.Range(0.3f, 0.7f), ForceMode2D.Impulse);
                                rigid.AddTorque(Random.Range(-15, 15));
                            }
                        }
                    }
                }

            }
        }
    }
    void SpawnBloodFountain()
    {
        if (bloodFountain != null)
        {
            GameObject newGore = Instantiate(bloodFountain as GameObject, transform.parent.position, transform.parent.rotation, null);
        }
    }
}
