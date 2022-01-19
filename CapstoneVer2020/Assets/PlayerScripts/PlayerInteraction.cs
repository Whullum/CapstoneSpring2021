using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interact Action")]
    public InputAction interactAction;

    // TODO: replace with interact entity class so that more than just signposts work
    public Signpost interactObject;

    private void Start()
    {
        interactAction.Enable();
    }

    private void Update()
    {
        // Interact with an object when the interact button has been pressed and an object exists to be interacted with
        if (interactAction.triggered)
        {
            if (interactObject != null)
            {
                Debug.Log("Insteraction dectected");
                interactObject.Interaction();
            }
            else
            {
                Debug.Log("No interaction");
            }
        }
    }
}
