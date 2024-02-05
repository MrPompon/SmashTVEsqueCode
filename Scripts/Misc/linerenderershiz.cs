using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linerenderershiz : MonoBehaviour
{
    public Transform target;
    public LineRenderer renderer;
    

    void LateUpdate()
    {
        renderer.SetPosition(0, target.position);
        renderer.SetPosition(1, transform.position);
    }
}
