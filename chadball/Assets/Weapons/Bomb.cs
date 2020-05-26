using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float growthAmount, maxScale;

    private void Update()
    {
        if (transform.localScale.x >= maxScale) {
            GetComponent<CircleCollider2D>().enabled = false;
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x + (growthAmount * Time.deltaTime), transform.localScale.y + (growthAmount * Time.deltaTime), transform.localScale.z);
        }
    }
}
