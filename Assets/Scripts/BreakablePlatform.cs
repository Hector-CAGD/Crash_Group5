using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Hector Palos-Hernandez
 * 4/10/25
 * Starts a timer when the player lands on the platform, then it breaks and respawns after 5 seconds
 */

public class BreakablePlatform : MonoBehaviour
{
    public Renderer renderer;
    public BoxCollider boxCollider;

    public int breakTimer;

    private void Start()
    {
        renderer.enabled = true;
        boxCollider.enabled = true;
    }
    
    /// <summary>
    /// Detects if the player collides (steps on) it and starts the BreakPlatform function
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(BreakPlatform());
        }
        if (collision.gameObject.tag == "Attack")
        {
            StartCoroutine(BreakPlatform());
        }
    }

    /// <summary>
    /// Waits 3 seconds, disables platform
    /// </summary>
    /// <returns></returns>
    IEnumerator BreakPlatform()
    {
        yield return new WaitForSeconds(breakTimer);
        renderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5);
        renderer.enabled = true;
        boxCollider.enabled = true;
    }
}
