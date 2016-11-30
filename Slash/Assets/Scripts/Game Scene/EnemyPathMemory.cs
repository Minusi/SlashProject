using UnityEngine;
using System.Collections;

public class EnemyPathMemory : MonoBehaviour {

    GameObject raycastWall;

    public bool isRaycast { set; get; }

    public bool[] IsUsablePathPoint { set; get; }
    int IsUsablePathPointSize;

    int[] visitStack;

    void Awake()
    {
        IsUsablePathPointSize = -1;

        visitStack = new int[3];
        for (int i = 0; i < visitStack.Length; i++)
        {
            visitStack[i] = -1;
        }
    }
 
    public GameObject AttachRaycastWall(GameObject wall)
    {
        if (raycastWall != wall) { 
            raycastWall = wall;
            ObjectsWall objectsWall = wall.GetComponent<ObjectsWall>();
            InitPathMemory(objectsWall.getPathPointCount());
            return raycastWall;
        }
        return null;
    }

    public void DetachRaycastWall()
    {
        raycastWall = null;
    }

    public void InitPathMemory(int pathPointSize)
    {
        IsUsablePathPoint = new bool[pathPointSize];
        for(int i = 0; i<pathPointSize; i++)
        {
            IsUsablePathPoint[i] = true;
        }
        IsUsablePathPointSize = IsUsablePathPoint.Length;
    }

    public void setVisitedPathPoint(int i)
    {
        for (int j = visitStack.Length - 2; j >= 0; j--)
        {
            visitStack[j + 1] = visitStack[j];
        }
        visitStack[0] = i;
        if(visitStack[2] != -1)
            IsUsablePathPoint[visitStack[2]] = true;
        if(visitStack[0] != -1)
            IsUsablePathPoint[visitStack[0]] = false;


    }

    public int getIsUsablePathPointSize()
    {
        return IsUsablePathPointSize;
    }
}
