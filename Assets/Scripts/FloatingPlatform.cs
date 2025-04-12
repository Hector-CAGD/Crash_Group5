using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/10/25
 * Moves the platform the script is attached to and allows it to move between horizontal and vertical axis using invisible points
 */

public class FloatingPlatform : MonoBehaviour
{
    public int speed;

    public GameObject leftPoint;
    public GameObject rightPoint;
    public Vector3 leftPos;
    public Vector3 rightPos;
    public bool goingLeft;

    public GameObject downPoint;
    public GameObject upPoint;
    public Vector3 downPos;
    public Vector3 upPos;
    public bool goingDown;

    public bool Horizontal;
    public bool Vertical;

    // Start is called before the first frame update
    void Start()
    {
        downPos = downPoint.transform.position;
        upPos = upPoint.transform.position;

        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FourWayCheck();
    }

    private void Move()
    {
        if (Horizontal == true)
        {
            if (goingLeft)
            {
                transform.LookAt(leftPos);
                if (transform.position.x <= leftPos.x)
                {
                    goingLeft = false;
                }
                else
                {
                    transform.position += Vector3.left * Time.deltaTime * speed;
                }
            }
            else
            {
                transform.LookAt(rightPos);
                if (transform.position.x >= rightPos.x)
                {
                    goingLeft = true;
                }
                else
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
            }
        }
        if (Vertical == true)
        {
            if (goingDown)
            {
                if (transform.position.y <= downPos.y)
                {
                    goingDown = false;
                }
                else
                {
                    transform.position += Vector3.down * Time.deltaTime * speed;
                }
            }
            else
            {
                if (transform.position.y >= upPos.y)
                {
                    goingDown = true;
                }
                else
                {
                    transform.position += Vector3.up * Time.deltaTime * speed;
                }
            }
        }
    }

    /// <summary>
    /// Checks every direction of the floating platform for any obstruction, and changes direction if there is any obstruction in the way
    /// </summary>
    private void FourWayCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f)) // Checks for a wall and turns the enemy away from it and continues its path
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                if (goingLeft == true)
                {
                    goingLeft = false;
                }
                else
                {
                    goingLeft = true;
                }
            }
            if (hit.collider.gameObject.tag == "Ground")
            {
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                if (goingDown == true)
                {
                    goingDown = false;
                }
                else
                {
                    goingDown = true;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                if (goingDown == true)
                {
                    goingDown = false;
                }
                else
                {
                    goingDown = true;
                }
            }
        }
    }
}
