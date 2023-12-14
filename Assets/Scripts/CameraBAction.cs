using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBAction : MonoBehaviour
{
    GameObject Target;
    Vector3 Offset = new Vector3(0, 1.4f, 0); //ターゲットの少し上
    Vector3 CamPos;
    Vector3 newPos;
    float nowRotate;
    bool isGoal;
    public GameObject PatDash;
    ParticleSystem DashMain;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        CamPos = transform.position;
        isGoal = false;
    }
    void Dash() {
        DashMain = PatDash.GetComponent<ParticleSystem>();
        DashMain.Play();
    }
    void Goal() {
        isGoal = true;
    }
    void LateUpdate()
    {
        if (!Target)
            return; //ターゲット不在なら動かさない。
                    //ターゲットのＹ角度と同じになるような角度を徐々に求める。
        if (isGoal) {
            transform.RotateAround
        (
            Target.transform.position,
            Vector3.up,
            10 * Time.deltaTime
        );
        } else {
            nowRotate = Mathf.LerpAngle(transform.eulerAngles.y,
            Target.transform.eulerAngles.y, 2.0f * Time.deltaTime);
            //ターゲット位置から新しい座標newPosを計算し、徐々に自分に与える。
            newPos = Target.transform.position -
            Quaternion.Euler(0, nowRotate, 0) * Vector3.forward * Mathf.Abs(CamPos.z);
            newPos.y = Mathf.Lerp(transform.position.y,
            Target.transform.position.y + CamPos.y, 3.0f * Time.deltaTime);
            transform.position = newPos;
            //カメラの注視点はターゲットの少し上を狙う。
            transform.LookAt(Target.transform.position + Offset);
        }
    }
}
