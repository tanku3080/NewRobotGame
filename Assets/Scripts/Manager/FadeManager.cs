using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    public enum Mode
    {
        No,In,Out,
    }
    public Mode mode = Mode.No;
    [SerializeField] float fadeSPD = 2f;
    float fadeTime = 0f;
    [SerializeField] GameObject thisObj = null;
    CanvasGroup group = null;
  
    private void Start()
    {
        group = thisObj.GetComponent<CanvasGroup>();
        group.alpha = 1;
    }

    private void Update()
    {
        switch (mode)
        {
            case Mode.In:
                FadeIn();
                break;
            case Mode.Out:
                FadeOut();
                break;
        }
    }

    public void FadeIn(float time = 1f)
    {
        Instance.fadeTime = time;
        while (Instance.group.alpha != Instance.fadeTime)
        {
            Instance.group.alpha += Time.deltaTime;
        }
        if (Instance.group.alpha == 1)
        {
            mode = Mode.No;
            return;
        }
    }

    public void FadeOut(float time = 0)
    {
        Instance.fadeTime = time;
        while (Instance.group.alpha != Instance.fadeTime)
        {
            Instance.group.alpha -= Time.deltaTime;
        }
        if (Instance.group.alpha == 0)
        {
            mode = Mode.No;
            return;
        }
    }
}
