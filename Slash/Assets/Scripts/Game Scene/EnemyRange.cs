using UnityEngine;
using System.Collections;

public class EnemyRange : MonoBehaviour {

    CircleCollider2D range;
    Enemy enemy;

    void Awake()
    {
        range = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
       // range.radius = enemy.getRange();
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

    public double getRadius()
    {
        return range.radius;
    }
}
