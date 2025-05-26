using Unity.VisualScripting;
using UnityEngine;

public class Hallway
{
    Vector2Int _startPos;
    Vector2Int _endPos;

    HallwayDirection _startDir;
    HallwayDirection _endDir;

    Room _startRoom;
    Room _endRoom;

    public Room StartRoom { get => _startRoom; set => _startRoom = value; }
    public Room EndRoom { get => _endRoom;  set => _endRoom = value; }

    public Vector2Int StartPos
    {
        get => _startPos;
        set => _startPos = value;
    }

    public Vector2Int EndPos
    {
        get => _endPos;
        set => _endPos = value;
    }
    
    public RectInt Area
    {
        get
        {
            int x = Mathf.Min(StartPosAbsolute.x, EndPosAbolute.x);
            int y = Mathf.Min(StartPosAbsolute.y, EndPosAbolute.y);
            int width = Mathf.Max(1, Mathf.Abs(StartPosAbsolute.x - EndPosAbolute.x));
            int height = Mathf.Max(1, Mathf.Abs(StartPosAbsolute.y - EndPosAbolute.y));

            if (StartPosAbsolute.x == EndPosAbolute.x)
            {
                y++;
                height--;
            }
            if (StartPosAbsolute.y == EndPosAbolute.y)
            {
                x++;
                width--;
            }

            return new RectInt(x, y, width, height);
        }
    }

    public Vector2Int StartPosAbsolute { get => _startPos + _startRoom.Area.position; }
    public Vector2Int EndPosAbolute { get => _endPos + _endRoom.Area.position; }

    public HallwayDirection StartDir { get => _startDir; }
    public HallwayDirection EndDir { get => _endDir;  set => _endDir = value; }

    public Hallway(HallwayDirection startDir, Vector2Int startPos, Room startRoom = null)
    {
        this._startDir = startDir;
        this._startPos = startPos;
        this._startRoom = startRoom;
    }
}
