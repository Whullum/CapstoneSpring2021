using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Action")]
    public InputAction attackAction;

    // Note: will be better to put into a PlayerBrain type script to handle communication between player scripts 
    public PlayerMovement playerMovement;

    public GameObject attackHitbox;
    public Vector3 boundsVector;

    // Start is called before the first frame update
    void Start()
    {
        // Enable attackAction for input
        attackAction.Enable();

        // Get bounds input to spawn in the correct location
        boundsVector = GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        // Attack behavior
        Attack();
    }

    /// <summary>
    /// Sword swipe attack behavior
    /// TODO: Add rotation behavior for attacking in different directions
    /// </summary>
    private void Attack()
    {
        if (attackAction.triggered)
        {
            // Instantiate the hitbox prefab
            GameObject hitbox = Instantiate(attackHitbox, gameObject.transform.position + (Vector3)playerMovement.spriteDirection, Quaternion.identity);
            
            // Rotate hitbox in the correct direction
            hitbox.transform.rotation = Quaternion.Euler(0, 0, playerMovement.rotationAngle);
            
            // Set reference to this script in the hitbox prefab
            hitbox.GetComponent<AttackBehavior>().playerAttack = this;

            //GameObject hitbox = Instantiate(attackHitbox, gameObject.transform.position + boundsVector, Quaternion.Euler((Vector3)playerMovement.lastMovedDirection));
            //Debug.Log("Attack!");
        }
    }
}
