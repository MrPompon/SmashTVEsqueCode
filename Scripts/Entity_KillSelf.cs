using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_KillSelf : MonoBehaviour
{
    public StatHandler statHandler;
    public int minDelay=5, maxDelay=15;
    // Start is called before the first frame update
    void Start()
    {
        int rndDelay = Random.Range(minDelay, maxDelay);
        Invoke("Soduku", rndDelay); 
    }
    void Soduku()
    {
        statHandler.Damage(statHandler.transform.position, 99999);
    }
}
