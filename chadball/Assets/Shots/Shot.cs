using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float shotSpeed, shotDamage;

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(transform.gameObject);
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (shotSpeed * Time.deltaTime), transform.position.z);
    }
}
