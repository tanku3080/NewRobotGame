using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [Tooltip("味方の獲得ポイント")]
    public int SidePoint;
    [Tooltip("敵の獲得ポイント")]
    public int enemyPoint;
    [Tooltip("獲得ポイントの上限")]
    public int limitPoint;
    public GameObject score_object = null;
    public int score_num = 0;
    PlayerController player;
    void Start()
    {
        //ここにplayerControllerから参照した得点を取得する
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        //もし、どちらかの得点がlimitPointと同じかそれ以下ならPointMGを呼びだす
        if (limitPoint <= SidePoint || limitPoint <= enemyPoint) 
        {
            PointMG();
        }
        Text score_text = score_object.GetComponent<Text>();
        score_text.text = "Score : " + score_num;
    }
    /// <summary>ポイントを加算する処理を書く</summary>
    void PointMG()
    {
        //ここの処理を書くならプレイヤーコントローラーを参照する。
        //仮にプレイヤーコントローラーに書かれてなかったらテキトウな値をポイントとして加算するようにする
        
    }
}
