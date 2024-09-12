using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class TestNull : MonoBehaviour
{
    //public GameObject myObject;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 position = new Vector3();
        position.Set(-1, 2, 3);

    }

    void Update()
    {
        //myObject.SetActive(true);
        rb.velocity = new Vector2(1f, 0f);
    }
}
