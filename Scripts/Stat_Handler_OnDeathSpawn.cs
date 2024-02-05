using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatHandler))]
public class Stat_Handler_OnDeathSpawn : Stat_Handler_Component
{
    public List<GameObject> spawns;

    public int min, max;
    public float spawnRange = 0.016f;
    private void Awake()
    {
        statHandler.OnDeath += OnDeath;
    }
    public void OnDeath()
    {
        if(spawns==null || spawns.Count <= 0)
        {
            return;
        }

        int rndSpawn = Random.Range(min, max);
        for (int i = 0; i < rndSpawn; i++)
        {
            GameObject spawn = spawns[Random.Range(0, spawns.Count)];
            GameObject newSpawn = Instantiate(spawn as GameObject, RandomOffset(this.transform.position,spawnRange), Quaternion.identity, null);
        }
    }
    public Vector2 RandomOffset(Vector2 origin, float range)
    {
        return new Vector2(origin.x + Random.Range(-range, range), origin.y + Random.Range(-range, range));
    }
}
