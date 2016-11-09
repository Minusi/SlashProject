using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {
    public static event Action<Vector2> OnPressMove;
    public static event Action<Vector2> OnMouse;
    public static event Action<Vector2> OnAttack;
    public static event Action<Vector2> OnAvoid;
    public static event Action<Vector2> OnFire;
    public static event Action<Vector2> OnActiveItem;


    
    KeyCode keyCode_moveRight;
    KeyCode keyCode_moveLeft;
    KeyCode keyCode_moveUp;
    KeyCode keyCode_moveDown;
    KeyCode keyCode_activeItem;
    KeyCode keyCode_avoid;
    

}
