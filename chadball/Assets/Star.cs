using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float starSpeed;

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + -starSpeed * Time.deltaTime, transform.position.z);
    }
}
