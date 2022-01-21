using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interact Action")]
    public InputAction interactAction;
    public Entity interactObject;
    public PlayerBrain playerBrain;
    public bool currentlyInteracting;


    private void Start()
    {
        interactAction.Enable();
        currentlyInteracting = false;
    }

    public void ActivateInteraction()
    {
        // Interact with an object when the interact button has been pressed and an object exists to be interacted with
        if (interactAction.triggered)
        {
            if (interactObject != null)
            {
                Debug.Log("Interaction dectected");
                interactObject.Interaction();
            }
            else
            {
                Debug.Log("No interaction");
            }
        }
    }
}
