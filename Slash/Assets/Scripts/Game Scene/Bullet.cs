using UnityEngine;
using System.Collections;

public class Bullet : Throwable {

    void Awake()
    {
        speed = 4;
    }

    void Update()
    {
        Shot(GameObject.FindWithTag("Player").transform.position);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Extinct") || collider.gameObject.CompareTag("Player"))
        {
            // 오브젝트 풀에 반환
        }
    }

    void Shot(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
