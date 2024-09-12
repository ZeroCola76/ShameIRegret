using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    Vector3 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Zero");
        direction = target.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            rb.velocity = direction * moveSpeed;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ///ªÁ∏¡√≥∏Æ
            Debug.Log("ªÁ∏¡");
        }
    }
}