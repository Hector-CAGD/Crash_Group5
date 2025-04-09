using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/8/25
 * The crate progressively breaks when the player hits it, three hits and it breaks and gives the player an extra life
 */

public class ExtraLifeCrates : MonoBehaviour
{
    public Renderer firstCrateR;
    public Renderer renderer1;
    public Renderer renderer2;

    public Collider firstCrateC;
    public Collider collider1;
    public Collider collider2;

    public int crateLife = 3;

    // Update is called once per frame
    void Start()
    {
        firstCrateR.enabled = true; // the base crate is enabled
        firstCrateC.enabled = true;

        collider1.enabled = false; // the damaged crate is not enabled
        renderer1.enabled = false;

        collider2.enabled = false; // the final damaged crate is not enabled
        renderer2.enabled = false;
    }

    private void Update()
    {
        if (crateLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Damage();
        }
    }

    public void Damage()
    {
        crateLife--;

        if (crateLife == 2)
        {
            firstCrateR.enabled = false;
            renderer1.enabled = true;
            collider1.enabled = true;
        }
        if (crateLife == 1)
        {
            renderer1.enabled = false;
            collider1.enabled = false;

            renderer2.enabled = true;
            collider2.enabled = true;
        }
        if (crateLife == 0)
        {
            renderer2.enabled = false;
            collider2.enabled = false;
            GetComponent<PlayerMovement>().lives++;
        }
    }
}
