using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> objects;
    public float spawnTime;
    public bool isBackground = false;
    public int spawnAmount = 0;

    private int spawnCounter = 0, spawned = 0;
    private float lastTime;
    private GameController controller;

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if(Time.time - lastTime > spawnTime)
        {
            if (spawnCounter >= spawnAmount)
            {

            }
            else
            {
                int randomSpawn = Random.Range(0, objects.Count - 1);
                Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                GameObject obsti = Instantiate(objects[randomSpawn], new Vector3(Random.Range(-screenBounds.x + 5f, screenBounds.x - 5f), transform.position.y, 0), Quaternion.identity);
                obsti.GetComponent<Asteroid>().isBackground = isBackground;
                spawnCounter++;
                spawned++;
            }

            lastTime = Time.time;
        }
    }

    public void onObstacleDestroyed(bool isUser = false)
    {
        spawned--;

        if (isUser)
        {
            controller.AddDestroyedAsteroid();
        }

        //End the wave if the last of the asteroids has been destroyed.
        if (spawned == 0)
        {
            if(controller)
            {
                controller.OnWaveComplete();
            }
        }
    }

    public void RestartSpawner() {
        spawnCounter = 0;
        spawned = 0;
    }
}
