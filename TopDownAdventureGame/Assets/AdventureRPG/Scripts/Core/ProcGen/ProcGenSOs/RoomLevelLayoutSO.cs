using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Room Levl Config", menuName = "Scriptable Objects/ProcGen/RoomLevelLayout")]
public class RoomLevelLayoutSO : ScriptableObject
{
    #region Map Settings
    [field: SerializeField] public int Width { get; private set;} = 64;
    [field: SerializeField] public int Height { get; private set;} = 64;
    [field: SerializeField] public int MaxRoomCount { get; private set;} = 8;
    [field: SerializeField] public int MinRoomDistance { get;private set;} = 1;
    [field: SerializeField] public int MaxRoomDistance { get; private set;} = 5;
    #endregion

    #region Room Settings
    [field: SerializeField] public int RoomWidthMin { get; private set;} = 3;
    [field: SerializeField] public int RoomWidthMax { get; private set;} = 5;
    [field: SerializeField] public int RoomLengthMin { get; private set;} = 3;
    [field: SerializeField] public int RoomLengthMax { get; private set;} = 5;
    [field: SerializeField] public int DoorDistanceFromEdge {get; private set;} = 1;
    #endregion

        #region Hallway Settings
    [field: SerializeField] public int HallwayLengthMin = 1;
    [field: SerializeField] public int HallwayLengthMax = 5;
    #endregion
}
