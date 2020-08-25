using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizumo : MonoBehaviour
{
    public float gizmoSize = 0.5f;
    public Color gizmoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);

    }
}
