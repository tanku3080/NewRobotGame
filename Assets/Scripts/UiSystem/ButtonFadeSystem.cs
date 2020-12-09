using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFadeSystem : MonoBehaviour
{
    //未完成
    Color img;
    [SerializeField] float spd = 1f;
    float timer;
    float alfa;
    private void Start()
    {
        img = this.gameObject.GetComponent<Image>().color;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * spd * 0.5f + 0.5f;
        img.a = Mathf.Sin(timer);
    }
}
