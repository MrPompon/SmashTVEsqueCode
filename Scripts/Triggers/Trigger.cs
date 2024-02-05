using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public LayerMask triggeredLayer;
    public bool removeOnTrigger = true;
    public System.Action OnTriggered;
    public StatHandler statHandler;
    private void OnValidate()
    {
        if (statHandler == null)
        {
            statHandler = GetComponentInChildren<StatHandler>();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((triggeredLayer & 1 << collision.transform.gameObject.layer) == 1 << collision.transform.gameObject.layer)
        {
            Triggered(collision);
        }
    }
    /// <summary>
    /// Call base after overriden functionallity, since it destroys self.
    /// </summary>
    protected virtual void Triggered(Collider2D collision)
    {
        OnTriggered?.Invoke();
        if (removeOnTrigger)
        {
            if (statHandler != null)
            {
                statHandler.Damage(this.transform.position, 9999999);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

}
