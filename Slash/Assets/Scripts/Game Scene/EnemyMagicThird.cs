using UnityEngine;
using System.Collections;

public class EnemyMagicThird : Enemy {

    void Awake()
    {
        eType = EType.MAID;
        eventFlag = false;

        activeFlag = false;
        attackFlag = false;
        castFlag = false;
        controlFlag = true;


        hitRecover = 0.270F;
        attackTime = 0.834F;
        castTime = 0.917F;
        dead = 1.42F;
        attackDelay = 1.167F;
        castDelay = 1.167F;


        hp = 2;
        speed = 1;
        attackRange = 2;
        attackBound = 1.75F;

        magicRange = 6;
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
            magicBound[i] = 2;
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

    void OnCollisionStay2D(Collision2D collision)
    {
        rigid.isKinematic = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (controlFlag)
            rigid.isKinematic = false;
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

                enemyAttackBound.TranslateBound(direction);

                for (int i = 0; i < synchronizedMagic; i++)
                {
                    Quaternion rotation = Quaternion.Euler(0, 0, (360 / synchronizedMagic) * (i + 1));
                    enemyMagicBound[i].TranslateBound(direction);
                    enemyMagicBound[i].RotateBound(rotation);
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


    public void Cast(Vector2 direction)
    {
        direction.Normalize();

        activeFlag = false;
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
