using UnityEngine;
using System.Collections;

public class ObjectsWall : Objects {
    public enum WallType { HORIZON, VERTICAL };
    BoxCollider2D wallCollider;
    CircleCollider2D roundWallCollider;

    ObjectsWallPathPointer[] wallPathPointer;
    int pathPointCount; // 4개로 고정시킬것

    void Awake()
    {
        oType = OType.WALL;
        wallCollider = GetComponent<BoxCollider2D>();
        if (wallCollider == null)
        {
            roundWallCollider = GetComponent<CircleCollider2D>();
        }
    }

    void Start()
    {
        ObjectsWallPathPointer[] wallPathPointer = GetComponentsInChildren<ObjectsWallPathPointer>();
        pathPointCount = wallPathPointer.Length;

        if(pathPointCount != 4)
        {
            print(" 패스파인딩 초기화에 실패하였습니다. 런타임 에러가 발생하지 않게 소스코드를 수정하십시오");
            return;
        }
        if (wallCollider != null)
        {
            float wallHalfLengthX = wallCollider.size.x / 2;
            float wallHalfLengthY = wallCollider.size.y / 2;

            PPos pathPosition;
            Vector2[] direction = new Vector2[pathPointCount];
            float locatePositionX;
            float locatePositionY;
            for (int i = 0; i < pathPointCount; i++)
            {
                pathPosition = (PPos)i;

                locatePositionX = Mathf.Pow(-1, i + 1) * wallHalfLengthX * 3.5F;
                if (i <= 1)
                    locatePositionY = wallHalfLengthY * 2.5F;
                else
                    locatePositionY = -wallHalfLengthY * 2.5F;
                direction[i] = new Vector2(locatePositionX, locatePositionY);

                wallPathPointer[i].LocatePathPointer(pathPosition, direction[i]);
                wallPathPointer[i].SetWallPointerType(pathPosition);
            }
        }
        else
        {
            float wallHalfRadius = roundWallCollider.radius;

            PPos pathPosition;
            Vector2[] direction = new Vector2[pathPointCount];
            float locatePositionX;
            float locatePositionY;
            for (int i = 0; i < pathPointCount; i++)
            {
                pathPosition = (PPos)i;

                locatePositionX = Mathf.Pow(-1, i + 1) * wallHalfRadius * 3.5F;
                if (i <= 1)
                    locatePositionY = wallHalfRadius * 2.5F;
                else
                    locatePositionY = -wallHalfRadius * 2.5F;
                direction[i] = new Vector2(locatePositionX, locatePositionY);

                wallPathPointer[i].LocatePathPointer(pathPosition, direction[i]);
                wallPathPointer[i].SetWallPointerType(pathPosition);
            }
        }
    }

    public int getPathPointCount()
    {
        return pathPointCount;
    }
}
