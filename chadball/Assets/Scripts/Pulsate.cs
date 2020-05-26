using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsate : MonoBehaviour
{
    private int startScale = 1;

    void Update()
    {
        transform.localScale = new Vector3((Mathf.Sin(Time.time) * .5f) + 1f, (Mathf.Sin(Time.time) * .5f) + 1f, 1);  
    }
}
