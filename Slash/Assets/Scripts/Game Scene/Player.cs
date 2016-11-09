using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    static public int Speed = 7;
    bool hasArmor;
    int bulletCount;
    Item item;

    int damage = 1;

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
        }

    }

    void Hit()
    {
        if (hasArmor == true)
        {
            hasArmor = false;
        }
        else
        {
            Die();
        }
    }

    void Die()  
    {
        SceneManager.LoadScene("Game Scene");
        // 버튼 restart, menu버튼 띄우기
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }

    void Avoid()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // 회피
        }
    }

    void ActiveItem()
    {
        if (Input.GetKey(KeyCode.E))
        {
            // 아이템 사용
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
            this.transform.Translate(new Vector2(-1 * Time.deltaTime * Speed, 0));
        if (Input.GetKey(KeyCode.D))
            this.transform.Translate(new Vector2(1 * Time.deltaTime * Speed, 0));
        if (Input.GetKey(KeyCode.W))
            this.transform.Translate(new Vector2(0, 1 * Time.deltaTime * Speed));
        if (Input.GetKey(KeyCode.S))
            this.transform.Translate(new Vector2(0, -1 * Time.deltaTime * Speed));
    }
  
    
    void Update()
    {
        Move();

    }
}
