using UnityEngine;
using System;
using System.Collections.Generic;

public enum HallwayDirection { Undefined, Left, Right, Top, Bottom };

public static class HallwayDirectionExtension 
{
    static readonly Dictionary<HallwayDirection, Color> DirectionColorMap = new Dictionary<HallwayDirection, Color>
    {
        { HallwayDirection.Left, Color.yellow },
        { HallwayDirection.Right, Color.magenta },
        { HallwayDirection.Top, Color.cyan },
        { HallwayDirection.Bottom, Color.green }
    };

    public static Color GetColor(this HallwayDirection direction)
    {
        return DirectionColorMap.TryGetValue(direction, out Color color) ? color : Color.grey;
    }

    public static HallwayDirection GetOppositeDirection(this HallwayDirection direction)
    {
        Dictionary<HallwayDirection, HallwayDirection> oppositeDirectionMap = new Dictionary<HallwayDirection, HallwayDirection>
        {
            { HallwayDirection.Left, HallwayDirection.Right },
            { HallwayDirection.Right, HallwayDirection.Left },
            { HallwayDirection.Top, HallwayDirection.Bottom },
            { HallwayDirection.Bottom, HallwayDirection.Top }
        };

        return oppositeDirectionMap.TryGetValue(direction, out HallwayDirection oppositeDirection) ? oppositeDirection : HallwayDirection.Undefined;
    }
}