using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rd.velocity = Vector3.forward * 10;
        Debug.Log(rd.velocity);
    }
}
