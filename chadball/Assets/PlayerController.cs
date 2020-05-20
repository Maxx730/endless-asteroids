using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject shot, player, pauseScreen, playScreen, deathAnimation;
    public float shotSpeed, slowMoFactor, bulletTimeDecrease, shotPower, rewardAmount = 1;
    public Text cashIndicator;
    public Slider bulletTimeIndicator, healthIndicator;

    private float bulletTimeAmount = 1;
    private float cash, playerHealth = 100;
    private float lastTime;
    private bool inBullettime = false, paused = false;
    private GameObject[] hardpoints;

    private void Start()
    {
        bulletTimeIndicator.value = bulletTimeAmount;
        hardpoints = GameObject.FindGameObjectsWithTag("hardpoint");
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && (Time.time - lastTime) > shotSpeed)
        {
            SpawnBall();
            lastTime = Time.time;
        }

        if(playerHealth >= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                inBullettime = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                inBullettime = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            paused = !paused;

            if(paused)
            {
                pauseScreen.SetActive(true);
            } else
            {
                pauseScreen.SetActive(false);
            }
        }

        if (paused)
        {
            Time.timeScale = 0;
        } else
        {
            if (inBullettime)
            {
                Time.timeScale = slowMoFactor;
                Time.fixedDeltaTime = Time.timeScale * .02f;
                addBulletTime(-bulletTimeDecrease);
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
            }

            healthIndicator.value = playerHealth;
            Vector3 retPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.transform.position = new Vector3(retPos.x, -3.5f, 0);
        }
    }

    private void SpawnBall()
    {
        for(int i = 0;i < hardpoints.Length; i++)
        {
            GameObject newShot = Instantiate(shot, new Vector3(hardpoints[i].transform.position.x, hardpoints[i].transform.position.y, 0), Quaternion.identity);
            newShot.GetComponent<Ball>().damage = shotPower;
        }
    }

    public void addBulletTime(float time)
    {
        if(bulletTimeAmount <= 1)
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
        cashIndicator.text = cash.ToString();
    }

    public void damageHealth(float amount)
    {
        playerHealth -= amount;
        if(playerHealth <= 0)
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
        removeMoney(10);
    }

    private void removeMoney(float amount)
    {
        cash -= amount;
    }
}
