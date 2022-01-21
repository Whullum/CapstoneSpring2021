using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Bertiz
// Signpost.cs
// A static object that displays text to the player when interacted with (player presses the E key)
// 6/14/2020 (Edited 1/19/2020)

public class Signpost : MonoBehaviour
{
    public TextAsset textFile;          // The text file that holds all the string data for dialogue

    private bool activated;             // Determines if the object has already been interacted with before. Prevents multiple instances of one action when holding down a key
    private GameObject dialogueWindow;  // Reference to the dialogueWindow to control when it appears/dissapears

    [SerializeField]
    private DialougeManager dialogueManager;    // Reference to the dialogue script 

    private PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        dialogueWindow = dialogueManager.textDisplay.gameObject;    // Get the dialogueWindow
        dialogueWindow.SetActive(false);

        activated = false;                                          // Set activated to false to start off the object
        dialogueManager.textFile = this.textFile;                   // Set the dialogue script's text file to the one given in this object
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //Debug.Log("SIGN - recieved player");

            playerInteraction = collision.gameObject.GetComponent<PlayerInteraction>();
            playerInteraction.interactObject = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //Debug.Log("SIGN - exited player");
            playerInteraction = null;
        }
    }

    /// <summary>
    /// Unhides the textbox to the player and displays text on the screen when interacted with
    /// </summary>
    public void Interaction()
    {
        // Check if the object has been activated yet or not
        if (activated == true)
        {
            // if it has, continue with typing logic
            dialogueManager.NextSentence();

            // If the dialogue is done, then hide the dialogueWindow again
            // NOTE: instead of checking every frame, might be better to just have a single event when dialouge ends
            if (dialogueManager.done == true)
            {
                activated = false;
                playerInteraction.currentlyInteracting = false;
                dialogueWindow.SetActive(false);
                dialogueManager.ClearData();
            }
        }
        else
        {
            // Let the player know it's interacting with something
            playerInteraction.currentlyInteracting = true;

            // Set activated to true
            activated = true;

            dialogueManager.ClearData();
            dialogueManager.LoadDataFromFile(textFile);

            // Unhide the dialogueWindow and reveal the dialogue window
            dialogueWindow.SetActive(true);

            // Start the typing coroutine in Dialogue
            StartCoroutine(dialogueManager.Type());
        }
    }
}
