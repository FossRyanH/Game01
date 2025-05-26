using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class Room
{
    RectInt _area;
    public RectInt Area { get { return _area; } }

    public Room(RectInt area)
    {
        this._area = area;
    }

    public List<Hallway> CalcAllPossibleDoorways(int width, int height, int minDistFromEdge)
    {
        List<Hallway> hallwayCandidates = new List<Hallway>();

        int top = height - 1;
        int minX = minDistFromEdge;
        int maxX = width - minDistFromEdge;

        for (int x  = minX; x < maxX; x++)
        {
            hallwayCandidates.Add(new Hallway(HallwayDirection.Bottom, new Vector2Int(x, 0)));
            hallwayCandidates.Add(new Hallway(HallwayDirection.Top, new Vector2Int(x, top)));
        }

        int right  = width - 1;
        int minY = minDistFromEdge;
        int maxY = height - minDistFromEdge;

        for (int y = minY; y < maxY; y++)
        {
            hallwayCandidates.Add(new Hallway(HallwayDirection.Left, new Vector2Int(0, y)));
            hallwayCandidates.Add(new Hallway(HallwayDirection.Right, new Vector2Int(right, y)));
        }

        return hallwayCandidates;
    }
}
