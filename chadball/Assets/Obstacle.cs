using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public float damageTimeout;
    public float hitpoints;
    public GameObject explosion, explosionMessage;
    public float cashValue;

    private Rigidbody2D rb;
    private float lastTime, xForce, yForce;
    private PlayerController controller;
    private GameObject UICanvas;

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
    private void Start()
    {
        controller = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        UICanvas = GameObject.Find("UICanvas");
        xForce = Random.Range(-15, 15);
        yForce = Random.Range(-500, -15);
    }

    void Update()
    {
        if(Time.time - lastTime > damageTimeout)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if(hitpoints <= 0)
        {
            controller.addBulletTime(0.05f);
            controller.addCash(cashValue * transform.localScale.x);
            Destroy(transform.gameObject);
            GameObject expo = Instantiate(explosion, transform.position, Quaternion.identity);
            expo.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
        }

        rb.AddForce(new Vector2(xForce, yForce));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            lastTime = Time.time;
            hitpoints -= collision.gameObject.GetComponent<Ball>().damage;
        }
    }
}
