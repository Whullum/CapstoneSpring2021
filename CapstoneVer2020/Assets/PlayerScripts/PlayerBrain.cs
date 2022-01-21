using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum PlayerStates
{
    NORMAL,
    INTERACTING
}

public class PlayerBrain : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerInteraction playerInteraction;
    public PlayerMovement playerMovement;

    public PlayerStates currentPlayerState;

    public int health;

    public Image[] heartImage;

    private void Start()
    {
        health = 3;
        currentPlayerState = PlayerStates.NORMAL;
    }

    private void Update()
    {
        switch (currentPlayerState)
        {
            case PlayerStates.INTERACTING:
                // Stop movement so player doesn't move while talking to something
                playerMovement.StopMovement();

                // Interaction Mechanic
                playerInteraction.ActivateInteraction();
                break;
            case PlayerStates.NORMAL:
            default:
                // Move the player
                playerMovement.Movement();

                // Do dash mechanic if applicable
                playerMovement.DashMovement(playerMovement.lastMovedDirection);

                // Attack Mechanic
                playerAttack.Attack();

                // Interaction Mechanic
                playerInteraction.ActivateInteraction();
                break;
        }
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
