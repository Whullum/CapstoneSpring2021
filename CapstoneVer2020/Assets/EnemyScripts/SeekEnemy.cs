using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemy : EnemyBase
{
    public float moveForce;
    public float maxSpeed;

    private void Update()
    {
        SeekPlayer();
        position = gameObject.transform.position;
    }

    private Vector2 SeekPlayer()
    {
        Vector2 moveDirection = (player.position - this.position).normalized * moveForce;

        // Apply movement to rigidbody
        rigidbody.AddForce(moveDirection);

        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);

        //Debug.Log(moveDirection);

        //Debug.DrawLine(position, position + moveDirection.normalized * moveForce, Color.red);

        return moveDirection;
    }
}
