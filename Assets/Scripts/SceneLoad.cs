using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public enum SceneChenge
    {
        _Title,_Game,_Result,_Sumple
    }
    public SceneChenge _SneneChenge;
    string Scenename;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneLoader();
        }
    }

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
    }
}
