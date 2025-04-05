using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Hector Palos-Hernandez
 * 4/5/25
 * Displays the amount of Wumpa Fruit and Lives the player has
 */

public class UIManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public TMP_Text wumpaText;
    public TMP_Text livesText;

    private void Update()
    {
        wumpaText.text = "Wumpa Fruit: " + playerMovement.totalWumpaFruit;
        livesText.text = "Lives: " + playerMovement.lives;
    }
}
