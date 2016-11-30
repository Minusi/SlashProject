using UnityEngine;
using System.Collections;

public class EnemyRecogMagic : EnemyRecog {

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            enemy = obj.GetComponent<Enemy>();

            if (enemy.eType == EType.JELLY)
            {
                enemy = obj.GetComponent<EnemyMagicFirst>();
                enemy.activeFlag = true;
            }

            else if (enemy.eType == EType.BOOK)
            {
                enemy = obj.GetComponent<EnemyMagicSecond>();
                enemy.activeFlag = true;
            }
            else if (enemy.eType == EType.MAID)
            {
                enemy = obj.GetComponent<EnemyCloseFirst>();
                enemy.activeFlag = true;
            }
        }
    }
}
