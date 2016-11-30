using UnityEngine;
using System.Collections;

public enum PPos { NORTHWEST, NORTHEAST, SOUTHWEST, SOUTHEAST };

public class ObjectsWallPathPointer : MonoBehaviour {

    PPos wallPointerType;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy.eType == EType.STONE || enemy.eType == EType.WITCH ||
                enemy.eType == EType.GUN || enemy.eType == EType.MAID)
            {
                EnemyPathMemory ePathMemory = collider.GetComponent<EnemyPathMemory>();
                if (ePathMemory.isRaycast)
                ePathMemory.setVisitedPathPoint((int)wallPointerType);
            }
        }
    }

    public void SetWallPointerType(PPos ppos)
    {
        wallPointerType = ppos;
    }
    public void LocatePathPointer(PPos pathPosition, Vector2 direction)
    {
        gameObject.transform.localPosition = direction;
    }

}
