using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/9/25
 * For extra life crates, the player gain an extra life after destroying it
 */

public class ExtraLifeCrate : MonoBehaviour
{

    void Update()
    {
        PlayerJumps();
    }

    private void PlayerJumps()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            other.gameObject.GetComponent<PlayerMovement>().lives++;
            Destroy(gameObject);
        }
    }
}
