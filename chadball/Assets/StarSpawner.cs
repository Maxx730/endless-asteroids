using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    public float spawnRate;

    private Vector2 screenBounds;
    private float lastSpawn;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update()
    {
        if(Time.time - lastSpawn > spawnRate)
        {
            Instantiate(star, new Vector3(Random.Range(-screenBounds.x, screenBounds.x), transform.position.y, 0), Quaternion.identity);
            lastSpawn = Time.time;
        }
    }
}
