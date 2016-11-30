using UnityEngine;
using System.Collections;

public class EnemyAttackBound : MonoBehaviour {

    CircleCollider2D attackBound;
    Enemy enemy;

    public bool onHitFlag { get; set; }

    void Awake()
    {
        attackBound = GetComponent<CircleCollider2D>();
        onHitFlag = false;
    }

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        attackBound.radius = enemy.getAttackBound();

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            onHitFlag = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            onHitFlag = false;
        }
    }

    public void TranslateBound(Vector2 direction)
    {
        attackBound.offset = direction * enemy.getAttackRange();
    }
    public void TranslateBound(Vector2 direction, float multiplier)
    {
        attackBound.offset = direction * multiplier;
    }

    public void SetAttackBound(float bound)
    {
        attackBound.radius = bound;
    }

    public void DamageBound()
    {
        if (onHitFlag)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Hit();
        }
    }
}
