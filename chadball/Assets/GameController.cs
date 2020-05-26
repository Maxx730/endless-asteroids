using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int waveNumber, waveAsteroidCount, rocketSpawnAmount;
    public GameObject astSpawner, waveMenu, gameOverMenu;
    public Text waveText, destroyedText, cashText, destroyedCounter, gamePlayCash;
    public Slider healthDisplay;
    public bool gameActive = true;

    private int asteroidsDestroyed = 0, playerCash = 0, playerHealth = 100;

    private void Start()
    {
        astSpawner.GetComponent<ObstacleSpawner>().spawnAmount = GetAsteroidAmount(waveNumber);
        healthDisplay.value = playerHealth;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameLoop");
    }

    public void OnWaveComplete()
    {
        GameObject[] rocketsLeft = GameObject.FindGameObjectsWithTag("rocket");
        for(int i = 0;i < rocketsLeft.Length;i++)
        {
            Destroy(rocketsLeft[i]);
        }

        waveMenu.SetActive(true);
        waveText.text = "Wave " + waveNumber;
        destroyedCounter.text = asteroidsDestroyed.ToString();
        gameActive = false;
    }

    public void StartNextWave()
    {
        waveMenu.SetActive(false);
        waveNumber++;
        asteroidsDestroyed = 0;
        astSpawner.GetComponent<ObstacleSpawner>().RestartSpawner();
        astSpawner.GetComponent<ObstacleSpawner>().spawnAmount = GetAsteroidAmount(waveNumber);
        gameActive = true;
    }

    public void AddDestroyedAsteroid()
    {
        asteroidsDestroyed++;
    }

    public void DamagePlayer(int damageAmount)
    {
        playerHealth -= damageAmount;
        healthDisplay.value = playerHealth;
    }

    public int GetPlayerHealth()
    {
        if(playerHealth <= 0) {
            gameOverMenu.SetActive(true);
        }

        return playerHealth;
    }

    public void RestartGameplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverMenu.SetActive(false);
    }

    public void AddMoney(int amount)
    {
        playerCash += amount;
        gamePlayCash.text = playerCash.ToString();
    }

    public void AddRocketAmount()
    {
        rocketSpawnAmount += 1;
        AddMoney(-10);
    }

    private int GetAsteroidAmount(int waveNumber)
    {
        return waveNumber * waveAsteroidCount;
    }
}
