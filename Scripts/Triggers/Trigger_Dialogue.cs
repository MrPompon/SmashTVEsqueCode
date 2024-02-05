using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger_Dialogue : Trigger
{
    public TextActivator textActivator;
    private void OnValidate()
    {
        if (textActivator == null)
        {
            textActivator = GetComponent<TextActivator>();
        }

    }

    protected override void Triggered(Collider2D collision)
    {
        base.Triggered(collision);
        if (textActivator != null)
        {
            textActivator.ActivateDialogue();
        }
    }
}
