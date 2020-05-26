using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float ShieldRadius;

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ShieldRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "coin":
                gameController.AddMoney(collision.gameObject.GetComponent<Coin>().value);
                Destroy(collision.gameObject);
                break;
            case "asteroid":
                collision.gameObject.GetComponent<Asteroid>().hitpoints = 0;
                gameController.DamagePlayer(33);

                if (gameController.GetPlayerHealth() <= 0)
                {
                    Destroy(transform.gameObject);
                }
                break;
        }

    }

    private void Update()
    {
        Collider2D[] overlapped = Physics2D.OverlapCircleAll(transform.position, ShieldRadius);

        foreach(Collider2D col in overlapped)
        {
            if(col.gameObject.tag == "coin")
            {
                col.gameObject.GetComponent<Coin>().targetUnit = gameObject;
            }
        }
    }
}
