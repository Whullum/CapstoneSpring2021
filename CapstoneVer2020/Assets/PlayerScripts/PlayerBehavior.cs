using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public int health;

    public Image[] heartImage;

    private void Start()
    {
        health = 3;
    }

    public void GetDamaged()
    {
        // Check if health is 0
        if (health < 0)
        {
            // If it is, display game over for now
            Debug.Log("GAME OVER");
        }
        else
        {
            // Deincrement health
            health--;

            // Change the color of hearts image to white (just testing for now)
            heartImage[health].color = Color.white;
        }
    }
}
