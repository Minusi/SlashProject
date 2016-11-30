using UnityEngine;
using System.Collections;
using System;

public class Trap :Objects {

    void Awake()
    {
        oType = OType.TRAP;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Player>().Hit();
        }
        else if(collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().Hit();
        }
    }
}
