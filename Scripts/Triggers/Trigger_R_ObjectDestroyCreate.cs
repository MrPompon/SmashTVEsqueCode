using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_R_ObjectDestroyCreate : MonoBehaviour
{
    public Trigger trigger;
    public GameObject toRemove;
    public GameObject prefabToCreate;

    private void Awake()
    {
        trigger.OnTriggered+=Do;
    }
    private void OnDestroy()
    {
        trigger.OnTriggered -= Do;
    }
    void Do()
    {
        Vector3 position = toRemove.transform.position;
        GameObject newSpawn = Instantiate(prefabToCreate as GameObject, position, Quaternion.identity, null);
        Destroy(toRemove);
    }

}
