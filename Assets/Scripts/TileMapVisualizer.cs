using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    public Tilemap floorTilemap, wallTileMap, secondWallTileMap;
    [SerializeField]
    public TileBase[] floorTiles, wallTop, secondWall;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorpositions)
    {
        PaintTiles(floorpositions, floorTilemap, floorTiles[0]);
        PaintTiles(floorpositions, wallTileMap, wallTop[0]);
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTileMap, wallTop[0], position);
        PaintSingleTile(secondWallTileMap, secondWall[0], position);
    }

    internal void PaintSecondBasicWall(Vector2Int position)
    {
        PaintSingleTile(secondWallTileMap, secondWall[0], position);
    }
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        secondWallTileMap.ClearAllTiles();
    }
}
