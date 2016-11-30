using UnityEngine;
using System.Collections;

public class EnemyMagicRange : MonoBehaviour {
    CircleCollider2D magicRange;
    Enemy enemy;

    void Awake()
    {
        magicRange = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        magicRange.radius = enemy.getMagicRange();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemy.castFlag = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemy.castFlag = false;
        }
    }

    public void SetMagicRange(float magicRangeRadius)
    {
        magicRange.radius = magicRangeRadius;
    }
    public double GetMagicRange()
    {
        return magicRange.radius;
    }
}
