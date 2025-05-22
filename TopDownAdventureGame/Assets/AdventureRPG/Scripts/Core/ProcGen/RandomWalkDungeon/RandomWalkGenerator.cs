using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Rand = System.Random;

public class RandomWalkGenerator : MonoBehaviour
{
    #region Components
    [SerializeField] RandomWalkSettings randomWalkSettings;
    [SerializeField] TileVisualizer tileVisualizer;
    [SerializeField] Button testGenButton;
    #endregion

    #region RNG Factors
    [field: SerializeField] public bool StartRandomlyEachIteration { get; private set; } = true;
    #endregion

    void Start()
    {
        CreateDungeon();
    }

    public void CreateDungeon()
    {
        var floorPositions = RunRandomWalk();

        foreach (var pos in floorPositions)
        {
            tileVisualizer.ClearTiles();
            tileVisualizer.DrawFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tileVisualizer);
        }
    }

    protected HashSet<Vector3Int> RunRandomWalk()
    {
        var currentPos = randomWalkSettings.StartingPos;
        HashSet<Vector3Int> tilePositions = new HashSet<Vector3Int>();
        Rand rng = new Rand();

        for (int i = 0; i < randomWalkSettings.Iterations; i++)
        {
            var path = PCGAlgos.RandomWalk(currentPos, randomWalkSettings.WalkLength);
            tilePositions.UnionWith(path);

            if (StartRandomlyEachIteration)
            {
                currentPos = tilePositions.ElementAt(rng.Next(0, tilePositions.Count));
            }
        }

        return tilePositions;
    }
}
