using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public DungeonRoomList dungeonRoomList;
    public GameObject currentDungeonRoom;
    public EnemyManager enemyManager;

    public Bounds floorBounds;
    public Vector3 cellSize;

    private void Start()
    {
        // Spawn a new dungeon room on startup
        //SpawnNewDungeonRoom();

        floorBounds = currentDungeonRoom.GetComponentInChildren<FloorTileInfo>().floorBounds;
        cellSize = currentDungeonRoom.GetComponentInChildren<FloorTileInfo>().tileGrid.cellSize;

        enemyManager = gameObject.GetComponent<EnemyManager>();
    }

    /// <summary>
    /// Spawns a new dungeon room
    /// </summary>
    public void SpawnNewDungeonRoom()
    {
        // get a random number to instantiate a random dungeon room
        int dungeonRoomIndex = Random.Range(0, dungeonRoomList.dungeonRooms.Length);

        // Instantiate a new dungeon room
        currentDungeonRoom = Instantiate(dungeonRoomList.dungeonRooms[dungeonRoomIndex]);

        enemyManager.Init();

        // Loop through each of the dungeon exits in the map and set this as the dungeonManager for each DungeonExitScript component
        foreach (DungeonExitScript exitScript in currentDungeonRoom.GetComponentsInChildren<DungeonExitScript>())
        {
            exitScript.dungeonManager = this;
        }

        floorBounds = currentDungeonRoom.GetComponentInChildren<FloorTileInfo>().floorBounds;
        cellSize = currentDungeonRoom.GetComponentInChildren<FloorTileInfo>().tileGrid.cellSize;
    }
}
