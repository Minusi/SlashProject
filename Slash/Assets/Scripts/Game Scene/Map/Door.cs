using UnityEngine;
using System.Collections;


public enum DoorDirec
{
    Up,
    Right,
    Left,
    Down
}

public class Door : MonoBehaviour {
    static public bool isOpen;
    public DoorDirec doorDirec;

    void Awake()
    {
        isOpen = true;
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (isOpen)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                switch (doorDirec)
                {
                    case DoorDirec.Up:
                        GameObject.Find("GameManager").GetComponent<GameManager>().NextRoom((int)DoorDirec.Up);
                        break;
                    case DoorDirec.Right:
                        GameObject.Find("GameManager").GetComponent<GameManager>().NextRoom((int)DoorDirec.Right);
                        break;
                    case DoorDirec.Left:
                        GameObject.Find("GameManager").GetComponent<GameManager>().NextRoom((int)DoorDirec.Left);
                        break;
                    case DoorDirec.Down:
                        GameObject.Find("GameManager").GetComponent<GameManager>().NextRoom((int)DoorDirec.Down);
                        break;
                }   
            }
        }
    }

  
}
