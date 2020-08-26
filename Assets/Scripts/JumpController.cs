using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rigidbody を使ってプレイヤーが動かすコンポーネント
//入力を受け取り、それに従ってオブジェクトを動かす
[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    //ジャンプ力
    [SerializeField] float m_jumpPower = 5f;
    //接地判定の際、中心(Pivot)からどれくらいの距離を「接地している」と判定するかの長さ
    [SerializeField] float m_isGroundedLength = 1.1f;
    //キャラクターのAnimator
    [SerializeField] Animator m_anim;
    Rigidbody m_rb;
    Vector3 m_movingDirection = Vector3.zero;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //物理挙動はこちらで処理する

        m_rb.AddForce(m_movingDirection, ForceMode.Force);
    }
    // Update is called once per frame
    void Update()
    {
        //方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");        

        // Animator Controller のパラメータをセットする
        if (m_anim)
        {


            // 水平方向の速度を Speed にセットする
            Vector3 velocity = m_rb.velocity;
            velocity.y = 0f;
            m_anim.SetFloat("Speed", velocity.magnitude);

            // 地上/空中の状況に応じて IsGrounded をセットする
            if (m_rb.velocity.y <= 0f && IsGrounded())
            {
                m_anim.SetBool("IsGrounded", true);
            }
            else if (!IsGrounded())
            {
                m_anim.SetBool("IsGrounded", false);
            }
        }

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);

            // Animator Controller のパラメータをセットする
            if (m_anim)
            {
                m_anim.SetBool("IsGrounded", false);
            }
        }

        //地面に衝突しているか判定する
        bool IsGrounded()
        {
            //Physics.Linecast()　を使って足元から線を張り、そこに何かが衝突していたら　true とする
            CapsuleCollider col = GetComponent<CapsuleCollider>();
            Vector3 start = this.transform.position + col.center;  // start: 体の中心
            Vector3 end = start + Vector3.down * m_isGroundedLength;　//end: start　から真下の地点
            Debug.DrawLine(start, end); //動作確認用に Scene ウィンドウ上で線を表示する
            bool isGrounded = Physics.Linecast(start, end);　//引いたラインに何かがぶつかっていたら　true　とする
            return isGrounded;
        }
        
    }
}
