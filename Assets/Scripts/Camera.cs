using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hector Palos-Hernandez
 * 4/4/25
 * Keeps the camera from rotating, but still follows the player
 */

public class Camera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
