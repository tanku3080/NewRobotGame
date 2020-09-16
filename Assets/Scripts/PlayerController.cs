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
    float myLife = 200;
    public int point;
    public int maxLife = 200;
    public float Speed = 8f,jump = 5f,jumpPower = 5f;
    [Tooltip("マシンガンのマズル")]
    public Transform MGmuzzle;
    [Tooltip("ミサイルランチャーのマズル")]
    public Transform MRmuzzle;
    [Tooltip("レーザーのマズル")]
    public Transform Pulsemuzzle;
    public float _damage;
    public int RayDistance = 100;
    [Tooltip("体力の同期間隔")]
    public float lifeInterval = 1f;
    //接地判定の際、中心(Pivot)からどれくらいの距離を「接地している」と判定するかの長さ
    [SerializeField] float m_isGroundedLength = 1.1f;
    float timer;
    bool panch = false;
    //マウス操作の項目
    int keep;
    float mouse;
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
        photonView = GetComponent<PhotonView>();

        m_hpBar.maxValue = maxLife;
    }

    void Update()
    {
        //if (!photonView.IsMine) return;
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        Move();
        transform.Rotate(0, h * Speed, 0);

        mouse = Input.GetAxis("Mouse ScrollWheel") * 10;
        MouseCon();

        timer += Time.deltaTime;

        if (timer > lifeInterval)
        {
            //hp処理を書く
            m_hpBar.value = myLife;
        }

        if (Input.GetButtonDown("Fire1"))
        {

        }
        else if(Input.GetButtonDown("Fire1") && IsGrounded())//格闘
        {
            animator.SetTrigger("panch");
            panch = true;

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

    void Move()
    {
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
    }
    void MouseCon()
    {
        keep += (int)list2;

        if (keep >= (int)WeponList.NULL) keep = 0;

        if (mouse > 0)
        {
            keep += (int)mouse;
            Debug.Log("マウス前進" + keep);
        }
        if (mouse < 0)
        {
            keep -= (int)mouse;
            Debug.Log("マウス後退" + keep);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                Damage(PhotonNetwork.LocalPlayer.ActorNumber,_damage);
            }
        }
    }

    void Damage(int playerId,float damage)
    {
        myLife -= damage;
        HpRefresh();
        if(panch == true)
        {

        }
    }

    void HpRefresh()
    {
        if (m_hpBar)
        {
            m_hpBar.value -= _damage;
        }
    }

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
            case WeponList._MG://マシンガン
                ray = new Ray(MGmuzzle.position, MGmuzzle.forward);
                _damage = 10f;
                break;
            case WeponList._MR://ミサイルランチャー
                ray = new Ray(MRmuzzle.position, MRmuzzle.forward);
                _damage = 50f;
                break;
            case WeponList._Beat://格闘
                _damage = 80f;
                break;
            case WeponList._Pulse://レーザー
                ray = new Ray(Pulsemuzzle.position,Pulsemuzzle.forward);
                _damage = 15f;
                break;
        }
    }
}
