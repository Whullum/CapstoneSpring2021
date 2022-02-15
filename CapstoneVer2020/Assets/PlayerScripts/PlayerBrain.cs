using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerStates
{
    NORMAL,
    INTERACTING,
    DASHING,
    INVULERABLE
}

public class PlayerBrain : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public PlayerInteraction playerInteraction;
    public PlayerMovement playerMovement;

    public PlayerStates currentPlayerState;

    public TextMeshProUGUI goldTextDisplay;

    public int health;
    public int maxHealth;
    public int gold;

    public Vector2 position;

    public Image[] heartImage;

    private void Start()
    {
        health = 3;
        maxHealth = 3;
        gold = 0;
        currentPlayerState = PlayerStates.NORMAL;

        playerAttack.playerBrain = this;
        playerInteraction.playerBrain = this;
        playerMovement.playerBrain = this;
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
            case PlayerStates.INVULERABLE:
            case PlayerStates.NORMAL:
            default:
                // Move the player
                playerMovement.Movement();
                position = gameObject.transform.position;

                // Do dash mechanic if applicable
                playerMovement.DashMovement(playerMovement.lastMovedDirection);

                // Attack Mechanic
                playerAttack.Attack();

                // Interaction Mechanic
                playerInteraction.ActivateInteraction();

                break;
        }
    }
    public void GetGold(int goldToGive)
    {
        gold += goldToGive;
        goldTextDisplay.text = gold.ToString();
    }
    public void RemoveGold(int goldToTake)
    {
        if (gold >= goldToTake)
        {
            gold -= goldToTake;
            goldTextDisplay.text = gold.ToString();
        }
        else
        {
            Debug.Log("Not Enough Gold");
        }
    }

    public void GetDamaged()
    {
        if (currentPlayerState == PlayerStates.INVULERABLE) return;

         // Deincrement health
         health--;

        // Check if health is 0
        if (health <= 0)
        {
            // If it is, display game over for now
            Debug.Log("GAME OVER");

            SceneManager.LoadScene(0);
        }

        // Change the color of hearts image to white (just testing for now)
        heartImage[health].color = Color.black;

        if (currentPlayerState != PlayerStates.INVULERABLE)
                StartCoroutine(StartIFrames(1));
    }

    public void RecoverHealth()
    {
        if (health >= maxHealth)
        {
            Debug.Log("FULL HEALTH");
        }
        else
        {
            heartImage[health].color = Color.white;
            health++;
        }
    }

    public IEnumerator StartIFrames(float seconds)
    {
        currentPlayerState = PlayerStates.INVULERABLE;

        yield return new WaitForSeconds(seconds);

        currentPlayerState = PlayerStates.NORMAL;
    }
}
