using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCom : MonoBehaviour
{
    public GameObject player;
    public Text can;
    [Tooltip("プレイヤーとの距離を測る")]
    public float playerdis = 100f;
    void Start()
    {
        can = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(this.gameObject.transform.position,player.transform.position);
        if (dis > playerdis) can.enabled = true;
    }
}
