using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    enum AnimeList
    {
        _wait,moveF,moveB,jump,die
    }
    enum WeponList:int//マシンガン、ミサイル、パンチ、レーザー
    {
        _MG,_MR,_Beat,_Pulse,NULL
    }
    private readonly AnimeList list1;
    private readonly WeponList list2;
    int myLife = 200;
    public int point;
    public int maxLife = 200;
    public float Speed = 8f,jump = 5f,jumpPower = 5f;
    [Tooltip("マシンガンのマズル")]
    public Transform MGmuzzle;
    [Tooltip("ミサイルランチャーのマズル")]
    public Transform MRmuzzle;
    [Tooltip("レーザーのマズル")]
    public Transform Pulsemuzzle;
    public int _damage;
    public int RayDistance = 100;
    [Tooltip("体力の同期間隔")]
    public float lifeInterval = 1f;
    //接地判定の際、中心(Pivot)からどれくらいの距離を「接地している」と判定するかの長さ
    [SerializeField] float m_isGroundedLength = 1.1f;
    float timer;
    bool panch = false;
    //移動関係の定数
    float v, h;
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
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        Move();
        transform.Rotate(0, h * Speed, 0);

        mouse = Input.GetAxis("Mouse ScrollWheel") * 10;
        weponSet(Mouse());
        //MouseCon();

        timer += Time.deltaTime;

        if (timer > lifeInterval)
        {
            //hp処理を書く
            m_hpBar.value = myLife;
        }

        if (Input.GetButtonDown("Fire1"))//格闘以外の攻撃
        {
            //アニメーターのパラメーターを設定する
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
        if (v > 0)
        {
            animeSet(AnimeList.moveF);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
        else if (v < 0)
        {
            animeSet(AnimeList.moveB);
            transform.position -= transform.forward * Speed * Time.deltaTime;
        }
        if (h > 0)
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
        else if (h < 0)
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
    }
    int Mouse()
    {
        keep += (int)list2;
        if (keep >= (int)WeponList.NULL) keep = 0;
        if (mouse > 0)
        {
            keep += (int)mouse;
        }
        if (mouse < 0)
        {
            keep -= (int)mouse;
        }
        return keep;
    }

    ///// <summary>
    ///// この処理はネットワークに繋げたら検証する
    ///// </summary>
    ///// <param name="collision"></param>
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (photonView.IsMine)
    //    {
    //        if (collision.gameObject.GetComponent<PlayerController>())
    //        {
    //            Damage(PhotonNetwork.LocalPlayer.ActorNumber,_damage);
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        //パンチを受けた場合
        if (other.gameObject.tag == "Enemy" && panch == true)
        {
            PlayerController enemy = GetComponent<PlayerController>();
            enemy.Damage(PhotonNetwork.LocalPlayer.ActorNumber,_damage);
            panch = false;
        }
    }

    void Damage(int playerId,int damage)
    {
        myLife -= damage;
        HpRefresh();
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

    void weponSet(int _keep)
    {
        WeponList wepon;
        wepon = (WeponList)_keep;
        switch (wepon)
        {
            case WeponList._MG://マシンガン
                //ray = new Ray(MGmuzzle.position, MGmuzzle.forward);
                _damage = 10;
                break;
            case WeponList._MR://ミサイルランチャー
               //ray = new Ray(MRmuzzle.position, MRmuzzle.forward);
                _damage = 50;
                break;
            case WeponList._Beat://格闘
                _damage = 80;
                break;
            case WeponList._Pulse://レーザー
                //ray = new Ray(Pulsemuzzle.position,Pulsemuzzle.forward);
                _damage = 15;
                break;
        }
    }
}
