using UnityEngine;
using System.Collections;

public class EnemyMagicSecond : Enemy {
    void Awake()
    {
        eType = EType.BOOK;
        eventFlag = false;

        activeFlag = false;
        attackFlag = false;
        castFlag = false;
        controlFlag = true;


        hitRecover = 0.500F;
        attackTime = 0.417F;
        castTime = 0.917F;
        dead = 1.167F;
        attackDelay = 1.6F;
        castDelay = 1.6F;


        hp = 4;
        speed = 1;
        attackRange = 1.5F;
        attackBound = 1.5F;

        magicRange = 8;
        // 마법공격판정범위는 start에서 제어한다!

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        enemyAttackRange = GetComponentInChildren<EnemyAttackRange>();
        enemyAttackRange.SetAttackRange(attackRange);
        enemyAttackBound = GetComponentInChildren<EnemyAttackBound>();
        enemyAttackBound.SetAttackBound(attackBound);
        enemyMagicRange = GetComponentInChildren<EnemyMagicRange>();
        enemyMagicRange.SetMagicRange(magicRange);
        enemyMagicBound = GetComponentsInChildren<EnemyMagicBound>();

        synchronizedMagic = enemyMagicBound.Length;
        magicBound = new float[synchronizedMagic];

        for (int i = 0; i < synchronizedMagic; i++)
        {
            magicBound[i] = 1.5F;
            (enemyMagicBound[i]).SetMagicBound(magicBound[i]);
        }
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
            if (attackFlag && activeFlag)
            {
                eventFlag = true;

                WaitForSeconds wfs = new WaitForSeconds(attackDelay);
                Attack(GameObject.FindWithTag("Player").transform.position
                    - transform.position);
                yield return wfs;
            }
            else if (attackFlag == false && castFlag)
            {
                eventFlag = true;
                WaitForSeconds wfs = new WaitForSeconds(castDelay);
                Cast(GameObject.FindWithTag("Player").transform.position
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

    public override void Recog(Vector2 playerPosition)
    {
        Vector2 distance = playerPosition - (Vector2)transform.position;

        if (attackFlag == false && castFlag == false)
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

                for (int i = 0; i < synchronizedMagic; i++)
                {
                    enemyMagicBound[i].TranslateBound(direction * ((float)i*2.25f/(float)synchronizedMagic));
                }
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

        Invoke("CalcAttack", attackTime);
        Invoke("ReActive", attackDelay);
        animeState = (int)State.ATTACK;
        SetAnime(animator, direction.x, direction.y, animeState);
    }

    public void CalcAttack()
    {
        enemyAttackBound.DamageBound();
    }


    public void Cast(Vector2 direction)
    {
        direction.Normalize();

        activeFlag = false;

        for (int i = 0; i < synchronizedMagic; i++)
        {
            enemyMagicBound[i].TranslateBound(direction * ((float)i * 2.25f / (float)synchronizedMagic));
        }

        Invoke("CalcMagic", castTime);
        Invoke("ReActive", castDelay);
        animeState = (int)State.ATTACK;
        SetAnime(animator, direction.x, direction.y, animeState);
    }

    public void CalcMagic()
    {
        for (int i = 0; i < synchronizedMagic; i++)
        {
            enemyMagicBound[i].DamageBound();
        }
    }
}
