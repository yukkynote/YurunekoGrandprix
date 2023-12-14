using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AIツールを使うのに必要

public class PlayerBAction:MonoBehaviour
{
    float h; // 水平軸
    float v; // 垂直軸
    Vector3 Dir; // 移動方向
    Animator myAnim;
    Rigidbody myRB; // リジッドボディ
    public float charaAcc = 3.0f;
    public float charaSpeed = 10.0f;
    public float charaRot = 72.0f;
    public float foreSpeed = 6.0f; // 前進速度
    // public float foreSpeed = 20.0f; // 前進速度
    public float backSpeed = 5.0f; // 後退速度
    // public float backSpeed = 8.0f; // 後退速度
    public float rotSpeed = 72.0f; // 旋回速度
    //public float rotSpeed = 36.0f; // 旋回速度
    // public float jumpPower = 4.2f; // 跳躍力
    GameObject checkPoint1;
    GameObject checkPoint2;
    GameObject checkPoint3;
    GameObject checkPoint4;
    GameObject checkPointFlag1;
    GameObject checkPointFlag2;
    GameObject checkPointFlag3;
    GameObject checkPointFlag4;
    GameObject goalFlag;
    GameObject goal;
    GameObject mainCamera;
    GameObject gameManager;
    GameObject playerObject;
    public int checkCount;
    NavMeshAgent myAgent;
    bool isGoal;
    public GameObject PatSmoke;
    ParticleSystem.MainModule SmokeMain; //砂煙の本体
    public float progress;
    public bool isBoost = false;
    public GameObject chara1;
    public GameObject chara2;
    public GameObject chara3;
    public GameObject chara4;
    public GameObject chara5;
    public GameObject chara6;
    public GameObject chara7;
    public GameObject chara8;
    public GameObject obj;

