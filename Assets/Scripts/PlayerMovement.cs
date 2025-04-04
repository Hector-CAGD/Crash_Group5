using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/4/25
 * Controls the player movement using the w,a,s, and d key to move forward, left, back, and right
 */

public class PlayerMovement : MonoBehaviour
{
    // Jump force added when player presses space to jump
    public float jumpForce = 4f;
    private Rigidbody rigidbody;
    public float speed = 4f;
    public float totalWumpaFruit;

    public float wumpaFruitValue = 1f;

    // respawning variables
    public int lives = 3;
    public int fallDepth;
    private Vector3 startPosition;

    [SerializeField]
    private Material myMaterial;

    void Start()
    {
        startPosition = transform.position;
        // set reference to the player's rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(AttackChange());
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;

            // if the raycast returns true then an object has hit and the player is touching the floor
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                Debug.Log("Touching the ground");

                // adds an upwards velocity to the player object causing the player to jump up
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Can't jump midair");
            }
        }
    }

    IEnumerator AttackChange()
    {
        myMaterial.color = Color.red;
        
        yield return new WaitForSeconds(3 / 2);
        myMaterial.color = new Color(1.0f, 0.64f, 0.0f); // new color numbers make it orange
    }
}
