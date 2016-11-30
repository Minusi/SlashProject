using UnityEngine;
using System.Collections;

public class BaseAnime : MonoBehaviour {

    protected Animator animator;
    protected int animeState;
    protected Vector2 IdleDirection;

    protected enum State { IDLE, MOVE, ATTACK, FIRE, AVOID, HIT, DIE };
    //  *************** W   A   R   N   I   N   G ***************
    //  YOU NEVER EXCHANGE THIS SEQUENCE
    //  THIS VALUES MATCH TO ITS ANIMATOR'S VALUE AT "AnimeState"
    //  IF YOU MODIFY THIS ENUM, YOU SHOULD WRITE CODE AFTER "DIE"
    //  *************** W   A   R   N   I   N   G ***************

    //  Params use animatior's state and mean follows:
    //  IDLE : No Action State. Player and Enemy have.
    //  MOVE : Translation State. Player and Enemy have.
    //  ATTACK : Do Damage State. Player and Enemy have.
    //  FIRE : Do Damage in range State. Player only haves.
    //  HIT : Get Damaged State. Player and Enemy have.
    //  DIE : Destroy Object State. Player and Enemy have.

    void Awake()
    {
        animeState = (int)State.IDLE;
        IdleDirection = new Vector2(0, 0);    

    }

    protected IEnumerator StopAnime(float animeLength)
    {
        WaitForSeconds wfs = new WaitForSeconds(animeLength);
        yield return wfs;
        animator.speed = 0;

        IEnumerator coroutine = StopAnime(0);
        StopCoroutine(coroutine);
    }

    protected void SetAnime(Animator animator, int animeState)
    {
        animator.SetInteger("AnimeState", animeState);
    }

    protected void SetAnime(Animator animator, float directionX, float directionY, int animeState)
    {
        animator.SetFloat("DirectionX", directionX);
        animator.SetFloat("DirectionY", directionY);
        animator.SetInteger("AnimeState", animeState);
    }

}
