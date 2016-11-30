using UnityEngine;
using System.Collections;

public class Knife : Throwable {
    void Awake()
    {
        speed = 8;
    }

    void Update()
    {
        Shot(GameObject.FindWithTag("Player").transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            // 오브젝트 풀에 반환
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Extinct"))
        {
            // 오브젝트 풀에 반환
        }
    }

    void Shot(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
