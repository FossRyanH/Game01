using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Rand = System.Random;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector3Int> floorPositions, TileVisualizer tileVisualizer)
    {
        var basicWallPositions = FindWallInDirections(floorPositions, Direction3D.CardinalDirectionsList);

        foreach (var position in basicWallPositions)
        {
            tileVisualizer.DrawSingleWall(position);
        }
    }

    private static HashSet<Vector3Int> FindWallInDirections(HashSet<Vector3Int> floorPositions, List<Vector3Int> directionsList)
    {
        HashSet<Vector3Int> wallPositions = new HashSet<Vector3Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neighborPosition = position + direction;
                if (!floorPositions.Contains(neighborPosition))
                {
                    wallPositions.Add(neighborPosition);
                }
            }
        }

        return wallPositions;
    }
}