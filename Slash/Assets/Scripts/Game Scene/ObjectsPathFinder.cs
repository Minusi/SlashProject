using UnityEngine;
using System.Collections;
using System;

public class ObjectsPathFinder {

    private static ObjectsPathFinder pathFinder;

    public static ObjectsPathFinder getInstance
    {
        get
        {
            if( pathFinder == null)
            {
                pathFinder = new ObjectsPathFinder();
            }
            return pathFinder;
        }
    }

    // ***** 커스텀 패스파인딩 알고리즘
    // 

    public Vector2 PathFind(GameObject startGO, RaycastHit2D hitWall)
    {
        // 벽 오브젝트의 패스포인트 4지점을 가지고 온다.
        Transform[] temp4PathPoint = hitWall.collider.gameObject.GetComponentsInChildren<Transform>();
        // 여기서 배열 크기는 5인데 0번째가 벽 자기자신을 의미하므로 아래 순환문에서 해당요소를 제거하는 작업을 한다.
        GameObject[] pathPoint = new GameObject[temp4PathPoint.Length-1];
        for (int i = 1; i < temp4PathPoint.Length; i++)
        {
            pathPoint[i-1] = temp4PathPoint[i].gameObject;
        }
        
        // 해당 패스포인트가 올바른 경로를 제시하는 지를 나타내는 변수
        bool[] isPathCorrect = new bool[pathPoint.Length];

        // 에너미가 벽의 패스포인터에 raycast를 하기 위한 매개변수 준비 단계
        // 매개변수 1 :: Vector2 origin == startGOPosition
        // 매개변수 2 :: Vector2 direction == pathPointPosition[]
        // 매개변수 3 :: float distance == distance[]
        Vector2 startGOPosition = startGO.transform.position;
        Vector2[] pathPointPosition = new Vector2[pathPoint.Length];
        float[] distance = new float[pathPoint.Length];

        // startGO 객체의 패스메모리를 참조
        EnemyPathMemory startGOMemory = startGO.GetComponent<EnemyPathMemory>();
        
        for (int i = 0; i < pathPoint.Length; i++)
        {
            // startGO객체가 벽의 4지점의 이전 경로 상태값들을 isPathCorrect에 복사한다.
            isPathCorrect[i] = startGOMemory.IsUsablePathPoint[i];
            // 4개의 패스포인트 지점과 그 거리를 계산하여 입력하는 부분
            pathPointPosition[i] = pathPoint[i].transform.position;
            distance[i] = (pathPointPosition[i] - startGOPosition).magnitude;
        }

        // 에너미가 첫번째 충돌벽의 패스포인트에 Raycast하는 프로시저
        for (int i = 0; i < pathPoint.Length; i++)
        {
            // 해당 경로가 올바르다는 가정이 위에서 설정된 포인트에 한해서 아래의 기능을 수행
            if (isPathCorrect[i])
            {
                // 먼저 startGO 객체가 해당 패스포인터까지의 거리에 Raycast를 실행
                RaycastHit2D[] hits = Physics2D.RaycastAll(startGOPosition, pathPointPosition[i], distance[i]);
                for (int j = 0; j < hits.Length; j++)
                {
                    // 충돌한 모든 hit들에 대해 그 요소에 벽과의 충돌 지점이 있었는 지 검사
                    GameObject hitGO = hits[j].collider.gameObject;
                    if (hitGO.CompareTag("Object"))
                    {
                        Objects objects = hitGO.GetComponent<Objects>();
                        // 벽과 충돌한 요소가 존재할 경우 해당 패스포인터는 올바르지 못한 경로로 전환
                        if (objects.oType == OType.WALL)
                            isPathCorrect[i] = false;
                    }
                }
            }
        }

        // StartGO 객체로부터 가장 가까운 거리의 패스포인터를 저장하는 변수
        float minDistance = Mathf.Infinity;
        // 가장 가까운 거리의 패스포인터의 인덱스를 저장하는 변수
        int finalPath = -1;

        // 바로 직전에 방문되지 않았으면서도 startGO객체에게 Raycast되었을 때 벽과 충돌하지 않은
        // 패스포인터에 대해서 startGO객체와 최단경로를 구성하는 지점을 찾는 프로시저
        for (int i = 0; i < pathPoint.Length; i++)
        {
            if (isPathCorrect[i])
            {
                // i번째 패스포인터의 거리가 최단거리이면
                if(distance[i] < minDistance)
                {
                    // 최단거리를 i번째 거리로 설정
                    minDistance = distance[i];
                    // 최종경로를 i번째 인덱스로 설정
                    finalPath = i;
                }
            }
        }

        if(finalPath == -1)
        {
            return Vector2.zero;
        }
        // 최종 선정된 경로로 이동할 벡터를 반환
        return pathPointPosition[finalPath] - startGOPosition;
    }
}
