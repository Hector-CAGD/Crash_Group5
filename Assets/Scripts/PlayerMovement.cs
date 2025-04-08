using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float rotationSpeed;
    public float totalWumpaFruit;
    public bool waiting;

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
        MoveAndTurn();
        Attack();
        OneUpLife();
        SpikeShell();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WumpaFruit")
        {
            totalWumpaFruit += wumpaFruitValue;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Spike")
        {
            Respawn();
        }
    }

    private void Attack()
    {
        if (waiting == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(AttackChange());
            }
        }
    }

    private void MoveAndTurn()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (transform.position.y < fallDepth)
        {
            Respawn();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;

            // if the raycast returns true then an object has hit and the player is touching the floor
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
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

    private void SpikeShell() // Player respawns if they land on the spiky turtle enemy
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "SpikyEnemy")
            {
                Respawn();
            }
        }
    }

    public void OneUpLife() // grants the player another life when they collect 100 wumpa fruit
    {
        if (totalWumpaFruit == 100)
        {
            print("You got another life!");
            lives++; // adds 1 to the total lives
            totalWumpaFruit = 0; // Resets the count to 0
        }
    }

    public void Respawn()
    {
        transform.position = startPosition;
        lives--;

        if (lives <= 0)
        {
            this.enabled = false;
            SceneManager.LoadScene(2);

        }

        StartCoroutine(Blink());
    }

    IEnumerator AttackChange()
    {
        myMaterial.color = Color.red; // changes material color to red
        gameObject.tag = "Attack"; // changes tag to attack for when enemies collide, they die
        yield return new WaitForSeconds(1); // in attack for 1 second
        myMaterial.color = new Color(1.0f, 0.64f, 0.0f); // changes color of material to orange
        gameObject.tag = "Player"; // changes tag back to player for when enemies collide, the player dies
        waiting = true;
        StartCoroutine(WaitToAttack());
    }

    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(3/2);

        waiting = false;
    }

    IEnumerator Blink() // the player blinks as it respawns
    {
        for (int index = 0; index < 30; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<MeshRenderer>().enabled = true;
    }
}
