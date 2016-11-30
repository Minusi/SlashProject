using UnityEngine;
using System.Collections;
using System;

public class RoomClear :MonoBehaviour{

    public int enemyCount;
    public GameObject AllMap;

    void Awake()
    {
        enemyCount = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemyCount++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemyCount--;
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy.eType == EType.SHELL)
            {
                GameManager.fscore += 2;
            }
        }

        else if (collider.CompareTag("BossEnemy"))
        {
            GameManager GM = new GameManager();
            GM.GameClear();
        }
    }
    
    void IsOpenCtrl()
    {
        if (enemyCount == 0)
        {
            Door.isOpen = true;
            AllMap.SetActive(true);
        }
    }

    void Update()
    {
        IsOpenCtrl();
    }
    // 승리 패배 팝업창
}
