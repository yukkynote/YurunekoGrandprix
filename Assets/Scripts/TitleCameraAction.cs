using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraAction : MonoBehaviour
{
    GameObject Target;
    Vector3 Offset = new Vector3(0, 1.4f, 0); //ターゲットの少し上
    Vector3 CamPos;
    Vector3 newPos;
    float nowRotate;
    GameObject[] Enemies;
    bool isGoal;
    public GameObject PatDash;
    ParticleSystem DashMain;
    float Elapsed; // 経過時間
    void Start() {
        Elapsed = 0.0f;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;
        foreach (GameObject Enemy in Enemies) {
            Target = Enemy;
            break;
        }
        CamPos = transform.position;
        isGoal = false;
    }
    void LateUpdate() {
        Elapsed += Time.deltaTime;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;
        foreach (GameObject Enemy in Enemies) {
            if (((int)(Elapsed / 5)) % 8 <= i) {
                Target = Enemy;
                break;
            }
            i++;
        }
        CamPos = transform.position;
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y+1.0f, Target.transform.position.z -3.0f);
        /*
        if (!Target)
            return; //ターゲット不在なら動かさない。
                    //ターゲットのＹ角度と同じになるような角度を徐々に求める。
        
            transform.RotateAround
        (
            Target.transform.position,
            Vector3.up,
            10 * Time.deltaTime
        );
        */
    }
}
