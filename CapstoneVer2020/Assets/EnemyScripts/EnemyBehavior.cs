using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if enemy is colliding with a player hitbox object on the playerattack layer
        // TODO: When this gets more complex, make this a case/switch block
        if (collision.gameObject.layer == 7)
        {
            // Teleport Enemy for now just for testing
            gameObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0);
            Debug.Log("Attack Hitbox Collision");
            //Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            // If colliding witha player, damage the player
            collision.gameObject.GetComponent<PlayerBehavior>().GetDamaged();
            Debug.Log("Player Collision");
        }
    }
}
