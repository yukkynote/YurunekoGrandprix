using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish2Action : MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;
    GameObject[] Enemies;
    Player1Action p1Action;
    Player2Action p2Action;
    Enemy2Action e2Action;
    void Start() {
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player2 = GameObject.FindGameObjectWithTag("Player2");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player1") {
            p1Action = Player1.GetComponent<Player1Action>();
            if (!p1Action.isBoost) {
                Player1.SendMessage("AddSpeed", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Player2") {
            p2Action = Player2.GetComponent<Player2Action>();
            if (!p2Action.isBoost) {
                Player2.SendMessage("AddSpeed", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Enemy") {

            foreach (GameObject Enemy in Enemies) {
                if (Enemy == other.gameObject) {
                    e2Action = Enemy.GetComponent<Enemy2Action>();
                    if (!e2Action.isBoost) {
                        other.gameObject.SendMessage("AddSpeed", SendMessageOptions.DontRequireReceiver);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    void Update() {
        transform.rotation = Quaternion.Euler(0.0f, Time.time * 90.0f, 0.0f); //自身を回す
    }
}
