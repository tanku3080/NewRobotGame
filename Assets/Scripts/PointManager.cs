using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [Tooltip("味方の獲得ポイント")]
    public int SidePoint;
    [Tooltip("敵の獲得ポイント")]
    public int enemyPoint;
    [Tooltip("獲得ポイントの上限")]
    public int limitPoint;
    PlayerController player;
    void Start()
    {
        //ここにplayerControllerから参照した得点を取得する
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        //もし、どちらかの得点がlimitPointと同じかそれ以下ならPointMGを呼びだす
        int limitPoint = 1;
        if (limitPoint <= SidePoint || limitPoint <= enemyPoint) 
        {
            PointMG();
        }
    }
    /// <summary>ポイントを加算する処理を書く</summary>
    void PointMG()
    {
        //ここの処理を書くならプレイヤーコントローラーを参照する。
        //仮にプレイヤーコントローラーに書かれてなかったらテキトウな値をポイントとして加算するようにする
        
    }
}
