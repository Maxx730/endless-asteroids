using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleMessage : MonoBehaviour
{
    public string message;
    public float speed, duration;

    private float lastTime;

    private void Start()
    {
        lastTime = Time.time;
        GetComponent<Text>().text = message;
    }

    void Update()
    {
        if(Time.time - lastTime > duration)
        {
            Destroy(transform.gameObject);
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
        }
    }
}
