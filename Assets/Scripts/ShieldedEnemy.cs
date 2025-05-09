using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/10/25
 * Movement for the shielded enemy
 */

public class ShieldedEnemy : MonoBehaviour
{
    [Header("Weak Spot")]

    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject forwardPoint;
    public GameObject backPoint;

    public Vector3 leftPos;
    public Vector3 rightPos;
    public Vector3 forwardPos;
    public Vector3 backPos;

    public int speed;
    public bool horizontal;

    public bool goingLeft;
    public bool goingStraight;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;

        forwardPos = forwardPoint.transform.position;
        backPos = backPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckForCliff();
        CheckForWall();
    }
    private void Movement()
    {
        if (horizontal == true) // if it moves on the x axis *left and right*
        {
            if (goingLeft)
            {
                transform.LookAt(leftPos); //rotates enemy to the left, facing the direction it walks
                if (transform.position.x <= leftPos.x)
                {
                    goingLeft = false;
                    transform.LookAt(rightPos);
                }
                else
                {
                    transform.position += Vector3.left * Time.deltaTime * speed;
                }
            }
            else
            {
                transform.LookAt(rightPos); // rotates enemy to the right, facing the direction it walks
                if (transform.position.x >= rightPos.x)
                {
                    goingLeft = true;
                    transform.LookAt(leftPos);
                }
                else
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
            }
        }
        else
        {
            if (goingStraight) // if it moves on the z axis *forward and backwards*
            {
                transform.LookAt(forwardPos); //rotates enemy to the front (away from camera), facing the direction it walks
                if (transform.position.z >= forwardPos.z)
                {
                    goingStraight = false;
                    transform.LookAt(backPos);
                }
                else
                {
                    transform.position += Vector3.forward * Time.deltaTime * speed;
                }
            }
            else
            {
                transform.LookAt(backPos); // rotates enemy to the back (towards camera), facing the direction it walks
                if (transform.position.z <= backPos.z)
                {
                    goingStraight = true;
                    transform.LookAt(forwardPos);
                }
                else
                {
                    transform.position += Vector3.back * Time.deltaTime * speed;
                }
            }
        }
    }

    private void CheckForCliff() // function should change the enemies direction if there is no ground underneath it
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
        {
            if (goingStraight == true)
            {
                goingStraight = false;
            }
            else
            {
                goingStraight = true;
            }

            if (goingLeft == true)
            {
                goingLeft = false;
            }
            else
            {
                goingLeft = true;
            }
        }
    }

    private void CheckForWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f)) // Checks for a wall and turns the enemy away from it and continues its path
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                if (goingStraight == true)
                {
                    goingStraight = false;
                }
                else
                {
                    goingStraight = true;
                }

                if (goingLeft == true)
                {
                    goingLeft = false;
                }
                else
                {
                    goingLeft = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Respawn(); // calls the respawn function from the player script
        }
    }
}
