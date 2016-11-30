using UnityEngine;
using System.Collections;
using System;

public class EnemyCloseThird : Enemy {

    float additiveAttackTime;
    void Awake()
    {
        eType = EType.WITCH;
        eventFlag = false;

        activeFlag = false;
        attackFlag = false;
        controlFlag = true;

        dead = 1.5F;
        hitRecover = 0.25F;
        attackTime = 0.5F;
        additiveAttackTime = 0.084F;
        attackDelay = 1.333F;

        hp = 4;
        speed = 1;
        attackRange = 3;
        attackBound = 2.5F;

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
        if (controlFlag)
            rigid.isKinematic = false;
    }

    public override void Recog(Vector2 playerPosition)
    {
        Vector2 distance = playerPosition - (Vector2)transform.position;

        if (attackFlag == false)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, distance /*distance.magnitude*/);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Player"))
                {
                    EnemyPathMemory ePathMemory = GetComponent<EnemyPathMemory>();
                    int usePointSize = ePathMemory.getIsUsablePathPointSize();
                    if (usePointSize != -1)
                        ePathMemory.InitPathMemory(usePointSize);
                    Move(distance);
                    
                    return;
                }
                else if (hits[i].collider.CompareTag("Object"))
                {
                    Objects objects = hits[i].collider.GetComponent<Objects>();
                    if(objects.oType == OType.WALL)
                    {
                        EnemyPathMemory ePathMemory = GetComponent<EnemyPathMemory>();
                        ePathMemory.isRaycast = true;
                        ePathMemory.AttachRaycastWall(hits[i].collider.gameObject);

                        distance = ObjectsPathFinder.getInstance.PathFind(gameObject, hits[i]);
                        Move(distance);
                        return;
                    }
                }
                else
                {
                    continue;
                }
            }
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

                enemyAttackBound.TranslateBound(direction, 0.75F);
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
        enemyAttackBound.TranslateBound(direction, 0.75F);

        Invoke("CalcAttack", attackTime);
        Invoke("CalcAttack", attackTime+additiveAttackTime);
        Invoke("ReActive", attackDelay);
        animeState = (int)State.ATTACK;
        SetAnime(animator, direction.x, direction.y, animeState);
    }

    public void CalcAttack()
    {
        enemyAttackBound.DamageBound();
    }
}
