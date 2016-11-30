using UnityEngine;
using System.Collections;
using System;

public class Room  {//안씀
    Room[,] rooms;
    MiniMapGenerator MG;
    Matrix mapSize;
    public event Action OnClear;

    public int EnemyType;
    public Matrix EnemyPoint;

    public void EnemyData(Room room)
    {

    }

    public void OpenDoors()
    {
        rooms[0, 0].OpenDoors();
    }

    public void InEnemy(int stage)
    {
        MG.mapSize = MG.ComputeMapSize(stage);
        Room rooms = new Room(MG.mapSize);
    }
    
    public Room(Matrix mapSize)
    {
        this.mapSize = mapSize;
        rooms = new Room[mapSize.x, mapSize.y];
    }
}
