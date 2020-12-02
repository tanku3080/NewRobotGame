using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    enum NowScene
    {
        Title,Game,Result
    }
    NowScene now = NowScene.Title;
    [SerializeField] GameObject fadeObj = null;
    private void Start()
    {
        Instantiate(fadeObj);
    }

    void Update()
    {
        switch (now)
        {
            case NowScene.Title:
                break;
            case NowScene.Game:
                break;
            case NowScene.Result:
                break;
        }
    }
}
