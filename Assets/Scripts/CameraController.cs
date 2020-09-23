﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform character, pivot;
    public float distanceTaget = 5.0f;
    public float heightTaget = 5.0f;
    Vector3 lookAtOffset = new Vector3(0.0f,2.0f,0.0f);
    [Range(1,10)]public int sensitivityX, sensitivityY;
    [Range(-0.999f, -0.5f)] public float maxAngle = -0.5f;
    [Range(0.5f, 0.999f)] public float minAngle = 0.5f;
    PhotonView m_view;
    void Start()
    {
        if (character == null) character = transform.parent;
        if (pivot == null) pivot = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!m_view.IsMine) return;
        float X_Rotation = Input.GetAxis("Mouse X") * sensitivityX;
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
    }
}
