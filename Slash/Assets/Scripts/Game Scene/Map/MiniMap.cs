using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MiniMap {

    Matrix mapSize;
    MiniRoom[,] miniRooms;

    public MiniRoom this[int x, int y]
    {
        get { return miniRooms[x, y]; }
        set { miniRooms[x, y] = new MiniRoom(); }
    }

    public MiniRoom this[Point point]
    { // property
        get { return miniRooms[point.x, point.y]; }
        set { miniRooms[point.x, point.y] = new MiniRoom(); }
    }



    public MiniMap(Matrix mapSize)
    {
        this.mapSize = mapSize;
        miniRooms = new MiniRoom[mapSize.x, mapSize.y]; 
    }

}
