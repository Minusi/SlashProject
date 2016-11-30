using UnityEngine;
using System.Collections;

public class EnemyCloseSecond : Enemy
{
    float deltaBound;

    void Awake()
    {
        eType = EType.SAND;
        eventFlag = false;

        activeFlag = false;
        attackFlag = false;
        controlFlag = true;

        dead = 0.75F;       //
        hitRecover = 0.3F;  //
        attackTime = 0.7F;     //설정하세요
        attackDelay = 1;          //

        hp = 2;
        speed = 1;
        attackRange = 2;
        attackBound = 2F;
        deltaBound = 0.5F;

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
        ReActive();
    }

    public void ExtendCalcAttack()
    {
        enemyAttackBound.SetAttackBound(attackBound + deltaBound);
        enemyAttackBound.DamageBound();
        enemyAttackBound.SetAttackBound(attackBound);
        ReActive();
    }
}
