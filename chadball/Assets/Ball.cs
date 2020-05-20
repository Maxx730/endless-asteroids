using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float throwForce;
    public float maxVelocity;
    public int hitpoints;
    public GameObject explosion;
    public float damage;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(transform.gameObject);
        }
    }

    void Update()
    {
        if(rb.velocity.y < Math.Abs(maxVelocity))
        {
           //transform.localScale = new Vector3(1, .75f, 1);
            rb.AddForce(new Vector2(0, throwForce));
        } else
        {
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
