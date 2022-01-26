using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerBrain playerBrain;

    public void BuyHealth()
    {
        if (playerBrain.health >= playerBrain.maxHealth)
        {
            playerBrain.RemoveGold(5);

            // May want to edit this to make it increasing max health points
            playerBrain.RecoverHealth();
        }
    }

    public void BuyAttack()
    {
        playerBrain.RemoveGold(5);

        // TODO: Increase attack somehow
    }

    public void ExitShop()
    {
        playerBrain.playerInteraction.interactObject.EndInteraction();
    }
}