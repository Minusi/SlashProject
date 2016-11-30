using UnityEngine;
using System.Collections;

public class EnemyAttackRange : MonoBehaviour {

    CircleCollider2D attackRange;
    Enemy enemy;

    void Awake()
    {
        attackRange = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        attackRange.radius = enemy.getAttackRange();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemy.attackFlag = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemy.attackFlag = false;
        }
    }

    public void SetAttackRange(float attackRangeRadius)
    {
        attackRange.radius = attackRangeRadius;
    }
    public double GetAttackRange()
    {
        return attackRange.radius;
    }
}
