using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRange : MonoBehaviour {

    List<GameObject> inObj;
    CircleCollider2D range;

    void Awake()
    {
        inObj = new List<GameObject>();
        range = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            GameObject go = collider.gameObject;
            inObj.Add(go);
        }
        else
        {

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            inObj.Remove(collider.gameObject);
        }
    }

    public void TranslateRange(Vector2 direction)
    {
        range.offset = direction;
    }

    public void DamageBound()
    {
        foreach (GameObject hitObj in inObj)
        {
            Enemy enemy = hitObj.GetComponent<Enemy>();
            enemy.Hit();
        }
    }
}
