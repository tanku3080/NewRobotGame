using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    enum SceneChenge
    {
        _Title,_Game,_Result,_Sumple
    }
    SceneChenge _SneneChenge;
    string Scenename;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SceneLoader()
    {
        SceneManager.LoadScene(SneneSet());
    }

    string SneneSet()
    {
        SceneChenge chenge;
        chenge = _SneneChenge;
        switch (chenge)
        {
            case SceneChenge._Title:
                Scenename = "Title";
                break;
            case SceneChenge._Game:
                Scenename = "Game";
                break;
            case SceneChenge._Result:
                Scenename = "Result";
                break;
            case SceneChenge._Sumple:
                Scenename = "SampleScene";
                break;
        }
        return SneneSet();
    }
}
