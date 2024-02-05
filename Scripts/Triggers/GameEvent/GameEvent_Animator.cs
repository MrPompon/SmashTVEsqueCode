using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameEvent_Animator : GameEvent
{
    public Animator animator;
    public AnimatorVariable variable;
    public string variableName;
    public enum AnimatorVariable
    {
        Trigger,
    }
    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    protected override void EventTriggered()
    {
        base.EventTriggered();
        if (animator == null)
        {
            return;
        }
        switch (variable)
        {
            case AnimatorVariable.Trigger:
                animator.SetTrigger(variableName);
                break;
        }
    }
}
