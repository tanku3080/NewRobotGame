using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    enum AnimeList
    {
        _wait,moveF,moveB,jump,die
    }
    enum WeponList:int
    {
        _MG,_MR,_Beat,_Pulse,NULL
    }
    AnimeList list1;
    WeponList list2;
    int myLife = 20;
    public int maxLife = 20;
    public float Speed = 2f,jump = 5f;
    [Tooltip("マズル")]
    public Transform _muzzleGun;
    public int RayDistance = 100;
    [Tooltip("体力の同期間隔")]
    public float lifeInterval = 1f;
    float timer;
    RaycastHit hit;
    Ray ray;
    Rigidbody rd;
    Animator animator;
    PhotonView photonView;
    private void Start()
    {
        rd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeInterval)
        {
            myLife = maxLife;
            //hp処理を書く
        }
        float h = Input.GetAxis("Horizontal") * Speed;
        float v = Input.GetAxis("Vertical") * Speed;
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        int keep = (int)list2;

        if (mouse >= (int)WeponList.NULL) mouse = 0;

        if (mouse > 0) 
        {
            keep++;
            Debug.Log("マウス前進");
        }
        if (mouse < 0)
        {
            keep--;
            Debug.Log("マウス後退");
        }

        if (v > 0) animeSet(AnimeList.moveF);
        else if (v < 0) animeSet(AnimeList.moveB);

        transform.position = transform.position + new Vector3(h * timer, 0, v * timer);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = new Ray(_muzzleGun.position, _muzzleGun.forward);

            if (Physics.Raycast(ray,out hit,RayDistance))
            {
                if (hit.collider.tag != "Pbject")
                {
                    //Damage()
                }
            }
        }
    }

    //void Damage(int playerID,int damage)
    //{
    //    myLife -= damage;
    //    //hpの関数を作って呼び出す
    //    object[] parameter = new object[] { myLife };
    //}

    void animeSet(AnimeList anime)
    {
        anime = list1;
        switch (anime)
        {
            case AnimeList._wait:
                break;
            case AnimeList.moveF:
                break;
            case AnimeList.moveB:
                break;
            case AnimeList.jump:
                break;
            case AnimeList.die:
                break;
        }
    }

    void weponSet(WeponList wepon)
    {
        wepon = list2;
        switch (wepon)
        {
            case WeponList._MG:
                break;
            case WeponList._MR:
                break;
            case WeponList._Beat:
                break;
            case WeponList._Pulse:
                break;
        }
    }
}
