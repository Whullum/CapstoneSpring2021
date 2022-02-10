using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorTileInfo : MonoBehaviour
{
    public Tilemap floorTiles;
    public Bounds floorBounds;

    public Grid tileGrid;

    private void Start()
    {
        floorTiles = GetComponent<Tilemap>();
        floorBounds = floorTiles.localBounds;

        tileGrid = GetComponentInParent<Grid>();
    }
}
