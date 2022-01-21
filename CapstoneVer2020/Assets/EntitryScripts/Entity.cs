using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected bool activated;                       // Determines if the object has already been interacted with before. Prevents multiple instances of one action when holding down a key
    protected PlayerInteraction playerInteraction;    // Reference to player interaction input controls

    // Every entity can be interacted with in different ways
    public abstract void Interaction();

    // Entities can have ending actionas when interaction is complete
    public abstract void EndInteraction();
}
