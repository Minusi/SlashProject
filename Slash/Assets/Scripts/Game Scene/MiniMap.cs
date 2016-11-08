using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMap {

    int column;
    int row;

    MiniRoom[,] miniRooms;

    public MiniRoom this[int x, int y]
    {
        get { return miniRooms[x, y]; }
    }

    public MiniMap(int column, int row)
    {
    }

}
