using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/7/25
 * Starts a timer when the player lands on the platform, then it breaks and respawns after 5 seconds
 */

public class BreakablePlatform : MonoBehaviour
{
    public Renderer renderer;

    private void Start()
    {
        renderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStep();
    }

    /// <summary>
    /// Detects if the player lands on it and starts the BreakPlatform function
    /// </summary>
    private void PlayerStep()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, 2f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                StartCoroutine(BreakPlatform());
            }
            if (hit.collider.gameObject.tag == "Attack")
            {
                StartCoroutine(BreakPlatform());
            }
        }
    }

    /// <summary>
    /// Waits 3 seconds, disables platform
    /// </summary>
    /// <returns></returns>
    IEnumerator BreakPlatform()
    {
        yield return new WaitForSeconds(3);
        renderer.enabled = false;
        yield return new WaitForSeconds(5);
        renderer.enabled = true;
    }
}
