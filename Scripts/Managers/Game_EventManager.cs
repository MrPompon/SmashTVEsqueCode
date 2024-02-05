using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_EventManager : MonoBehaviour
{
    public static System.Action<Event_Completion> OnMapEventCompleted;

    public enum Event_Completion
    {
        KEYCARD,
        COMPLETEMAP,
    }
    public static void CompleteEvent(Event_Completion type)
    {
        OnMapEventCompleted?.Invoke(type);
    }
}