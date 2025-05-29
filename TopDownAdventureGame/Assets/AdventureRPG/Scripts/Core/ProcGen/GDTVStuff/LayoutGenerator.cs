using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    [field: SerializeField] public RoomLevelLayoutSO RoomLevelLayout { get; set; }
    int _seed;

    [SerializeField] GameObject _levelLayoutDisplay;
    [SerializeField] List<Hallway> _openDoorways;

    System.Random _random;
    Level _level;

    [ContextMenu("Generate Level Layout")]
    public void GenerateLevel()
    {
        _random = new System.Random(_seed);
        var roomRect = GetStartRoomRect();
        _openDoorways = new List<Hallway>();
        _level = new Level(RoomLevelLayout.Width, RoomLevelLayout.Height);

        Room room = new Room(roomRect);
        List<Hallway> hallways = room.CalcAllPossibleDoorways(room.Area.width, room.Area.height, RoomLevelLayout.DoorDistanceFromEdge);

        hallways.ForEach((h) => h.StartRoom = room );
        hallways.ForEach((h) => _openDoorways.Add(h));
        _level.AddRoom(room);

        Hallway selectedEntryway = _openDoorways[_random.Next(_openDoorways.Count)];

        AddRooms();

        DrawLayout(selectedEntryway, roomRect);
    }

    [ContextMenu("Generate New Seed")]
    public void GeneratedNewSeed()
    {
        _seed = Environment.TickCount;
    }

    [ContextMenu("Generate World And Seed")]
    public void GenerateTheWorld()
    {
        GeneratedNewSeed();
        GenerateLevel();
    }

    RectInt GetStartRoomRect()
    {
        int roomWidth = _random.Next(RoomLevelLayout.RoomWidthMin, RoomLevelLayout.RoomWidthMax);
        int availableWidthX = RoomLevelLayout.Width / 2 - roomWidth;
        int randomX = _random.Next(0, availableWidthX);
        int roomX = randomX + (RoomLevelLayout.Width / 4);

        int roomLength = _random.Next(RoomLevelLayout.RoomLengthMin, RoomLevelLayout.RoomLengthMax);
        int availableHeightY = RoomLevelLayout.Height / 2 - roomLength;
        int randomY = _random.Next(0, availableHeightY);
        int roomY = randomY + (RoomLevelLayout.Height / 4);

        return new RectInt(roomX, roomY, roomWidth, roomLength);
    }

    void DrawLayout(Hallway selectedEntryway = null, RectInt roomCandidateRect = new RectInt(), bool isDebug = false)
    {
        var renderer = _levelLayoutDisplay.GetComponent<Renderer>();
        var layoutTexture = (Texture2D)renderer.sharedMaterial.mainTexture;

        layoutTexture.Reinitialize(RoomLevelLayout.Width, RoomLevelLayout.Height);
        _levelLayoutDisplay.transform.localScale = new Vector3(RoomLevelLayout.Width, RoomLevelLayout.Height, 1);
        layoutTexture.FillWithColor(Color.black);

        Array.ForEach(_level.Rooms, room => layoutTexture.DrawRectangle(room.Area, Color.white));
        Array.ForEach(_level.Hallways, hallway => layoutTexture.DrawLine(hallway.StartPosAbsolute, hallway.EndPosAbolute, Color.white));

        if (isDebug)
        {
            layoutTexture.DrawRectangle(roomCandidateRect, new Color(0.75f, 0.55f, 1f));
            _openDoorways.ForEach((hallway) => layoutTexture.SetPixel(hallway.StartPosAbsolute.x, hallway.StartPosAbsolute.y, hallway.StartDir.GetColor()));
        }

        if (isDebug && selectedEntryway != null)
        {
            layoutTexture.SetPixel(selectedEntryway.StartPosAbsolute.x, selectedEntryway.StartPosAbsolute.y, Color.red);
        }

        layoutTexture.SaveAsset();
    }

    Hallway SelectHallwayCandidate(RectInt roomCandidateRect, Hallway entryWay)
    {
        Room room = new Room(roomCandidateRect);
        List<Hallway> candidates = room.CalcAllPossibleDoorways(room.Area.width, room.Area.height, RoomLevelLayout.DoorDistanceFromEdge);
        HallwayDirection requiredDirection = entryWay.StartDir.GetOppositeDirection();
        List<Hallway> filteredHallwayCandidates = candidates.Where(hallwayCandidate => hallwayCandidate.StartDir == requiredDirection).ToList();

        return filteredHallwayCandidates.Count > 0 ? filteredHallwayCandidates[_random.Next(filteredHallwayCandidates.Count)] : null;
    }

    Vector2Int CalculateRoomPos(Hallway entryway, int roomWidth, int roomLength, int distance, Vector2Int endPos)
    {
        Vector2Int roomPos = entryway.StartPosAbsolute;
        switch (entryway.StartDir)
        {
            case HallwayDirection.Left:
                roomPos.x -= distance + roomWidth;
                roomPos.y -= endPos.y;
                break;
            case HallwayDirection.Top:
                roomPos.x -= endPos.x;
                roomPos.y += distance + 1;
                break;
            case HallwayDirection.Right:
                roomPos.x += distance + 1;
                roomPos.y -= endPos.y;
                break;
            case HallwayDirection.Bottom:
                roomPos.x -= endPos.x;
                roomPos.y -= distance + roomLength;
                break;
            default:
                Debug.LogError("Invalid direction");
                break;
        }
        return roomPos;
    }

    Room BuildAdjacentRoom(Hallway selectedEntryway)
    {
        RectInt roomCandidateRect = new RectInt
        {
            width = _random.Next(RoomLevelLayout.RoomWidthMin, RoomLevelLayout.RoomWidthMax),
            height = _random.Next(RoomLevelLayout.RoomLengthMin, RoomLevelLayout.RoomLengthMax)
        };
        
        Hallway selectedExit = SelectHallwayCandidate(roomCandidateRect, selectedEntryway);
        
        if (selectedExit == null) return null;
        
        int distance = _random.Next(RoomLevelLayout.HallwayLengthMin, RoomLevelLayout.HallwayLengthMax + 1);
        Vector2Int roomCandidatePos = CalculateRoomPos(selectedEntryway, roomCandidateRect.width, roomCandidateRect.height, distance, selectedExit.StartPos);
        roomCandidateRect.position = roomCandidatePos;
        Room newRoom = new Room(roomCandidateRect);

        if (!IsRoomCandidateValid(roomCandidateRect)) return null;

        selectedEntryway.EndRoom = newRoom;
        selectedEntryway.EndPos = selectedExit.StartPos;

        return newRoom;
    }

    bool IsRoomCandidateValid(RectInt roomCandidateRect)
    {
        RectInt levelRect = new RectInt(1, 1, RoomLevelLayout.Width - 2, RoomLevelLayout.Height - 2);

        return levelRect.Contains(roomCandidateRect) && !CheckRoomOverlap(roomCandidateRect, _level.Rooms, _level.Hallways, _random.Next(RoomLevelLayout.MinRoomDistance, RoomLevelLayout.MaxRoomDistance));
    }

    bool CheckRoomOverlap(RectInt roomCandidateRect, Room[] rooms, Hallway[] hallways, int minRoomDist)
    {
        RectInt paddedRoomRect = new RectInt
        {
            x = roomCandidateRect.x - minRoomDist,
            y = roomCandidateRect.y - minRoomDist,
            width = roomCandidateRect.width + 2 * minRoomDist,
            height = roomCandidateRect.height + 2 * minRoomDist
        };

        foreach (Room room in rooms)
        {
            if (paddedRoomRect.Overlaps(room.Area))
            {
                return true;
            }
        }

        foreach (Hallway hallway in hallways)
        {
            if (paddedRoomRect.Overlaps(hallway.Area))
            {
                return true;
            }
        }

        return false;
    }

    void AddRooms()
    {
        while (_openDoorways.Count > 0 && _level.Rooms.Length < RoomLevelLayout.MaxRoomCount)
        {
            Hallway selectedEntryway = _openDoorways[_random.Next(0, _openDoorways.Count)];
            Room newRoom = BuildAdjacentRoom(selectedEntryway);

            if (newRoom == null)
            {
                _openDoorways.Remove(selectedEntryway);
                continue;
            }

            _level.AddRoom(newRoom);
            _level.AddHallway(selectedEntryway);

            selectedEntryway.EndRoom = newRoom;
            List<Hallway> newOpenHallways = newRoom.CalcAllPossibleDoorways(newRoom.Area.width, newRoom.Area.height, RoomLevelLayout.DoorDistanceFromEdge);
            newOpenHallways.ForEach(hallway => hallway.StartRoom = newRoom);

            _openDoorways.Remove(selectedEntryway);
            _openDoorways.AddRange(newOpenHallways);
        }
    }
}
