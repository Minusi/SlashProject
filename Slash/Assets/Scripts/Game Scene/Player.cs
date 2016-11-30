using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class Player : BaseControl {

    public static event Action OnDie;

    static public int speed;
    static public  int damage;
    static public int bulletCount;


    static public bool isArmed;
    List<Item> itemList;

    PlayerRange playerRange;

    void Awake()
    {
        controlFlag = true;

        speed = 6;
        damage = 1;
        bulletCount = 0;

        isArmed = false;
        itemList = null;

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        playerRange = GetComponentInChildren<PlayerRange>();
    }

    void OnEnable()
    {
        InputManager.OnMove += Move;
        InputManager.OnAttack += Attack;
        InputManager.OnUseItem += UseItem;
        InputManager.OnAvoid += Avoid;
        InputManager.OnFire += Fire;
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.CompareTag("EnemyCast"))
        {
            Hit();
        }
        else if (obj.gameObject.CompareTag("Item"))
        {
            ItemGet();
        }
        else if (obj.gameObject.CompareTag("Object"))
        {
            Objects objects = obj.gameObject.GetComponent<Objects>();
            if (objects.oType == OType.TRAP)
            {
                Hit();
            }
        }
    }

    public void Move(Vector2 direction)
    {
        // Move implements
        if (direction.x != 0 || direction.y != 0)
        {
            animeState = (int)State.MOVE;
            IdleDirection = direction;

            this.transform.Translate(direction * Time.deltaTime * speed);
            playerRange.TranslateRange(direction);
        }
        // Idle implements
        else
        {
            animeState = (int)State.IDLE;
            direction = IdleDirection;
        }

        SetAnime(animator, direction.x, direction.y, animeState);
    }

    public void Attack()
    {

        animeState = (int)State.ATTACK;
        SetAnime(animator, animeState);

        playerRange.DamageBound();

    }

    public void Fire(Vector2 direction)
    {
        if (bulletCount > 0)
        {
            // 오브젝트 풀에서 플레이어의 포지션에 총알 객체를 푸시오브젝트 시킨다.

            direction.Normalize();
            animeState = (int)State.FIRE;
            SetAnime(animator, direction.x, direction.y, animeState);
        }
        else
        {
            bulletCount = 0;

            // 찰칵 소리나게 할까?
        }
    }

    public void Hit()
    {
        if (isArmed == true)
        {
            isArmed = false;

            animeState = (int)State.HIT;
            SetAnime(animator, animeState);
        }
        else
        {
                      Die();
            //print("die");
        }
    }

    public void Die()  
    {
        controlFlag = false;
        animeState = (int)State.DIE;
        SetAnime(animator, animeState);
        StartCoroutine(StopAnime(0.833F));

        OnDie();
        // 게임오버
    }

    public void Avoid(Vector2 direction)
    {

    }

    public void ItemGet()
    {
        
    }
    public void UseItem()
    {
    }


    public int getDamage()
    {
        return damage;
    }

    void OnDisable()
    {
        InputManager.OnMove -= Move;
        InputManager.OnAttack -= Attack;
        InputManager.OnUseItem -= UseItem;
        InputManager.OnAvoid -= Avoid;
        InputManager.OnFire -= Fire;
    }
}
