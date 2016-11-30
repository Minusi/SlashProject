using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRangeFirst : Enemy {

    public bool aimFlag { set; get; }

    void Awake()
    {
        eType = EType.STONE;
        eventFlag = false;

        aimFlag = false;
        activeFlag = false;
        attackFlag = false;
        controlFlag = true;

        dead = 0.833F;      //
        hitRecover = 0.3F;  //
        attackTime = 0.0F;     //
        attackDelay = 0.917F;     //

        hp = 3;
        speed = 1;
        attackRange = 8;
        // RANGE 계열은 오브젝트를 발사하므로 bound 콜라이더가 필요없다.

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
        while (hp > 0)
        {
            if (attackFlag)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, 
                    GameObject.FindGameObjectWithTag("Player").transform.position);
                aimFlag = hit.collider.CompareTag("Player");
                if (aimFlag)
                {
                    eventFlag = true;

                    WaitForSeconds wfs = new WaitForSeconds(attackDelay);
                    Attack(GameObject.FindWithTag("Player").transform.position
                        - transform.position);
                    yield return wfs;
                }
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
                    if (objects.oType == OType.WALL)
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
            // 패스파인딩 알고리즘 구현해야함
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

        Invoke("ReActive", attackDelay);
        animeState = (int)State.ATTACK;
        SetAnime(animator, direction.x, direction.y, animeState);

        // 오브젝트 풀
    }
}
