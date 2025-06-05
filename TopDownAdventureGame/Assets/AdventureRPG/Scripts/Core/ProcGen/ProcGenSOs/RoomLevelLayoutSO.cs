using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Room Levl Config", menuName = "Scriptable Objects/ProcGen/RoomLevelLayout")]
public class RoomLevelLayoutSO : ScriptableObject
{
    #region Map Settings
    [SerializeField] int _width = 64;
    [SerializeField] int _height = 64;
    [SerializeField] int _maxRoomCount = 8;
    [SerializeField] int _minRoomDistance = 1;
    [SerializeField] int _maxRoomDistance = 5;
    [SerializeField] int _roomWidthMin = 5;
    [SerializeField] int _roomWidthMax = 10;
    [SerializeField] int _roomLengthMin = 5;
    [SerializeField] int _roomLengthMax = 10;
    #endregion

    #region Room Settings
    [SerializeField] int _doorDistanceFromEdge = 1;
    #endregion

    #region Hallway Settings
    [SerializeField] int _hallwayLengthMin = 1;
    [SerializeField] int _hallwayLengthMax = 5;
    #endregion

    #region Public Getters
    public int Width { get => _width; }
    public int Height { get => _height; }
    public int MinRoomDistance { get => _minRoomDistance; }
    public int MaxRoomCount { get => _maxRoomCount; }
    public int MaxRoomDistance { get => _maxRoomDistance; }
    public int RoomWidthMin { get => _roomWidthMin; }
    public int RoomWidthMax { get => _roomWidthMax; }
    public int RoomLengthMin { get => _roomLengthMin; }
    public int RoomLengthMax { get => _roomLengthMax; }
    public int DoorDistanceFromEdge { get => _doorDistanceFromEdge; }
    public int HallwayLengthMin { get => _hallwayLengthMin; }
    public int HallwayLengthMax { get => _hallwayLengthMax; }
    #endregion
}
