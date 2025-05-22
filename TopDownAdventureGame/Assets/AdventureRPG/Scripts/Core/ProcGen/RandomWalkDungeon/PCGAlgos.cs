using UnityEngine;
using System;
using Rand = System.Random;
using System.Collections.Generic;

public class PCGAlgos : MonoBehaviour
{
    public static Rand Rng = new Rand();

    static void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int j = Rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public static HashSet<Vector3Int> RandomWalk(Vector3Int startingPos, int walkLength)
    {
        HashSet<Vector3Int> path = new HashSet<Vector3Int>();
        List<Vector3Int> pathList = new List<Vector3Int>();
        Vector3Int currentPos = startingPos;

        path.Add
    }
}
