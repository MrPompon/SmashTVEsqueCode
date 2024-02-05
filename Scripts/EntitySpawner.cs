using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public bool autoSpawn = true;
    public float cooldown = 16f;

    public List<GameObject> spawns;

    public int min, max;
    public float spawnRange = 0.016f;

    public float minRangeToEnemySpawn = 3f;
    //public SO_BaseStats baseStats;

    private GameObject target;

    private float progress = 0;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (!autoSpawn)
        {
            return;
        }
        if (target != null)
        {
            if(Vector2.Distance(this.transform.position, target.transform.position) > minRangeToEnemySpawn)
            {
                return;
            }
        }
        progress += Time.deltaTime;
        if (progress > cooldown)
        {
            progress = 0;
            Spawn();
        }
    }
    public void Spawn()
    {
        if (spawns == null || spawns.Count <= 0)
        {
            return;
        }

        int rndSpawn = Random.Range(min, max);
        for (int i = 0; i < rndSpawn; i++)
        {
            GameObject spawn = spawns[Random.Range(0, spawns.Count)];
            GameObject newSpawn = Instantiate(spawn as GameObject, RandomOffset(this.transform.position, spawnRange), Quaternion.identity, null);
        }
    }
    public Vector2 RandomOffset(Vector2 origin, float range)
    {
        return new Vector2(origin.x + Random.Range(-range, range), origin.y + Random.Range(-range, range));
    }
}
