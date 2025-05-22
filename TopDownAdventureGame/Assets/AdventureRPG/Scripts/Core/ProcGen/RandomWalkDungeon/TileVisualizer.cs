using UnityEngine;
using System;
using Rand = System.Random;
using System.Collections.Generic;

public class TileVisualizer : MonoBehaviour
{
    #region Tiles and Pieces
    [field: SerializeField] public GameObject floorPrefab { get; private set; }
    [field: SerializeField] public GameObject wallPrefab { get; private set; }
    List<GameObject> _instantiatedTiles = new();
    #endregion

    public void DrawFloorTiles(IEnumerable<Vector3Int> tiles)
    {
        if (floorPrefab == null)
        {
            Debug.LogError($"{nameof(floorPrefab)}: Is not assigned.");
            return;
        }

        DrawTiles(tiles, floorPrefab);
    }

    private void DrawTiles(IEnumerable<Vector3Int> positions, GameObject tilePrefab)
    {
        foreach (var position in positions)
        {
            DrawSingleTile(tilePrefab, position);
        }
    }

    private void DrawSingleTile(GameObject tilePrefab, Vector3Int position)
    {
        Vector3 worldPos = new Vector3(position.x, position.y, position.z);
        GameObject tile = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform.parent);
        _instantiatedTiles.Add(tile);
    }

    public void DrawSingleWall(Vector3Int position)
    {
        DrawSingleTile(wallPrefab, position);
    }


    public void ClearTiles()
    {
        foreach (var tile in _instantiatedTiles)
        {
            if (tile != null)
            {
                Destroy(tile);
            }
        }

        _instantiatedTiles.Clear();
    }
}