    // Start is called before the first frame update
    void Start() {
        myAnim = GetComponent<Animator>(); // Animatorを取得
        SmokeMain = PatSmoke.GetComponent<ParticleSystem>().main;
        myRB = GetComponent<Rigidbody>(); // RigidBodyを取得
        // transform.position = new Vector3(0, 0, 0); // StartBoxの上
        // transform.rotation = Quaternion.identity;
        enabled = false;
        checkPoint1 = GameObject.FindGameObjectWithTag("checkPoint1");
        checkPoint2 = GameObject.FindGameObjectWithTag("checkPoint2");
        checkPoint3 = GameObject.FindGameObjectWithTag("checkPoint3");
        checkPoint4 = GameObject.FindGameObjectWithTag("checkPoint4");
        checkPointFlag1 = GameObject.FindGameObjectWithTag("checkPointFlag1");
        checkPointFlag2 = GameObject.FindGameObjectWithTag("checkPointFlag2");
        checkPointFlag3 = GameObject.FindGameObjectWithTag("checkPointFlag3");
        checkPointFlag4 = GameObject.FindGameObjectWithTag("checkPointFlag4");
        goalFlag = GameObject.FindGameObjectWithTag("goalFlag");
        goal = GameObject.FindGameObjectWithTag("Goal");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // gameManager = GameObject.FindGameObjectWithTag("GameController");
        checkCount = 0;
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.enabled = false;
        isGoal = false;
        progress = (checkPoint1.transform.position - transform.position).magnitude;

        foreSpeed = 0.0f;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("player1Character"))
        {
            switch (PlayerPrefs.GetInt("player1Character")) {
                case 1:
                    obj = (GameObject)Instantiate(chara1, new Vector3(-15.0f, 0.5f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(10.0f * 0.15f, 10.0f * 0.15f, 10.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 4.0f * 0.4f;
                    charaSpeed = 9.0f * 0.4f;
                    charaRot = 72.0f;
                    break;
                case 2:
                    obj = (GameObject)Instantiate(chara2, new Vector3(-15.0f, 0.5f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(15.0f * 0.15f, 15.0f * 0.15f, 15.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 3.0f * 0.4f;
                    charaSpeed = 10.0f * 0.4f;
                    charaRot = 144.0f;
                    break;
                case 3:
                    obj = (GameObject)Instantiate(chara3, new Vector3(-15.0f, 0.65f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(20.0f * 0.15f, 20.0f * 0.15f, 20.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 2.0f * 0.4f;
                    charaSpeed = 12.0f * 0.4f;
                    charaRot = 72.0f;
                    break;
                case 4:
                    obj = (GameObject)Instantiate(chara4, new Vector3(-15.0f, 0.5f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(10.0f * 0.15f, 10.0f * 0.15f, 10.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 4.0f * 0.4f;
                    charaSpeed = 10.0f * 0.4f;
                    charaRot = 36.0f;
                    break;
                case 5:
                    obj = (GameObject)Instantiate(chara5, new Vector3(-15.0f, 0.5f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(20.0f * 0.15f, 20.0f * 0.15f, 20.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 3.0f * 0.4f;
                    charaSpeed = 10.0f * 0.4f;
                    charaRot = 72.0f;
                    break;
                case 6:
                    obj = (GameObject)Instantiate(chara6, new Vector3(-15.0f, 0.5f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(20.0f * 0.15f, 20.0f * 0.15f, 20.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 2.0f * 0.4f;
                    charaSpeed = 11.0f * 0.4f;
                    charaRot = 144.0f;
                    break;
                case 7:
                    obj = (GameObject)Instantiate(chara7, new Vector3(-15.0f, 0.5f, -33.0f), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(0.4f * 0.15f, 0.4f * 0.15f, 0.4f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 3.0f * 0.4f;
                    charaSpeed = 11.0f * 0.4f;
                    charaRot = 36.0f;
                    break;
                case 8:
                    obj = (GameObject)Instantiate(chara8, new Vector3(0, 0.45f, 0), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    obj.gameObject.transform.localScale = new Vector3(20.0f * 0.15f, 20.0f * 0.15f, 20.0f * 0.15f);
                    obj.transform.parent = playerObject.transform;
                    charaAcc = 2.0f * 0.4f;
                    charaSpeed = 11.0f * 0.4f;
                    charaRot = 144.0f;
                    break;
                default:
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update() {
        if (isGoal/*myAgent.enabled*/) {
            //myAgent.destination = new Vector3(0.0f, 0.0f, -2.0f);
            // transform.rotation = Quaternion.Euler(0, Mathf.Repeat(Time.time, 0.1f) * 360f, 0);
            // ゆっくり止まる
            Dir = new Vector3(0, 0, 0.1f);
            Dir = transform.TransformDirection(Dir);
            foreSpeed -= charaAcc * Time.deltaTime / 2;
            if (foreSpeed < 0) {
                foreSpeed = 0;
            }
            Dir *= foreSpeed;
            transform.localPosition += Dir * Time.deltaTime; // キャラクターを移動

            SmokeMain.startSize = Dir.sqrMagnitude * 0.0f;
        } else if (enabled) {
            h = Input.GetAxis("Horizontal"); // 入力デバイスの水平軸
            v = Input.GetAxis("Vertical"); // 入力デバイスの垂直軸
            myAnim.SetFloat("Speed", v);
            myAnim.SetFloat("Direction", h);
            Dir = new Vector3(0, 0, v);
            Dir = transform.TransformDirection(Dir);
            /*
            charaAcc = 2.0f;
            charaSpeed = 10.0f;
            charaRot = 144.0f;
            */
            if (v > 0.1f) {
                foreSpeed += charaAcc * Time.deltaTime;
                if (foreSpeed > charaSpeed) {
                    foreSpeed = charaSpeed;
                }
            } else {
                foreSpeed -= charaAcc * Time.deltaTime / 2;
                if (foreSpeed < 0) {
                    foreSpeed = 0;
                }
            }
            rotSpeed = charaRot;
            Dir *= (v > 0.1f) ? foreSpeed : backSpeed;
            transform.localPosition += Dir * Time.deltaTime; // キャラクターを移動
            transform.Rotate(0, h * rotSpeed * Time.deltaTime, 0); // キャラクターを回転

            // 移動方向への量に応じて砂ぼこりを制御する。
            SmokeMain.startSize = Dir.sqrMagnitude * 1.5f;

            if (checkCount == 0) {
                progress = (checkPointFlag1.transform.position - transform.position).magnitude;
            }
            if (checkCount == 1) {
                progress = (checkPointFlag2.transform.position - transform.position).magnitude;
            }
            if (checkCount == 2) {
                progress = (checkPointFlag3.transform.position - transform.position).magnitude;
            }
            if (checkCount == 3) {
                progress = (checkPointFlag4.transform.position - transform.position).magnitude;
            }
            if (checkCount == 4) {
                progress = (goalFlag.transform.position - transform.position).magnitude;
            }
        } else {
            SmokeMain.startSize = 0.0f;
        }


    }
    void Move() {
        enabled = true;
        myAnim.SetFloat("Speed", 1.0f);
        myAnim.SetFloat("Direction", 1.0f);
    }
    void DontMove() {
        enabled = false;
        myAnim.SetFloat("Speed", 0);
        myAnim.SetFloat("Direction", 0);
    }
    void AddSpeed() {
        StartCoroutine("SpeedBooster");
        mainCamera.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("GameManager").SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
    }
    void ResetSpeed() {
        foreSpeed = 10.0f;
    }
    float getProgress() {
        return progress;
    }
    int getCheckCount() {
        return checkCount;
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject == checkPoint1 && checkCount == 0) {
            checkCount = 1;
            /*
            // 検証用
            GameObject.Find("GameManager").SendMessage("Goal", SendMessageOptions.DontRequireReceiver);
            //myAgent.enabled = true;
            isGoal = true;

            mainCamera.SendMessage("Goal", SendMessageOptions.DontRequireReceiver);
            */
        }
        if (other.gameObject == checkPoint2 && checkCount == 1) {
            checkCount = 2;
        }
        if (other.gameObject == checkPoint3 && checkCount == 2) {
            checkCount = 3;
        }
        if (other.gameObject == checkPoint4 && checkCount == 3) {
            checkCount = 4;
        }
        if (other.gameObject == goal && checkCount == 4) {
            GameObject.Find("GameManager").SendMessage("Goal", SendMessageOptions.DontRequireReceiver);
            //myAgent.enabled = true;
            isGoal = true;

            mainCamera.SendMessage("Goal", SendMessageOptions.DontRequireReceiver);
        }
    }
    IEnumerator SpeedBooster() {
        charaAcc += 3.0f;
        charaSpeed += 3.0f;
        isBoost = true;
        yield return new WaitForSeconds(5.0f);
        charaAcc -= 3.0f;
        charaSpeed -= 3.0f;
        isBoost = false;
    }
}
