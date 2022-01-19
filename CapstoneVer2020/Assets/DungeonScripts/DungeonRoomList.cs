using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonRoomList", menuName = "ScriptableObjects/DungeonRoomList", order = 1)]
public class DungeonRoomList : ScriptableObject
{
    // List of all the dungeon rooms in this particuar list
    public GameObject[] dungeonRooms;
}
