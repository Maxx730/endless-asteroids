using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBomb : MonoBehaviour
{
    public float rocketSpeed, rocketSize, rocketDamage, rocketTimeout, turningRadius, trackingRadius;
    public GameObject bomb, target;
    public bool showTarget;

    private Rigidbody2D rb;
    private Vector2 screenBounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update()
    {
        //Get a target if the rocket has not found one yet.
        if(!target)
        {
            GetHomingTarget();
            MoveForward();
        }
        else
        {
            //Rotate and move towards the target if one has been found until impact.
            RotateAtTarget();
            MoveTowardsTarget();

            if(showTarget && target)
            {
                Debug.DrawLine(target.transform.position, transform.position);
            }
        }

        if (transform.position.y > (screenBounds.y + 5) || transform.position.y < -screenBounds.y)
        {
            Destroy(transform.gameObject);
            target.GetComponent<Asteroid>().isTargeted = false;
        }
    }

    //Find the first asteroid that has overlapped the given radius.
    private void GetHomingTarget() {
        Collider2D[] overlapped = Physics2D.OverlapCircleAll(transform.position, trackingRadius);

        foreach(Collider2D obj in overlapped)
        {
            if(obj.gameObject.tag == "asteroid" && obj.gameObject.GetComponent<Asteroid>().isTargeted != true && !target)
            {
                target = obj.gameObject;
                obj.gameObject.GetComponent<Asteroid>().OnTargeted();
            }
        }
    }

    //Rotates the rocket towards it given target.
    private void RotateAtTarget()
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), turningRadius);
    }

    private void MoveTowardsTarget()
    {
        //First start checking distance, if close enough stop adding force.
        float distance = Vector2.Distance(transform.position, target.transform.position);
        rb.AddForce(transform.up * rocketSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "asteroid")
        {
            Destroy(transform.gameObject);
            collision.gameObject.GetComponent<Asteroid>().hitpoints = 0;
            target.GetComponent<Asteroid>().OnLostTargeted();
        }
    }

    private void MoveForward()
    {
        rb.AddForce(transform.up * rocketSpeed * Time.deltaTime);
    }
}
