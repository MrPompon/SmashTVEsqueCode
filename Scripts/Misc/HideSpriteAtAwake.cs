using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpriteAtAwake : MonoBehaviour
{
    public SpriteRenderer renderer;
    private void OnValidate()
    {
        if (renderer == null)
        {
            renderer = GetComponent<SpriteRenderer>();
        }
    }
    public void Awake()
    {
        renderer.enabled = false;
    }
}
