using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/*
 * Hector Palos-Hernandez
 * 4/4/25
 * Controls the enemy movement
 */

public class EnemyMovement : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;

    public Vector3 leftPos;
    public Vector3 rightPos;

    public int speed;
    public bool goingLeft;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
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
            transform .LookAt(rightPos); // rotates enemy to the right, facing the direction it walks
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Destroy(gameObject); // when the player is in attack mode, the enemy dies, not the player
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Respawn(); // calls the respawn function from the player script
        }
    }
}
