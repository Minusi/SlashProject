using UnityEngine;
using System.Collections;

public class EnemyRecogClose : EnemyRecog {

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            enemy = obj.GetComponent<Enemy>();

            if (enemy.eType == EType.SHELL)
            {
                enemy = obj.GetComponent<EnemyCloseFirst>();
                enemy.activeFlag = true;
            }

            else if (enemy.eType == EType.SAND)
            {
                enemy = obj.GetComponent<EnemyCloseSecond>();
                enemy.activeFlag = true;
            }
            else if (enemy.eType == EType.WITCH)
            {
                enemy = obj.GetComponent<EnemyCloseThird>();
                enemy.activeFlag = true;
            }
        }
    }
}
