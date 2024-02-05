using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DisableNActivateObjects : MonoBehaviour
{
    public List<GameObject> objs;

    public Trigger trigger;
    public bool activeValue = false;

    private void Awake()
    {
        trigger.OnTriggered += Do;
    }
    private void OnDestroy()
    {
        trigger.OnTriggered -= Do;
    }
    void Do()
    {
        if(objs!=null && objs.Count > 0)
        {
            foreach(GameObject obj in objs)
            {
                if(obj!=null)
                obj.gameObject.SetActive(activeValue);
            }
        }
    }
}
