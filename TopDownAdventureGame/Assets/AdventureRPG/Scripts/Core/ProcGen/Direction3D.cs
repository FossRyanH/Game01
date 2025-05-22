using System;
using UnityEngine;
using System.Collections.Generic;
using Rand = System.Random;

public static class Direction3D
{
    public static readonly List<Vector3Int> CardinalDirectionsList = new List<Vector3Int>
    {
        Vector3Int.back,
        Vector3Int.forward,
        Vector3Int.left,
        Vector3Int.right
    };

    public static Vector3Int GetRandomDirection(Rand rng)
    {
        int dir = rng.Next(4);

        return dir switch
        {
            0 => Vector3Int.forward,
            1 => Vector3Int.back,
            2 => Vector3Int.left,
            3 => Vector3Int.right,
            _ => Vector3Int.zero
        };
    }
}