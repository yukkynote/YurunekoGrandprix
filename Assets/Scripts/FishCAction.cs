using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCAction : MonoBehaviour
{
    GameObject Player;
    GameObject[] Enemies;
    PlayerCAction pAction;
    EnemyCAction eAction;
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            pAction = Player.GetComponent<PlayerCAction>();
            if (!pAction.isBoost) {
                Player.SendMessage("AddSpeed", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Enemy") {

            foreach (GameObject Enemy in Enemies) {
                if (Enemy == other.gameObject) {
                    eAction = Enemy.GetComponent<EnemyCAction>();
                    if (!eAction.isBoost) {
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
