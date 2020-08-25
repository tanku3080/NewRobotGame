using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //Rigidbody m_rb;

    //bool m_isHovering = false;
    //Vector3 m_movingDirection = Vector3.zero;

    //void Start()
    //{
    //    m_rb = GetComponent<Rigidbody>();
    //    m_view = GetComponent<PhotonView>();

    //    // カメラターゲットに自分を設定する
    //    if (m_view.IsMine)
    //    {
    //        CinemachineVirtualCameraBase vcam = GameObject.FindObjectOfType<CinemachineVirtualCameraBase>();
    //        vcam.Follow = transform;
    //        vcam.LookAt = transform;
    //    }
    //}

    void Update()
    {
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
            transform.position += transform.right * m_moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= transform.right * m_moveSpeed * Time.deltaTime;
        }
    }
}
