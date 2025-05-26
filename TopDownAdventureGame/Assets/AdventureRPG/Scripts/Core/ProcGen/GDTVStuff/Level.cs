using System;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    int _width, _height;
    List<Room> _rooms;
    List<Hallway> _hallways;

    public int Width => _width;
    public int Height => _height;

    public Room[] Rooms => _rooms.ToArray();
    public Hallway[] Hallways => _hallways.ToArray();

    public Level(int width, int height)
    {
        this._width = width;
        this._height = height;
        _rooms = new List<Room>();
        _hallways =  new List<Hallway>();
    }

    public void AddRoom(Room newRoom) => _rooms.Add(newRoom);

    public void AddHallway(Hallway newHallway) => _hallways.Add(newHallway);
}