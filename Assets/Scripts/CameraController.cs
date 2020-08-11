using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform character, pivot;
    [Range(-0.999f, -0.5f)] public float maxAngle = -0.5f;
    [Range(0.5f, 0.999f)] public float minAngle = 0.5f;
    void Start()
    {
        if (character == null) character = transform.parent;
        if (pivot == null) pivot = transform;
    }

    // Update is called once per frame
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
    }
}
