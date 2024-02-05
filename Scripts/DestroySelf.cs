using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destructionDelay;
    public StatHandler statHandler;

    private void OnValidate()
    {
        if (statHandler == null)
        {
            statHandler = GetComponent<StatHandler>();
        }
    }
    public void Awake()
    {
        if (statHandler != null)
        {
            Invoke("StatHandlerDamageDestroy", destructionDelay); 
        }
        else
        {
            Destroy(this.gameObject, destructionDelay);
        }
    }
    void StatHandlerDamageDestroy()
    {
        statHandler.Damage(statHandler.transform.position, 999999);
    }
}
