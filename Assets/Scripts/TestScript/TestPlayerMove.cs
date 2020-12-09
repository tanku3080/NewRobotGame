using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    [SerializeField] GameObject thisObj = null;
    Rigidbody _rd;
    void Start()
    {
        thisObj = this.gameObject;
        _rd = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rd.velocity += Vector3.forward;
        }
    }
}
