using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public float damageTimeout, hitpoints;
    public int cashValue;
    public GameObject explosion, explosionMessage, cashIndicator;
    public List<GameObject> explosions;
    public bool isBackground = false, isTargeted;

    private Rigidbody2D rb;
    private float lastTime, xForce, yForce;
    private PlayerController controller;
    private ObstacleSpawner spawner;
    private GameObject UICanvas;
    private Vector2 screenBounds;

    private void Start()
    {
        if(!isBackground)
        {
            controller = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            UICanvas = GameObject.Find("UICanvas");
        }

        spawner = GameObject.Find("AsteroidSpawner").GetComponent<ObstacleSpawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        rb = GetComponent<Rigidbody2D>();
        yForce = Random.Range(-500, -15);
        xForce = Random.Range(-10, 10);
    }

    void Update()
    {
        if (!isBackground)
        {
            if (Time.time - lastTime > damageTimeout)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (hitpoints <= 0)
            {
                spawner.onObstacleDestroyed(true);

                Instantiate(explosions[Random.Range(0, explosions.Count - 1)], transform.position, Quaternion.identity);

                Destroy(transform.gameObject);
            }
            else if (transform.position.y < (-screenBounds.y - 0.5f))
            {
                spawner.onObstacleDestroyed(false);
                Destroy(transform.gameObject);
            }
        }

        rb.AddForce(new Vector2(xForce, yForce));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            lastTime = Time.time;
            hitpoints -= collision.gameObject.GetComponent<Shot>().shotDamage;
        }

        if (collision.gameObject.tag == "bomb")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            lastTime = Time.time;
            hitpoints = 0;
        }
    }

    private GameObject SpawnTextIndicator()
    {
        GameObject obj = new GameObject("TextIndicator");
        GameObject text = Instantiate(cashIndicator);

        text.transform.SetParent(obj.transform);
        return obj;
    }

    public void OnTargeted()
    {
        isTargeted = true;
        //Find the redicle and show it.
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnLostTargeted()
    {
        isTargeted = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
