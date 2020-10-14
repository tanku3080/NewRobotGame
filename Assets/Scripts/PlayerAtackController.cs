using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtackController : MonoBehaviour
{
    Ray _ray;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Vector3.zero);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("ダメージを受けた");
                }
            }
            Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.red, 5);
        }
    }
}
