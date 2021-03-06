using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// By: Will Bertiz
// Displays text in a scene by typing it all out one character at a time
// Gets dialogue data from a .dialogue file and loads it into a dialogue array for display
// 5/31/2020 (Edited 1/19/2022)

// Reference: https://gamedev.stackexchange.com/Questions/138485/how-to-make-a-text-box-where-text-types-smoothly
// Reference: https://www.youtube.com/watch?v=f-oSXg6_AMQ
// Unity File IO Reference: https://support.unity3d.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-

public class DialougeManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;      // TextMeshPro object to display text
    public TextAsset textFile;               // The text file to use for dialouge
    public List<string> dialogue;            // Array of dialouge that will be shown to the player

    public Entity currentEntity;             // The entity that is being interacted with
    
    public float typingSpeed = .1f;          // Speed of typing
    
    public bool done;                        // Determines if the entire dialogue is finished or not
    public Button skipButton;
    
    private int index;                       // Index of current sentece that is being displayed
    private bool finished;                   // Determines if text has finished typing

    public int Index { get => index; set => index = value; }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize values
        dialogue = new List<string>();
        textDisplay.text = "";
        finished = false;
        //done = false;

        textDisplay.gameObject.SetActive(false);

        //instance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Dialouge/eklee-KeyPressMac04");
    
        // Load text file data from a test file
        LoadDataFromFile(textFile);
    }
    
    /// <summary>
    ///  Types out a sentence to a TextMeshPro object in the scene
    /// </summary>
    /// <returns></returns>
    public IEnumerator Type()
    {
        // Loop through each character in the current sentence being shwon
        foreach (char letter in dialogue[index].ToCharArray())
        {
            // If the text has not finished typing, continue with typing logic. Otherwise, text will not type
            // This is done to prevent threading issues when spamming the continue button
            if (!finished)
            {
                // Add a letter to the textMeshPro object
                textDisplay.text += letter;
    
                // Set finished to true if the dialouge has finished typing
                if (dialogue[index] == textDisplay.text)
                {
                    finished = true;
                }
            }
            // if the text has finished typing, break out of the loop to prevent further typing
            // This stops coroutine/threading issues with jumbled up text 
            else
            {
                break;
            }
    
            // Yield return to wait before typing out the next letter in the sentence
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    /// <summary>
    /// Loads in data from a .txt file
    /// </summary>
    public void LoadDataFromFile(TextAsset textFile)
    {
        //done = false;

        // Create a StringReader to read data from the text file
        StringReader reader = new StringReader(textFile.text);
    
        string line;    // A single line in the string data (line - text before/after a line break)
    
        // Create a loop to read all the lines in the txt file
        while (true)
        {
            // Read a single line from the reader
            line = reader.ReadLine();
    
            // Check if the string from line is empty or not
            if (!(string.IsNullOrEmpty(line)))
            {
                // If there is data in the line, add it to the dialouge list
                dialogue.Add(line);
            }
            else
            {
                // Otherwise, break out of the loop
                break;
            }
        }
    }

    /// <summary>
    /// Loads in dialouge data from a string array
    /// </summary>
    /// <param name="dialougeArray"> An array of strings to pull dialouge from </param>
    public void LoadDataFromStringArray(string[] dialougeArray)
    {
        foreach(string line in dialougeArray)
        {
            // Check if the string from line is empty or not
            if (!(string.IsNullOrEmpty(line)))
            {
                // If there is data in the line, add it to the dialouge list
                dialogue.Add(line);
            }
            else
            {
                // Otherwise, break out of the loop
                break;
            }
        }
    }
    
    /// <summary>
    /// Clears dialouge data from the object
    /// </summary>
    public void ClearData()
    {
        dialogue.Clear();
        finished = false;
        index = 0;
        textDisplay.text = "";

        textDisplay.gameObject.SetActive(false);
    }

    /// <summary>
    /// Skips the current sentence and goes onto the nexrt one
    /// </summary>
    public void SkipSentece()
    {
        textDisplay.text = dialogue[index];
        finished = true;
        StopCoroutine(Type());
    }
    
    /// <summary>
    /// Starts the next sectence in the dialouge array
    /// </summary>
    public void NextSentence()
    {
        // If the current sentence has finished typing, continue on to the next sentence
        if (finished)
        {
            // Check to make sure that there is still dialouge to type
            if (index < dialogue.Count - 1)
            {
                finished = false;           // Set finished to false
                index++;                    // Increment index to go to the next sentence
                textDisplay.text = "";      // Reset the text display to blank text

                Debug.Log(index + " " + dialogue[index]);

                StartCoroutine(Type());     // Start the coroutine again
            }
            else
            {
                // If there is no more dialogue, display an empty text string and set done to true
                finished = false;
                textDisplay.text = "";
                index = 0;

                textDisplay.gameObject.SetActive(false);
                ClearData();

                StopCoroutine(Type());

                currentEntity.EndInteraction();
            }
        }
        // If the current sentence is not finished, display the complete text and set finished to true
        else
        {
            SkipSentece();
        }
    }

    /// <summary>
    /// Skip button skips all the dialouge for the speaker
    /// </summary>
    public void SkipButton()
    {
        Debug.Log(index + " " + dialogue[index]);

        finished = true;            // mark this sentence as finished
        textDisplay.text = "";      // reset the text display to show no text
        index = dialogue.Count - 1; // go to the end of the dialouge array

        Debug.Log(index + " " + dialogue[index]);

        NextSentence();
    }
}
