using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Game_EventManager.Event_Completion eventCompletion;

    protected virtual void Awake()
    {
        Game_EventManager.OnMapEventCompleted += OnGoalCompleted;
    }
    protected virtual void OnDestroy()
    {
        Game_EventManager.OnMapEventCompleted -= OnGoalCompleted;
    }
    protected virtual void EventTriggered()
    {

    }
    private void OnGoalCompleted(Game_EventManager.Event_Completion eventCompletion)
    {
        if (this.eventCompletion == eventCompletion)
        {
            EventTriggered();
        }
    }
    //void OnGUI()
    //{
    //    var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    //    var textSize = GUI.skin.label.CalcSize(new GUIContent(eventCompletion.ToString()));
    //    GUI.Label(new Rect(position.x, Screen.height - position.y, textSize.x, textSize.y), eventCompletion.ToString());
    //}
}
