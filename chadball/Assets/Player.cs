using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject playerController;
    private void Start()
    {
        playerController = GameObject.Find("PlayerController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "asteroid")
        {
            collision.gameObject.GetComponent<Obstacle>().hitpoints = 0;
            playerController.GetComponent<PlayerController>().damageHealth(5f);
        }
    }
}
