using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    void Start()
    {
        if (gameManager != null)
        {
            Destroy(this.gameObject);
        }
        gameManager = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
