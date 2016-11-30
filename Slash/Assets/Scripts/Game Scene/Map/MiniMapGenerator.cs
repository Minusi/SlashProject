using System;
using UnityEngine;
using System.Collections;

using Random = UnityEngine.Random;

//struct Range{

//    public int min;
//    public int max;

//    public Range(int min, int max)
//    {
//        this.min = min;
//        this.max = max;
//    }

//}

public struct Matrix
{

    public int x;
    public int y;

    public int cellCount
    {
        get { return x * y; }
    }

    public Matrix(int column, int row)
    {
        this.x = column;
        this.y = row;
    }

}
    


public class MiniMapGenerator {

    public Matrix ComputeMapSize(int stage)
    {
        switch (stage)
        {
            case 1:
                return new Matrix(3, 3);
            case 2:
                return new Matrix(4, 4);
            default:
                return new Matrix(4, 4);
        }
    }

    int RandomRoomCount(int maxCellCount)                   // 사용x?
    {
        int half = maxCellCount / 2;
        return Random.Range(half, maxCellCount + 1);
    }


    public Matrix mapSize;

    public MiniMap Generate(int stage)
    {
        bool Label_go = false;
        mapSize = ComputeMapSize(stage);                
        int roomCount = Mapsize(stage);
        int curRoomCount = 0;

        MiniMap miniMap = new MiniMap(mapSize);
        MapLink mapLink = new MapLink();

        int startX = Random.Range(0, mapSize.x);          // 스타트지점 x,y 지정
        int startY = Random.Range(0, mapSize.y);
        int nextX = 0;
        int nextY = 0;

        MiniRoom startRoom = new MiniRoom();                // 스타트룸 생성, 포인트 지정
        Point startPoint = new Point(startX, startY);
        MiniRoom[] Rooms = new MiniRoom[roomCount];         // 나머지 룸, 포인트 생성
        Point[] Points = new Point[roomCount];


        miniMap[startPoint] = startRoom;
        miniMap[startPoint].isSource = true;
        miniMap[startPoint].isUsed = true;

       // MiniRoom PrevRoom = startRoom;
        for (int i = 0; i < roomCount; i++)
        {
            curRoomCount++;
            Rooms[i] = new MiniRoom();
            
        Roommake:
            nextX = startX;
            nextY = startY;
        is_SameRoom:
            Label_go = false;

            int ran = Random.Range(0, 4);                   // 랜덤으로 방을 생성하기위한 int 
            switch (ran)                                    // 랜덤 방생성
            {
                case 0:
                    nextX--;
                    if (nextX < 0) { goto Roommake; }
                    
                    Points[i] = new Point(nextX, nextY);
                    for (int j = 0; j < i; j++)                                          // 룸끼리 겹칠시 다시 세팅
                    {
                        if ((Points[j].x == Points[i].x) && (Points[j].y == Points[i].y) || (Points[i].x == startPoint.x) && (Points[i].y == startPoint.y))
                        {
                            Label_go = true;
                        }
                    }
                    if (Label_go == true) goto is_SameRoom;
                   miniMap[Points[i]] = Rooms[i];
                    miniMap[Points[i]].isUsed = true;
                    goto sameroom_check;
                case 1:
                    nextX++;
                    if (nextX >= mapSize.y) goto Roommake;

                    Points[i] = new Point(nextX, nextY);
                    for (int j = 0; j < i; j++)
                    {
                        if ((Points[j].x == Points[i].x) && (Points[j].y == Points[i].y) || (Points[i].x == startPoint.x) && (Points[i].y == startPoint.y))
                        {
                            Label_go = true;
                        }
                    }
                    if (Label_go == true) goto is_SameRoom;
                    miniMap[Points[i]] = Rooms[i];
                    miniMap[Points[i]].isUsed = true;
                    goto sameroom_check;
                case 2:
                    nextY--;
                    if (nextY < 0) goto Roommake;

                    Points[i] = new Point(nextX, nextY);
                    for (int j = 0; j < i; j++)
                    {
                        if ((Points[j].x == Points[i].x) && (Points[j].y == Points[i].y) || (Points[i].x == startPoint.x) && (Points[i].y == startPoint.y))
                        {
                            Label_go = true;
                        }
                    }
                    if (Label_go == true) goto is_SameRoom;
                    miniMap[Points[i]] = Rooms[i];
                    miniMap[Points[i]].isUsed = true;
                    goto sameroom_check;
                case 3:
                    nextY++;
                    if (nextY >= mapSize.x) goto Roommake;

                    Points[i] = new Point(nextX, nextY);
                    for (int j = 0; j < i; j++)
                    {
                        if ((Points[j].x == Points[i].x) && (Points[j].y == Points[i].y) || (Points[i].x == startPoint.x) && (Points[i].y == startPoint.y))
                        {
                            Label_go = true;
                        }
                    }
                    if (Label_go == true) goto is_SameRoom;
                    miniMap[Points[i]] = Rooms[i];
                    miniMap[Points[i]].isUsed = true;
                    goto sameroom_check;
            }
            sameroom_check:
            
            startX = nextX;
            startY = nextY;
        }

        mapLink.Link(miniMap, stage);      // 부르는 부분

        
        return miniMap;
    }


    public int Mapsize(int stage)
    {
        int mapsize = 0;
        switch (stage)
        {
            case 1:
                mapsize = 3;
                break;
            case 2:
                mapsize = 4;
                break;
        }

        return mapsize;
    }



    //void Link(MiniMap miniMap, Point p1, Point p2)
    //{
    //    if (p1.x == p2.x)
    //    {
    //        if (Math.Abs(p1.y - p2.y) != 1)
    //            throw new Exception();

    //        if (p1.x < p2.x)
    //        {
    //            miniMap[p1].hasRight = true;
    //            miniMap[p2].hasLeft = true;
    //        }

    //        else
    //        {
    //            miniMap[p1].hasLeft = true;
    //            miniMap[p2].hasRight = true;
    //        }
    //    }

    //    else if (p1.y == p2.y)
    //    {

    //        if (Math.Abs(p1.x - p2.x) != 1)
    //            throw new Exception();
    //    }

    //    else
    //        throw new Exception();
    //}

}
