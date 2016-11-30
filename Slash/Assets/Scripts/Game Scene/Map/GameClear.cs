using UnityEngine;
using System.Collections;

public class GameClear : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameClear();
            GameObject.Find("FileManager").GetComponent<FileManager>().GameClear();
        }
    }
}
