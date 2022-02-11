using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Actions")]
    public InputAction moveActions;
    [Header("Dash Action")]
    public InputAction dashAction;

    [SerializeField]
    public Rigidbody2D rigidbody;

    public PlayerBrain playerBrain;

    // Basic movement variables
    private const float moveForce = 5.0f;
    private Vector3 facingDirectionVector;

    // Dash mechanic variables
    private const float dashForce = 2.3f;
    private const float dashTimeStart = .1f;
    private float dashTimeCurrent;
    private Vector2 dashDirection;
    private bool isDashing;

    // Position variables
    //private Vector2 currentPosition;
    private Vector2 moveDirection;
    public Vector2 lastMovedDirection = new Vector2(1, 0);
    public Vector2 spriteDirection;
    public float rotationAngle = 0;

    private void Start()
    {
        // Enable actions for movement
        moveActions.Enable();
        dashAction.Enable();
    }

    /// <summary>
    /// Movement behavior for player
    /// </summary>
    /// <returns> The vector that the player is moving towards </returns>
    public Vector2 Movement()
    {
        // Get last moved direction when moving or stopped moving
        // Done to preserve direction for attacks/graphics/movement
        if (moveActions.IsPressed())
        {
            lastMovedDirection = moveDirection;

            //Debug.Log(moveActions.activeControl.name);
        }

        // Get current position
        playerBrain.position = gameObject.transform.position;

        // Apply movement input with moveFOrce to get movement vector
        moveDirection = moveActions.ReadValue<Vector2>() * moveForce;

        // Apply movement to rigidbody
        rigidbody.velocity = moveDirection;

        // Rotation code for sprite direction, will replace when sprites come in
        if (rigidbody.velocity.x > 0)
        {
            rotationAngle = 0;
            spriteDirection = new Vector2(1, 0);
        }
        else if (rigidbody.velocity.x < 0)
        {
            rotationAngle = 180;
            spriteDirection = new Vector2(-1, 0);
        }

        // Rotation code for sprite direction, will replace when sprites come in
        if (rigidbody.velocity.y > 0)
        {
            rotationAngle = 90;
            spriteDirection = new Vector2(0, 1);
        }
        else if (rigidbody.velocity.y < 0)
        {
            rotationAngle = 270;
            spriteDirection = new Vector2(0, -1);
        }

        // Rotation for diagonals
        // rotationAngle = Vector2.SignedAngle(Vector2.right, lastMovedDirection);

        // Rotate the player correctly
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationAngle - 90);

        // Draw debug line for testing
        Debug.DrawLine(playerBrain.position, playerBrain.position + lastMovedDirection.normalized * moveForce, Color.green);

        return playerBrain.position + lastMovedDirection.normalized * moveForce;
    }

    /// <summary>
    /// Behavior for dash mechanic
    /// </summary>
    /// <param name="moveDirection"> Direction in which to dash towards</param>
    // Reference: https://www.youtube.com/watch?v=G3cGpnuzVHU
    public void DashMovement(Vector2 moveDirection)
    {
        // Check if the player is dashing
        if (dashAction.triggered)
        {
            // Set isDashing to true for cooldown and set cooldown timer
            isDashing = true;
            dashTimeCurrent = dashTimeStart;
        }

        // Chek if player is in the middle of dashing
        if (isDashing)
        {
            // Add force to the rigidbody in direction of dash
            if (moveActions.IsPressed())
            {
                // Dash in the direction of velocity if movemnet buttons are being pressed
                rigidbody.AddForce(rigidbody.velocity * dashForce, ForceMode2D.Impulse);
            }
            else
            {
                // Dash in the last moved direction if movement buttons are not being pressed
                rigidbody.AddForce(lastMovedDirection.normalized * dashForce * moveForce, ForceMode2D.Impulse);
            }

            // Deincrement dash time for cooldown
            dashTimeCurrent -= Time.deltaTime;

            // Reset cooldown then dashTImeCurrent is 0
            if (dashTimeCurrent <= 0)
            {
                isDashing = false;
            }
        }
    }

    public void StopMovement()
    {
        // Apply movement input with moveFOrce to get movement vector
        moveDirection = moveActions.ReadValue<Vector2>() * 0;

        // Apply movement to rigidbody
        rigidbody.velocity = moveDirection;
    }
}
