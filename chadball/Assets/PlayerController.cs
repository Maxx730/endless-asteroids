using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject shot, player, pauseScreen, playScreen, deathAnimation, rocket;
    public float shotSpeed, slowMoFactor, bulletTimeDecrease, shotPower, rewardAmount = 1, rocketInterval;
    public Text cashIndicator, scoreIndicator;
    public Slider bulletTimeIndicator, healthIndicator;

    private float bulletTimeAmount = 1, cash, playerHealth = 100, lastTime, lastRocketTime, scoreMultiplyer = 1, score = 0;
    private bool inBullettime = false, paused = false;
    private GameObject[] hardpoints;
    private GameController controller;

    private void Start()
    {
        //bulletTimeIndicator.value = bulletTimeAmount;
        hardpoints = GameObject.FindGameObjectsWithTag("hardpoint");
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //For computer testing only
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.transform.position = new Vector3(pos.x, -3.5f, 0);

        switch (Input.touchCount)
        {
            case 1:
                if (Input.GetMouseButton(0) && (Time.time - lastTime) > shotSpeed){
                    SpawnBall();
                    lastTime = Time.time;
                }

                //Always move the player ship if using one finger.
                Vector3 retPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                player.transform.position = new Vector3(retPos.x, -3.5f, 0);
                break;
            case 2:
                spawnRocket();
                break;
        }

        if (Input.GetMouseButton(0) && (Time.time - lastRocketTime) > rocketInterval && controller.gameActive)
        {
            spawnRocket();
            lastTime = Time.time;
            lastRocketTime = Time.time;
        }
    }

    private void SpawnBall()
    {
        for (int i = 0; i < hardpoints.Length; i++)
        {
            GameObject newShot = Instantiate(shot, new Vector3(hardpoints[i].transform.position.x, hardpoints[i].transform.position.y, 0), Quaternion.identity);
        }
    }

    public void addBulletTime(float time)
    {
        if (bulletTimeAmount <= 1)
        {
            bulletTimeAmount += time;

            if (bulletTimeAmount > 1)
            {
                bulletTimeAmount = 1;
            }
        }

        if (bulletTimeAmount <= 0)
        {
            inBullettime = false;
        }

        bulletTimeIndicator.value = bulletTimeAmount;
    }

    public void addCash(float amount)
    {
        cash += amount;
        cashIndicator.text = "$" + cash.ToString();
    }

    public void damageHealth(float amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            Destroy(player);
            Instantiate(deathAnimation, player.transform.position, Quaternion.identity);
        } else
        {
            healthIndicator.value = playerHealth;
        }
    }

    public void upgradeShot()
    {
        shotPower += 0.25f;
    }

    public void spawnRocket()
    {
        for(int i = 0;i < controller.rocketSpawnAmount;i++)
        {
            Instantiate(rocket, player.transform.position, Quaternion.identity);
        }
    }

    public void addScore(float amount)
    {
        score += amount * scoreMultiplyer;
    }

    public void AsteroidDestroyed()
    {
        controller.AddDestroyedAsteroid();
    }
}
