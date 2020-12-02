using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : Singleton<SceneLoad>
{
    public enum SceneChenge
    {
        _Title,_Game,_Result,_Sumple
    }
    public SceneChenge _SneneChenge;
    private string Scenename;
    public bool sceneChangeFlag = false;

    public void SceneLoader()
    {
        SneneSet();
        SceneManager.LoadScene(Scenename);
    }

    void SneneSet()
    {
        SceneChenge chenge = _SneneChenge;
        switch (chenge)
        {
            case SceneChenge._Title:
                Scenename = "Title";
                sceneChangeFlag = true;
                break;
            case SceneChenge._Game:
                Scenename = "Game";
                sceneChangeFlag = true;
                break;
            case SceneChenge._Result:
                Scenename = "Result";
                sceneChangeFlag = true;
                break;
            case SceneChenge._Sumple:
                Scenename = "SampleScene";
                //sceneChangeFlag = true;
                break;
        }
    }
}
