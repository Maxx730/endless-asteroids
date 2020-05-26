using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public GameObject targetUnit;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    {
        if(transform.position.y > screenBounds.y)
        {
            Destroy(transform.gameObject);
        }

        if(targetUnit)
        {
            Vector2 dir = transform.position - targetUnit.transform.position;
            transform.position = new Vector3(transform.position.x - (dir.x * Time.deltaTime * 5f), transform.position.y - (Time.deltaTime * dir.y * 5f), transform.position.z);

        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 3f), transform.position.z);
        }
    }
}
