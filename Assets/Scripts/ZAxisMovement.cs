using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/15/25
 * For the last level, this script will allow floating platforms to move on the z axis towards the final boss and will only move while the player steps on it
 */

public class ZAxisMovement : MonoBehaviour
{
    public int speed;

    public GameObject forwardPoint;
    public GameObject backPoint;
    public Vector3 forwardPos;
    public Vector3 backPos;
    public bool goingForward;

    public bool steppedOn;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        forwardPos = forwardPoint.transform.position;
        backPos = backPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GroundCheck();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            steppedOn = true;
        }
        else
        {
            steppedOn = false;
        }
    }

    private void Move()
    {
        if (steppedOn == true)
        {
            if (goingForward)
            {
                speed = 4;
                transform.LookAt(forwardPos);
                if (transform.position.z <= forwardPos.z)
                {
                    goingForward = false;
                }
                else
                {
                    transform.position += Vector3.forward * Time.deltaTime * speed;
                }
            }
            else
            {
                speed = 4;
                transform.LookAt(backPos);
                if (transform.position.z >= backPos.z)
                {
                    goingForward = true;
                }
                else
                {
                    transform.position += Vector3.back * Time.deltaTime * speed;
                }
            }
        }
        else
        {
            speed = 0;
        }
    }

    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f)) // Checks for a wall and turns the enemy away from it and continues its path
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                if (goingForward == true)
                {
                    goingForward = false;
                }
                else
                {
                    goingForward = true;
                }
            }
            if (hit.collider.gameObject.tag == "Ground")
            {
                if (goingForward == true)
                {
                    goingForward = false;
                }
                else
                {
                    goingForward = true;
                }
            }
        }
    }
}
