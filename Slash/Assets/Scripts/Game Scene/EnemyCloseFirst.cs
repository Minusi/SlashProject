using UnityEngine;
using System.Collections;
using System;

public class EnemyCloseFirst : Enemy
{
    void Awake()
    {
        eType = EType.SHELL;
        eventFlag = false;

        activeFlag = false;
        attackFlag = false;
        controlFlag = true;

        dead = 0.75F;
        hitRecover = 0.3F;
        attackTime = 0.7F;
        attackDelay = 1;

        hp = 2;
        speed = 1;
        attackRange = 2;
        attackBound = 1.75F;

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        enemyAttackRange = GetComponentInChildren<EnemyAttackRange>();
        enemyAttackRange.SetAttackRange(attackRange);
        enemyAttackBound = GetComponentInChildren<EnemyAttackBound>();
        enemyAttackBound.SetAttackBound(attackBound);

        StartCoroutine(EventUpdate());
    }

    void Update()
    {
        if (activeFlag && controlFlag)
        {
            Recog(GameObject.FindWithTag("Player").transform.position);
        }
    }

    IEnumerator EventUpdate()
    {
        while (controlFlag)
        {
            if (attackFlag)
            {
                eventFlag = true;

                WaitForSeconds wfs = new WaitForSeconds(attackDelay);
                Attack(GameObject.FindWithTag("Player").transform.position
                    - transform.position);
                yield return wfs;
            }
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Objects objects = collision.gameObject.GetComponent<Objects>();
            if (objects.oType == OType.TRAP)
            {
                Hit();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        rigid.isKinematic = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(controlFlag)
            rigid.isKinematic = false;
    }

    public override void Recog(Vector2 playerPosition)
    {
        Vector2 distance = playerPosition - (Vector2)transform.position;


        if (attackFlag == false)
        {
            Move(distance);
        }
    }

    public override void Move(Vector2 playerPosition)
    {
        if (eventFlag == false)
        {
            Vector2 direction = playerPosition.normalized;

            if (direction.x != 0 || direction.y != 0)
            {
                animeState = (int)State.MOVE;
                IdleDirection = direction;

                this.transform.Translate(direction * Time.deltaTime * speed);

                enemyAttackBound.TranslateBound(direction);
            }
            // Idle implements
            else
            {
                animeState = (int)State.IDLE;
                direction = IdleDirection;
            }
            SetAnime(animator, direction.x, direction.y, animeState);
        }
    }

    public override void Attack(Vector2 direction)
    {
        activeFlag = false;

        direction.Normalize();
        enemyAttackBound.TranslateBound(direction);

        Invoke("CalcAttack", attackTime);
        Invoke("ReActive", attackDelay);
        animeState = (int)State.ATTACK;
        SetAnime(animator, direction.x, direction.y, animeState);
    }

    public void CalcAttack()
    {
        enemyAttackBound.DamageBound();
    }
}