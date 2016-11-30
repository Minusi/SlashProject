using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().NextStage();
        }
    }
}
