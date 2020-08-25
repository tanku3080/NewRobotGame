﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rigidbody を使ってプレイヤーを動かすコンポーネント
/// 入力を受け取り、それに従ってオブジェクトを動かす
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerMove : MonoBehaviour

{
    

    /// <summary>動く速さ</summary>
    [SerializeField] float m_moveSpeed = 8f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 1f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float m_jumpPower = 5f;
    /// <summary>ホバー力</summary>
    [SerializeField] float m_hoverPower = 5f;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float m_isGroundedLength = 1.1f;
    /// <summary>キャラクターの Animator</summary>
    [SerializeField] Animator m_anim;
    Rigidbody m_rb;
    PhotonView m_view;
    bool m_isHovering = false;
    Vector3 m_movingDirection = Vector3.zero;
    [SerializeField] Transform character, pivot;
    [Range(-0.999f, -0.5f)] public float maxAngle = -0.5f;
    [Range(0.5f, 0.999f)] public float minAngle = 0.5f;
    void Start()
    {
        if (character == null) character = transform.parent;
        if (pivot == null) pivot = transform;
    //    m_rb = GetComponent<Rigidbody>();
    //    m_view = GetComponent<PhotonView>();

    //    // カメラターゲットに自分を設定する
    //    if (m_view.IsMine)
    //    {
    //        CinemachineVirtualCameraBase vcam = GameObject.FindObjectOfType<CinemachineVirtualCameraBase>();
    //        vcam.Follow = transform;
    //        vcam.LookAt = transform;
    //    }
    }

    void FixedUpdate()
    {
        //物理挙動はこちらで処理する

        //m_rb.AddForce(m_movingDirection, ForceMode.Force);

        //if (m_isHovering)
        //{
        //    m_rb.AddForce(Vector3.up * m_hoverPower, ForceMode.Force);
        //}

    }

    void Update()
    {

        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        character.Rotate(0, X_Rotation, 0);

        float nowAngle = pivot.localRotation.x;

        if (-Y_Rotation != 0)
        {
            if (0 < Y_Rotation)
            {
                if (minAngle <= nowAngle) pivot.Rotate(-Y_Rotation, 0, 0);
            }
        }
        else
        {
            if (nowAngle <= maxAngle) pivot.Rotate(-Y_Rotation, 0, 0);
        }

        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, h * m_turnSpeed, 0);

        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.forward * m_moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            // Y軸を中心に右旋回
            transform.Rotate(new Vector3(0, 5, 0));
            //円を描くように右旋回
            //transform.position += transform.right * m_moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            // Y軸を中心に左旋回
            transform.Rotate(new Vector3(0, -5, 0));
            //円を描くように左旋回
            //transform.position -= transform.right * m_moveSpeed * Time.deltaTime;
        }



    }

    //if (!m_view.IsMine) return;

    //// 入力方向のベクトルを組み立てる
    //Vector3 dir = Vector3.forward * v + Vector3.right * h;

    //if (dir != Vector3.zero)
    //{
    //    // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
    //    dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
    //    dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

    //    // 入力方向に滑らかに回転させる
    //    Quaternion targetRotation = Quaternion.LookRotation(dir);
    //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);

    //    m_movingDirection = dir.normalized * m_moveSpeed; // 入力した方向に力をかける
    //}
    //else
    //{
    //    m_movingDirection = Vector3.zero;
    //}

    //    // Animator Controller のパラメータをセットする
    //    if (m_anim)
    //    {
    //        // 攻撃ボタンを押された時の処理
    //        if (Input.GetButtonDown("Fire1") && IsGrounded())
    //        {
    //            m_anim.SetTrigger("Attack");
    //            /* -----------
    //             * TODO: アニメーションイベントを使って攻撃モーションが始まる時に動きを止め、
    //             * 攻撃モーションが終わったら動けるように処理を追加する
    //             * ----------- */
    //        }

    //        // 水平方向の速度を Speed にセットする
    //        Vector3 velocity = m_rb.velocity;
    //        velocity.y = 0f;
    //        m_anim.SetFloat("Speed", velocity.magnitude);

    //        // 地上/空中の状況に応じて IsGrounded をセットする
    //        if (m_rb.velocity.y <= 0f && IsGrounded())
    //        {
    //            m_anim.SetBool("IsGrounded", true);
    //        }
    //        else if (!IsGrounded())
    //        {
    //            m_anim.SetBool("IsGrounded", false);
    //        }
    //    }

    //    // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
    //    if (Input.GetButtonDown("Jump") && IsGrounded())
    //    {
    //        m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);

    //        // Animator Controller のパラメータをセットする
    //        if (m_anim)
    //        {
    //            m_anim.SetBool("IsGrounded", false);
    //        }
    //    }

    //    // 空中でジャンプボタンを押し続けると若干ホバーする
    //    if (Input.GetButton("Jump") && !IsGrounded())
    //    {
    //        m_isHovering = true;
    //    }
    //    else
    //    {
    //        m_isHovering = false;
    //    }
    //}

    ///// <summary>
    ///// 地面に接触しているか判定する
    ///// </summary>
    ///// <returns></returns>
    //bool IsGrounded()
    //{
    //    // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
    //    CapsuleCollider col = GetComponent<CapsuleCollider>();
    //    Vector3 start = this.transform.position + col.center;   // start: 体の中心
    //    Vector3 end = start + Vector3.down * m_isGroundedLength;  // end: start から真下の地点
    //    Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
    //    bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
    //    return isGrounded;
    //}
}

