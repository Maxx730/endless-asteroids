using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> objects;
    public float spawnTime;

    private float lastTime;
    private void Update()
    {
        if(Time.time - lastTime > spawnTime)
        {
            int randomSpawn = Random.Range(0, objects.Count - 1);
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            GameObject obsti = Instantiate(objects[randomSpawn], new Vector3(Random.Range(-screenBounds.x, screenBounds.x), transform.position.y, 0), Quaternion.identity);
            lastTime = Time.time;
        }
    }
}
