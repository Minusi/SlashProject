using UnityEngine;
using System.Collections;

public enum BoundID { ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN };

public class EnemyMagicBound : MonoBehaviour {

    CircleCollider2D magicBound;
    Enemy enemy;

    public bool onHitFlag { get; set; }

    void Awake()
    {
        magicBound = GetComponent<CircleCollider2D>();
        onHitFlag = false;
    }

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
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
        magicBound.offset = direction * enemy.getMagicRange();
    }
    public void Translatebound(Vector2 direction, int sequenceNumber)
    {
        magicBound.offset = direction * sequenceNumber;
    }

    public void RotateBound(Quaternion eulerAngle)
    {
        Vector3 rotateVector = eulerAngle * (Vector3)magicBound.offset;
        magicBound.offset = (Vector2)rotateVector;
    }

    public void SetMagicBound(float bound)
    {
        magicBound.radius = bound;
    }

    public void DamageBound()
    {
        if (onHitFlag)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Hit();
        }
    }
}
