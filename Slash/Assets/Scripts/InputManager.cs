using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

    KeyCode keyCode_moveRight;
    KeyCode keyCode_moveLeft;
    KeyCode keyCode_moveUp;
    KeyCode keyCode_moveDown;

    KeyCode keycode_attack;
    KeyCode keyCode_fire;
    KeyCode keyCode_useItem;
    KeyCode keyCode_avoid;

    KeyCode keyCode_pause;

    public static event Action<Vector2> OnMove;
    public static event Action OnAttack;
    public static event Action<Vector2> OnFire;
    public static event Action<Vector2> OnAvoid;
    public static event Action OnUseItem;


    void Awake()
    {
        keyCode_moveUp = KeyCode.W;
        keyCode_moveLeft = KeyCode.A;
        keyCode_moveDown = KeyCode.S;
        keyCode_moveRight = KeyCode.D;

        keycode_attack = KeyCode.Mouse0;
        keyCode_fire = KeyCode.Mouse1;
        keyCode_useItem = KeyCode.E;
        keyCode_avoid = KeyCode.Space;

        keyCode_pause = KeyCode.Escape;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().controlFlag)
        {
            // Player's move event
            if (Input.GetKey(keyCode_moveUp) || Input.GetKey(keyCode_moveDown)
                || Input.GetKey(keyCode_moveLeft) || Input.GetKey(keyCode_moveRight))
                OnMove(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized);


            // Player's close attack event
            if (Input.GetKeyDown(keycode_attack))
            {
                OnAttack();
            }

            // Player's shot attack event
            if (Input.GetKeyDown(keyCode_fire))
            {
                OnFire(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            }

            // Player's item be used event
            if (Input.GetKeyDown(keyCode_useItem))
            {
                OnUseItem();
            }

            // Player's avoid event
            if (Input.GetKeyDown(keyCode_avoid))
            {
                OnAvoid(new Vector2(0, 0)); // should be modified , written by 16/11/20/pm 14:40
            }


            // BELOW SAYS SYSTEM KEY EVENT
            // FOR EXAMPLE, ESC TO PAUSE THE GAME ... ETC.
            // SHOULD BE WRITTEN
        }
    }

    


}
