using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Entity
{
    [SerializeField]
    private GameObject shopWindow;  // Reference to the dialogueWindow to control when it appears/dissapears

    private void Start()
    {
        shopWindow.SetActive(false);
    }

    public override void Interaction()
    {
        if (!activated)
        {
            shopWindow.SetActive(true);
            playerInteraction.playerBrain.currentPlayerState = PlayerStates.INTERACTING;
            activated = true;
        }
    }

    public override void EndInteraction()
    {
        activated = false;
        shopWindow.SetActive(false);
        playerInteraction.playerBrain.currentPlayerState = PlayerStates.NORMAL;
    }
}
