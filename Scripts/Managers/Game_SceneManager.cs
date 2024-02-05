using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_SceneManager : MonoBehaviour
{
    public void Awake()
    {
        Game_EventManager.OnMapEventCompleted += OnMapEventCompleted;
    }
    public void OnDestroy()
    {
        Game_EventManager.OnMapEventCompleted -= OnMapEventCompleted;
    }
    public void OnMapEventCompleted(Game_EventManager.Event_Completion gameEvent)
    {
        if(gameEvent== Game_EventManager.Event_Completion.COMPLETEMAP)
        {
            Invoke("LoadNextScene", 3f);
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
