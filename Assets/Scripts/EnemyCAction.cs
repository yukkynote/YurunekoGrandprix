using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // ナビメッシュを利用するのに必要

public class EnemyCAction : MonoBehaviour
{
    GameObject Player; // プレイヤー
    NavMeshAgent myNavi; // ナビメッシュ
    Animator myAnim; // アニメーター
    Vector3[] DestinationPos = new Vector3[] {
        /*
        new Vector3(-4.0f, 0.0f, -60.0f),
        new Vector3(-128.0f, 0.0f, -60.0f),
        new Vector3(-128.0f, 0.0f, 60.0f),
        new Vector3(-4.0f, 0.0f, 60.0f),
        new Vector3(0.0f, 0.0f, -2.0f),
        */
        new Vector3(-23.1f,13.0f,5.24f),
        new Vector3(-18.77f,1.0f,5.24f),
        new Vector3(-11.0f,1.0f,5.5f),
        new Vector3(-14.0f,1.0f,8.0f),
    }; 
    int DestinationIndex;
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
    public int checkCount;
    public float progress;
    public int carNo;
    public GameObject PatSmoke;
    ParticleSystem.MainModule SmokeMain; //砂煙の本体
    public bool isBoost = false;

    // SE
    AudioSource EnemyAudio;                    // オーディオソース
    public AudioClip cat1;               // 
    public AudioClip cat2;               // 
    public AudioClip cat3;               // 
    public AudioClip cat4;               // 
    public AudioClip pyun;               // 

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        myNavi = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        SmokeMain = PatSmoke.GetComponent<ParticleSystem>().main;
        myNavi.enabled = false; //ナビメッシュを停止する
        myAnim.SetFloat("Speed", 0);
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
        checkCount = 0;
        progress = (checkPoint1.transform.position - transform.position).magnitude;
        EnemyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
    }
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject == Player)
        {
            //プレイヤーと接触したらゲームオーバー
            Player.SendMessage("OnHitEnemy");
            GameObject.Find("GameManager").SendMessage("GameOver",
            SendMessageOptions.DontRequireReceiver);
            DontMove();
        }
        */
        if (other.gameObject == checkPoint1 && checkCount == 0) {
            checkCount = 1;
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

        }
    }
    void Move() {
        myNavi.enabled = true; //ナビメッシュを停止する
        myAnim.SetFloat("Speed", 1.0f);
    }
    void DontMove()
    {
        myNavi.enabled = false; //ナビメッシュを停止する
        myAnim.SetFloat("Speed", 0);
    }
    void AddSpeed() {
        EnemyAudio.PlayOneShot(pyun); //
        StartCoroutine("SpeedBooster");
    }
    public float getProgress() {
        return progress;
    }
    public int getCheckCount() {
        return checkCount;
    }
    void Update()
    {
        if (Player && myAnim && myNavi.enabled) {
            myNavi.destination = DestinationPos[DestinationIndex];
            if ((this.transform.position - DestinationPos[DestinationIndex]).magnitude < 1.0f) {
                if (DestinationIndex < DestinationPos.Length - 1) {
                    DestinationIndex++;
                }
            }
            // myNavi.destination = Player.transform.position;
            myAnim.SetFloat("Speed", myNavi.velocity.magnitude);
            // 移動方向への量に応じて砂ぼこりを制御する。
            if (myNavi.velocity.magnitude != 0.0f) {
                SmokeMain.startSize = myNavi.velocity.magnitude * 1.5f;
                if (Random.Range(0.0f, 1.0f) > 0.99f) {
                    float rd = Random.Range(1, 4);
                    if (rd <= 1) {
                        EnemyAudio.PlayOneShot(cat1); //
                    } else if (rd <= 2) {
                        EnemyAudio.PlayOneShot(cat2); //
                    } else if (rd <= 3) {
                        EnemyAudio.PlayOneShot(cat3); //
                    } else if (rd <= 4) {
                        EnemyAudio.PlayOneShot(cat4); //
                    }
                }
            }
        } else {
            SmokeMain.startSize = 0.0f;
        }
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
    }
    IEnumerator SpeedBooster() {
        myNavi.acceleration += 4.0f;
        myNavi.speed += 4.0f;
        isBoost = true;
        yield return new WaitForSeconds(5.0f);
        myNavi.acceleration -= 4.0f;
        myNavi.speed -= 4.0f;
        isBoost = false;
    }
}
