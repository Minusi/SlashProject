using UnityEngine;
using System.Collections;

public class EnemyRecogRange : EnemyRecog {

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            enemy = obj.GetComponent<Enemy>();

            if (enemy.eType == EType.STONE)
            {
                enemy = obj.GetComponent<EnemyRangeFirst>();
                enemy.activeFlag = true;
            }

            else if (enemy.eType == EType.GUN)
            {
                enemy = obj.GetComponent<EnemyCloseSecond>();
                enemy.activeFlag = true;
            }
        }
    }
}
