using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/6/25
 * When a player jumps on the crate, it drops at most 5 wumpa fruit
 */

public class Crates : MonoBehaviour
{
    public GameObject[] droppedWumpaFruit;
    public float dropChance = 5f;
    public int minFruitDrop;
    public int maxFruitDrop;

    private void Start()
    {
        int randomNumber = Random.Range(1, 5);
    }
    private void Update()
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
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Random.value < dropChance)
        {
            int numToDrop = Random.Range(minFruitDrop, maxFruitDrop + 1);
            for (int i = 0;  i < numToDrop; i++)
            {
                if (droppedWumpaFruit.Length > 0)
                {
                    GameObject drop = droppedWumpaFruit[Random.Range(0, droppedWumpaFruit.Length)];
                    Vector3 dropPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    Instantiate(drop, dropPosition, Quaternion.identity);
                }
            }
        }
    }
}