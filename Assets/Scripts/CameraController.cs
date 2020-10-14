using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform pivot;
    Transform lookCharacter;
    [SerializeField] LayerMask raycastHitmask;
    Vector3 cameraOffset;
    public float lookDistance;
    [Range(1,10)]public int sensitivityX, sensitivityY;
    [Range(-0.999f, -0.5f)] public float maxAngle = -0.5f;
    [Range(0.5f, 0.999f)] public float minAngle = 0.5f;
    PhotonView m_view;
    void Start()
    {
        if (lookCharacter == null) lookCharacter = transform.parent;

        cameraOffset = transform.localPosition;
        transform.LookAt(lookCharacter);
        lookDistance = Vector3.Distance(transform.position,lookCharacter.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookTargetPos = lookCharacter.position;

        Vector3 cameaPos = lookTargetPos - (transform.forward * lookDistance);

        Vector3 targetDir = (transform.position - lookTargetPos).normalized;
        float targetDistance = lookDistance + 0.5f;

        bool isHit = Physics.Raycast(lookTargetPos,targetDir,out RaycastHit hit,targetDistance,raycastHitmask);

        if (isHit) cameaPos = hit.point;
        transform.position = cameaPos;


        //if (!m_view.IsMine) return;
        float X_Rotation = Input.GetAxis("Mouse X") * sensitivityX;
        float Y_Rotation = Input.GetAxis("Mouse Y");

        if (transform.forward.y > 0.6f && Y_Rotation < 0) Y_Rotation = 0;
        if (transform.forward.y < -0.6f && Y_Rotation > 0) Y_Rotation = 0;

        if (Mathf.Abs(Y_Rotation) > 0.2f) transform.RotateAround(lookTargetPos,lookCharacter.right,Y_Rotation * sensitivityY);

        transform.LookAt(lookTargetPos);
    }
}
