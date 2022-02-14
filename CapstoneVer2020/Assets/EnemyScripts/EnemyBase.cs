using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyManager enemyManager;

    public int enemyHealth;
    public int goldAwarded;

    public PlayerBrain player;
    public Rigidbody2D rigidbody;

    public Vector2 position;

    public virtual void Init()
    {
        player = enemyManager.player;
        position = gameObject.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if enemy is colliding with a player hitbox object on the playerattack layer
        // TODO: When this gets more complex, make this a case/switch block
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("Attack Hitbox Collision");

            // Teleport Enemy for now just for testing
            //gameObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0);
            collision.gameObject.GetComponent<AttackBehavior>().playerAttack.playerBrain.GetGold(goldAwarded);
            enemyHealth--;

            // Knockback
            rigidbody.AddForce((this.position - player.position) * 2000);

            if (enemyHealth <= 0)
            {
                enemyManager.enemies.Remove(this);
                Destroy(gameObject);
            }
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            // If colliding with a player, damage the player
            collision.gameObject.GetComponent<PlayerBrain>().GetDamaged();
            //Debug.Log("Player Collision");
        }
    }
}
