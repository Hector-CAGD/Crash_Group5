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

    public GameObject backPoint;
    public GameObject forwardPoint;
    public Vector3 backPos;
    public Vector3 forwardPos;
    public bool goingBack;

    public bool Horizontal;
    public bool Vertical;
    public bool ZAxis;

    // Start is called before the first frame update
    void Start()
    {
        downPos = downPoint.transform.position;
        upPos = upPoint.transform.position;

        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;

        forwardPos = forwardPoint.transform.position;
        backPos = backPoint.transform.position;
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
        if (ZAxis == true)
        {
            if (goingBack)
            {
                transform.LookAt(backPos);
                if (transform.position.z <= backPos.z)
                {
                    goingBack = false;
                }
                else
                {
                    transform.position += Vector3.back * Time.deltaTime * speed;
                }
            }
            else
            {
                transform.LookAt(forwardPos);
                if (transform.position.z >= forwardPos.z)
                {
                    goingBack = true;
                }
                else
                {
                    transform.position += Vector3.forward * Time.deltaTime * speed;
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
                if (goingBack == true)
                {
                    goingBack = false;
                }
                else
                {
                    goingBack = true;
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                if (goingBack == true)
                {
                    goingBack = false;
                }
                else
                {
                    goingBack = true;
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                if (goingBack == true)
                {
                    goingBack = false;
                }
                else
                {
                    goingBack = true;
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
}
