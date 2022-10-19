using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class WallsGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.eightDirectionsList);
        foreach(var position in basicWallPositions)
        {
            tileMapVisualizer.PaintSingleBasicWall(position);
        }
        var secondWallPositions = FindWallsOnTop(basicWallPositions);
        foreach (var position in secondWallPositions)
        {
            tileMapVisualizer.PaintSecondBasicWall(position);
        }
        //Debug.Log(tileMapVisualizer.secondWallTileMap.ContainsTile(tileMapVisualizer.floorTiles[0]));
    }

    private static HashSet<Vector2Int> FindWallsOnTop(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            var neighbourPosition = position + Direction2D.eightDirectionsList[0];
            if (!floorPositions.Contains(neighbourPosition))
            {
                wallPositions.Add(neighbourPosition);
            }
        }
        return wallPositions;
    }
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var position in floorPositions)
        {
            foreach(var direction in directionsList)
            {
                var neighbourPosition = position + direction;
                if (!floorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
