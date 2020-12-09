using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject titleButtonObj = null;
    [SerializeField] GameObject fadeObj = null;
    Vector2 startButtonPos = Vector2.zero;
    private void Start()
    {
        Instantiate(fadeObj);
        if (SceneManager.GetActiveScene().name == "Title")
        {

        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {

        }
        else if (SceneManager.GetActiveScene().name == "Result")
        {

        }
    }

    void Update()
    {
    }

    

    public void StartSceneButton_Start()
    {

    }
    void StartSceneButton_Other()
    {

    }
}
