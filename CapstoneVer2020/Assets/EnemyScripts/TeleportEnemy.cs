using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TeleportEnemy : EnemyBase
{
    [SerializeField]
    private Bounds roomBounds;

    [SerializeField]
    private Vector3 tilePadding;

    [SerializeField]
    private Bounds spriteBounds;

    public InputAction debugInput;

    public override void Init()
    {
        base.Init();

        roomBounds = enemyManager.gameObject.GetComponent<DungeonManager>().floorBounds;
        tilePadding = enemyManager.gameObject.GetComponent<DungeonManager>().cellSize;
        spriteBounds = gameObject.GetComponent<SpriteRenderer>().bounds;

        debugInput.Enable();

        Debug.Log("Teleport Init");
    }

    private void Update()
    {
        if (debugInput.triggered)
        {
            TeleportEnemyBehavior();
        }

        position = gameObject.transform.position;
    }

    private void TeleportEnemyBehavior()
    {
        float randomX = Random.Range(
            -roomBounds.extents.x + (tilePadding.x + spriteBounds.extents.x), 
            roomBounds.extents.x - (tilePadding.x + spriteBounds.extents.x));

        float randomY = Random.Range(
            -roomBounds.extents.y + (tilePadding.y + spriteBounds.extents.y), 
            roomBounds.extents.y - (tilePadding.y + spriteBounds.extents.y));

        Vector2 randomPosition = new Vector2(randomX, randomY);

        gameObject.transform.position = randomPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        TeleportEnemyBehavior();
    }
}
