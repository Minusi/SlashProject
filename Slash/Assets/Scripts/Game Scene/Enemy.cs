using UnityEngine;
using System.Collections;
using System;

public enum EType { SHELL, SAND, WITCH, STONE, NONE, GUN, JELLY, BOOK, MAID};

public abstract class Enemy : BaseControl {

    public EType eType;

    public bool eventFlag { set; get; }

    public bool activeFlag { set; get; }
    public bool attackFlag { set; get; }
    public bool castFlag { set; get; }

    protected float dead;
    protected float hitRecover;
    protected float attackTime;
    protected float attackDelay;  // Time for next attack;
    protected float castTime;
    protected float castDelay;

    protected int hp;     // Health Point
    protected int speed;  // Move Speed;
    protected float attackRange;
    protected float attackBound;
    protected float magicRange;
    protected int synchronizedMagic;
    protected float[] magicBound;


    protected Rigidbody2D rigid;
    protected EnemyAttackRange enemyAttackRange;
    protected EnemyAttackBound enemyAttackBound;
    protected EnemyMagicRange enemyMagicRange;
    protected EnemyMagicBound[] enemyMagicBound;


    abstract public void Recog(Vector2 vec2);

    abstract public void Move(Vector2 vec2);

    abstract public void Attack(Vector2 playerPosition);

    public void Hit()
    {
        if (hp > 0)
        {
            eventFlag = true;
            activeFlag = false;
            hp = hp - GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getDamage();

            animeState = (int)State.HIT;
            SetAnime(animator, animeState);
            Invoke("ReActive", hitRecover);
        }
        else
            Die();
    }

    public void ReActive()
    {
        activeFlag = true;
        eventFlag = false;
    }

    public void Die()
    {
        activeFlag = false;
        controlFlag = false;
        animeState = (int)State.DIE;
        SetAnime(animator, animeState);

        StartCoroutine(StopAnime(dead));
        // 일정 시간후 지워지도록 해보자

        // 스코어 변수를 참조받아서 더하게 한다.
    }
    


    public float getAttackRange()
    {
        return attackRange;
    }
    public float getAttackBound()
    {
        return attackBound;
    }

    public float getMagicRange()
    {
        return magicRange;
    }
    public float[] getMagicBound()
    {
        for(int i=0; i < magicBound.Length; i++)
        {
            print(magicBound[i]);
        }
        return magicBound;
    }
}
