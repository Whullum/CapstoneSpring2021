using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public DungeonRoomList dungeonRoomList;
    public GameObject currentDungeonRoom;

    private void Start()
    {
        // Spawn a new dungeon room on startup
        SpawnNewDungeonRoom();
    }

    /// <summary>
    /// Spawns a new dungeon room
    /// </summary>
    public void SpawnNewDungeonRoom()
    {
        // get a random number to instantiate a random dungeon room
        int dungeonRoomIndex = Random.Range(0, 3);

        // Instantiate a new dungeon room
        currentDungeonRoom = Instantiate(dungeonRoomList.dungeonRooms[dungeonRoomIndex]);

        // Loop through each of the dungeon exits in the map and set this as the dungeonManager for each DungeonExitScript component
        foreach (DungeonExitScript exitScript in currentDungeonRoom.GetComponentsInChildren<DungeonExitScript>())
        {
            exitScript.dungeonManager = this;
        }
    }
}
