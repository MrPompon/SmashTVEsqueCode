using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_DayNNightManager : MonoBehaviour
{
    public FunkyCode.LightCycle lightCycle;
    public void Update()
    {
        lightCycle.SetTime(lightCycle.time += Time.deltaTime * 0.01f);
    }
}
