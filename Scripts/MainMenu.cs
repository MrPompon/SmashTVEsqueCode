using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform lightMouseFollow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lightMouseFollow!=null)
        lightMouseFollow.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
