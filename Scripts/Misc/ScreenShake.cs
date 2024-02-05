using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake ScreenShaker;

    public float shakeRate= 0.05f;
    Transform trans;
    private Vector3 defaultPos;
    
    private void Start()
    
    {
        if (ScreenShaker == null)
        {
            ScreenShaker = this;
        }

        trans = this.transform;
        defaultPos = trans.position;
    }

    public void Shake(float time, float decreaseFactor = 0.01f, float minMagnitude = -0.5f, float maxMagnitude = 0.5f)
    {
        StartCoroutine(ShakeRoutine(time,decreaseFactor,minMagnitude,maxMagnitude));
    }
    private IEnumerator ShakeRoutine(float time, float decreaseFactor=0.01f, float minMagnitude=-0.5f, float maxMagnitude =0.5f )
    {
        float t = time;
        float decrease = 0;
        while (t>0)
        {
            t -= shakeRate;
            decrease += decreaseFactor;
            Vector2 pos = Random.insideUnitCircle;
            trans.position = new Vector2((pos.x * Random.Range(minMagnitude, maxMagnitude)) * (1-decrease), (pos.y * Random.Range(minMagnitude, maxMagnitude))* (1-decrease));
            yield return new WaitForSeconds(shakeRate);
            
        }
        StopShake();
    }
    private void StopShake()
    {
        trans.position = defaultPos;
    }
}
