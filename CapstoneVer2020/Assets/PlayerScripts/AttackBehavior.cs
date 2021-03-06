using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public PlayerAttack playerAttack;
    private float activeTime = .12f;

    public float hit;

    private void Update()
    {
        activeTime -= Time.deltaTime;

        // Stop player from moving whey they're attacking
        // This is disgusting, need to refactor after prototyping
        playerAttack.playerBrain.playerMovement.rigidbody.velocity = Vector2.zero;

        if (activeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
