using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatHandler))]
public class Stat_Handler_Component : MonoBehaviour
{
    public StatHandler statHandler;
    private void OnValidate()
    {
        if (statHandler == null)
        {
            statHandler = GetComponent<StatHandler>();
        }
    }
}
