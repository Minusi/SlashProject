using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {


    private List<GameObject> poolKnife;         // 오브젝트 종류만큼 리스트를 생성하고, 각각의 리스트에 에너미 객체를 담는다
    private List<GameObject> poolStone;
    private List<GameObject> poolGun;

    private int listCount;                      // 오브젝트 종류만큼 리스트를 생성하기 위한 변수

    private Transform pool;                     // 풀매니저의 트랜스폼 변수
    private GameObject obj;                     // 비상 시 사용되어질 오브젝트 변수


    /*****************************************************************************
                                  싱글톤 패턴 구현 구간
    ******************************************************************************/
    private static PoolManager poolInstance;

    public static PoolManager getInstance
    {
        get
        {
            if( poolInstance == null)
            {
                poolInstance = new PoolManager();
            }
            return poolInstance;
        }
    }
    /*****************************************************************************
                                  싱글톤 패턴 구현 구간
    ******************************************************************************/

        /*
    public void InitPool(GameObject obj, List<GameObject> des, int poolSize)
    {
        poolKnife = new List<GameObject>();
        poolGun = new List<GameObject>();
        poolGun = new List<GameObject>();

        pool = transform;

        this.obj = obj;

        for (int j = 0; j < poolSize; j++)
        {
            GameObject go = Instantiate(obj) as GameObject;
            PushObject(go, lCount);
        }
    }

    public void PushObject(GameObject obj, int lCount)
    {
        listObj[lCount].Add(obj);

        
        obj.transform.parent = pool;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
    }

    public GameObject PopObject(int lCount)
    {
        if(listObj[lCount].Count > 0)
        {
            GameObject retObj = listObj[lCount][0];
            listObj[lCount].RemoveAt(0);

            return retObj;
        }
        else
        {
            return Instantiate(obj) as GameObject;
        }
    }

    // 오브젝트 풀을 비운다
    public void ClearPool()
    {
        for(int i=0; i<listCount; i++)
        {
            listObj[listCount].Clear();
        }

        // 리스트 요소를 삭제하는 기능 구현
    }
    */
}