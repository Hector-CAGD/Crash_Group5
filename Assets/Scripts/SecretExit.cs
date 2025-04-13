using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/12/25
 * Player teleports to secret level, and can't go back after the first time
 */

public class SecretExit : MonoBehaviour
{
    [SerializeField]
    public Material material1;
    public Material material2;
    private Renderer renderer;

    public GameObject spawnPoint;

    private void Start()
    {
        renderer.material = material1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(PortalSwitch());
        }
        if (collision.gameObject.tag == "Attack")
        {
            StartCoroutine(PortalSwitch());
        }
    }

    IEnumerator PortalSwitch()
    {
        yield return new WaitForSeconds(2);
        material1.color = material2.color;
        yield return new WaitForSeconds(2);
        gameObject.tag = "Broken";
    }
}
