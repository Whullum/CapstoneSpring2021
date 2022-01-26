using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Bertiz
// Signpost.cs
// A static object that displays text to the player when interacted with (player presses the E key)
// 6/14/2020 (Edited 1/19/2020)

public class Signpost : Entity
{
    [SerializeField]
    private DialougeManager dialogueManager;    // Reference to the dialogue script 
    public TextAsset textFile;          // The text file that holds all the string data for dialogue

    // Start is called before the first frame update
    void Start()
    {
        activated = false;                                          // Set activated to false to start off the object
        dialogueManager.textFile = this.textFile;                   // Set the dialogue script's text file to the one given in this object
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        //Debug.Log("SIGN - recieved player");

    //        playerInteraction = collision.gameObject.GetComponent<PlayerInteraction>();
    //        playerInteraction.interactObject = this;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        //Debug.Log("SIGN - exited player");
    //        playerInteraction = null;
    //    }
    //}

    /// <summary>
    /// Unhides the textbox to the player and displays text on the screen when interacted with
    /// </summary>
    public override void Interaction()
    {
        // Check if the object has been activated yet or not
        if (activated == true)
        {
            // if it has, continue with typing logic
            dialogueManager.NextSentence();
        }
        else
        {
            // Initialize dialouge stuff

            // Let the player know it's interacting with something
            playerInteraction.playerBrain.currentPlayerState = PlayerStates.INTERACTING;

            // Set activated to true
            activated = true;

            dialogueManager.ClearData();
            dialogueManager.LoadDataFromFile(textFile);
            dialogueManager.currentEntity = this;

            // Unhide the dialogueWindow and reveal the dialogue window
            dialogueManager.textDisplay.gameObject.SetActive(true);

            // Start the typing coroutine in Dialogue
            StartCoroutine(dialogueManager.Type());
        }
    }

    public override void EndInteraction()
    {
        if (!dialogueManager.done)
        {
            dialogueManager.SkipButton();
        }

        activated = false;
        playerInteraction.playerBrain.currentPlayerState = PlayerStates.NORMAL;
        //playerInteraction.currentlyInteracting = false;
    }
}