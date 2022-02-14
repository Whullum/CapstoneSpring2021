using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExitScript : MonoBehaviour
{
    public DungeonManager dungeonManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            // Unload the current dungeon room
            Destroy(dungeonManager.currentDungeonRoom);

            // Unload the current dungeon room's entities
            //TODO

            // Move player to correct position
            collision.gameObject.transform.position = new Vector3(0, -6, -1);

            // Load in the next dungeon room
            //dungeonManager.currentDungeonRoom = Instantiate(dungeonManager.dungeonRooms.dungeonPrefabs[Random.Range(0, 3)]);
            dungeonManager.SpawnNewDungeonRoom();

            // Load in next dungeon room's entities

            Debug.Log("Exit Player Collision");
        }
    }
}
