using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemy : EnemyBase
{
    public float moveForce;

    private void Update()
    {
        SeekPlayer();
        position = gameObject.transform.position;
    }

    private Vector2 SeekPlayer()
    {
        Vector2 moveDirection = (player.position - this.position).normalized * moveForce;

        // Apply movement to rigidbody
        rigidbody.velocity = moveDirection;

        Debug.Log(moveDirection);

        Debug.DrawLine(position, position + moveDirection.normalized * moveForce, Color.red);

        return moveDirection;
    }
}
