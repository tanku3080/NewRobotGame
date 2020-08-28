using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int point;
    public int maxLife = 20;
    public float Speed = 8f,jump = 5f,jumpPower = 5f;
    [Tooltip("マズル")]
    public Transform _muzzleGun;
    public int RayDistance = 100;
    [Tooltip("体力の同期間隔")]
    public float lifeInterval = 1f;
    //接地判定の際、中心(Pivot)からどれくらいの距離を「接地している」と判定するかの長さ
    [SerializeField] float m_isGroundedLength = 1.1f;
    float timer;
    RaycastHit hit;
    Ray ray;
    Rigidbody rd;
    Animator animator;
    PhotonView photonView;
    [SerializeField] Slider m_hpBar;
    [SerializeField] Button m_respawn;
    [SerializeField] Button m_backToTitle;
    //死んだときに呼んで
    //m_respawn.gameObject.SetActive(true);
    //m_backToTitle.gameObject.SetActive(true);

    private void Start()
    {
        rd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();

        m_hpBar.maxValue = maxLife;
    }
    void Update()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        timer += Time.deltaTime;

        if (timer > lifeInterval)
        {
            myLife = maxLife;
            //hp処理を書く
            m_hpBar.value = myLife;
        }
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
        transform.Rotate(0, h * Speed, 0);

        if (Input.GetKey("w"))
        {
            animeSet(AnimeList.moveF);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            animeSet(AnimeList.moveB);
            transform.position -= transform.forward * Speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = new Ray(_muzzleGun.position, _muzzleGun.forward);

            if (Physics.Raycast(ray, out hit, RayDistance))
            {
                if (hit.collider.tag != "Pbject")
                {
                    //Damage()
                }
            }
        }

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rd.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // Animator Controller のパラメータをセットする
            if (animator)
            {
                animator.SetBool("IsGrounded", false);
            }
        }
    }

    //void Damage(int playerID,int damage)
    //{
    //    myLife -= damage;
    //    //hpの関数を作って呼び出す
    //    object[] parameter = new object[] { myLife };
    //}



    //地面に衝突しているか判定する
    bool IsGrounded()
    {
        //Physics.Linecast()　を使って足元から線を張り、そこに何かが衝突していたら　true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;  // start: 体の中心
        Vector3 end = start + Vector3.down * m_isGroundedLength; //end: start　から真下の地点
        Debug.DrawLine(start, end); //動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); //引いたラインに何かがぶつかっていたら　true　とする
        return isGrounded;
    }


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
