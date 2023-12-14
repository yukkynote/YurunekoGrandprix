using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIを利用するのに必要
using UnityEngine.SceneManagement; // シーン制御用

public class GameManagerB:MonoBehaviour
{
    // ゲームオブジェクト
    GameObject StartBox; // スタート台
    GameObject Player; // プレイヤー
    GameObject[] Enemies;
    PlayerBAction pAction;
    EnemyBAction eAction;
    public GameObject Fish;
    public GameObject Enemy0;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Enemy6;
    public GameObject Enemy7;

    public GameObject MobileJoystick;

    /*
    public enum STS
    {
        READY,
        PLAYING,
        GOAL
    }
    STS GameStatus; // ゲームステータス
    */
    // public float LimitTime = 60.0f; // 制限時間の初期値

    // UI制御
    public Text txtMessage;
    public Text txtNavigate;
    public Text txtTime;
    public Text txtRank;
    public Text txtProgress;
    public Text txtCheckCount;
    public Image pauseMenu;                 // ポーズメニュー
    public Image tutorialMenu;
    public Text menu1SelectText;
    public Text menu2SelectText;
    public Text menu3SelectText;
    public Image helpMenu;                  // ヘルプメニュー
    public Image rankingMenu;               // ランキングメニュー
    public Text[] scoreText;
    public Text[] rankText;
    public Text[] newText;
    public Text menuSelectText;
    public GameObject EnterImage;

    public GameObject PauseInfo;

    public Image[] rankChara1;
    public Image[] rankChara2;
    public Image[] rankChara3;
    public Image[] rankChara4;
    public Image[] rankChara5;

    // SE
    AudioSource MyAudio;                    // オーディオソース
    AudioSource MyAudio1;                    // オーディオソース
    AudioSource MyAudio2;                    // オーディオソース
    public AudioClip SE_Goal;               // ゴール
    public AudioClip cursor;               // 
    public AudioClip submit;               // 
    public AudioClip cat1;               // 
    public AudioClip cat2;               // 
    public AudioClip cat3;               // 
    public AudioClip cat4;               // 
    public AudioClip countdown;               // 
    public AudioClip kansei;               // 
    public AudioClip pyun;               // 
    public AudioClip speedup;               // 

    // パラメータ
    float Elapsed; // 経過時間
    int myScore; // スコア
    string CountDown;
    public float Score;
    bool isPlaying;                         // プレイ状態(true:プレイしている)
    bool isTutorial;
    bool isReady;
    bool isGoal;                           // ゴール状態(true:ゴールしている)
    bool isPause;                           // ポーズ状態(true:ポーズ)
    bool isHelp;                            // ヘルプ表示状態(true:表示)
    bool isRanking;                         // ランキング表示状態(true:表示)

    bool upFlg;
    bool downFlg;
    bool keyDown;

    float[] Rank = new float[6]; //内部的に保持するランキング 
    int[] Rankin = new int[6]; //内部的に保持するランキング 
    int[] RankChara = new int[6]; //内部的に保持するランキング 
    int idx;

    public int menuNo;

    int[] CheckCountList = new int[10];
    float[] ProgressList = new float[10];
    float[] ProgressList1 = new float[10];
    float[] ProgressList2 = new float[10];
    float[] ProgressList3 = new float[10];
    float[] ProgressList4 = new float[10];
    float[] RankProgressList = new float[10];
    int[] RankList = new int[10];
    int playerRank;

    public GameObject PatGoal;
    ParticleSystem GoalParticle;


    void Start() {
        Application.targetFrameRate = 30;
        // パラメータを初期化
        isPlaying = false; // プレイ状態を初期化
        isTutorial = false;
        isReady = false; // プレイ前状態を初期化
        isGoal = false; // ゴール状態を初期化
        isPause = false; // ポーズ状態を初期化
        isHelp = false; // ヘルプ状態を初期化
        isRanking = false; // ランキング表示状態を初期化

        pauseMenu.gameObject.SetActive(false); // ポーズメニューUIを非表示にする
        helpMenu.gameObject.SetActive(false); // ヘルプメニューUIを非表示にする
        rankingMenu.gameObject.SetActive(false); // ランキングメニューUIを非表示にする

        // EnterImage.enabled = false;
        EnterImage.SetActive(false);
        PauseInfo.SetActive(false);

        MobileJoystick.SetActive(Device.isMobile);

        MyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio1 = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio2 = GetComponent<AudioSource>(); // オーディオソースを取得

        Player = GameObject.FindGameObjectWithTag("Player");
        pAction = Player.GetComponent<PlayerBAction>();
        //StartBox = GameObject.FindGameObjectWithTag("Box");
        // StartCoroutine("FishSpawner"); // 魚生成処理を起動

        GoalParticle = PatGoal.GetComponent<ParticleSystem>();
        GoalParticle.Stop();
        if (PlayerPrefs.HasKey("player1Character")) {
            switch (PlayerPrefs.GetInt("player1Character")) {
                case 1:
                    // tissue
                    Destroy(Enemy6);
                    break;
                case 2:
                    // nabe
                    Destroy(Enemy5);
                    break;
                case 3:
                    // liquid
                    Destroy(Enemy3);
                    break;
                case 4:
                    // dacks
                    Destroy(Enemy1);
                    break;
                case 5:
                    // cardboard
                    Destroy(Enemy7);
                    break;
                case 6:
                    // cleaner
                    Destroy(Enemy0);
                    break;
                case 7:
                    // kotatsu
                    Destroy(Enemy2);
                    break;
                case 8:
                    // mogura
                    Destroy(Enemy4);
                    break;
                default:
                    break;
            }
        }

        Tutorial();
    }
    void Tutorial() {
        // GameStatus = STS.READY;
        isTutorial = true;
        isReady = false;
        isPlaying = false;
        tutorialMenu.gameObject.SetActive(true);
    }
    void Ready() {
        // GameStatus = STS.READY;
        isTutorial = false;
        isReady = true;
        isPlaying = false;
        //StartBox.SetActive(true); // スタート台を有効に
        Player.SendMessage("Ready", SendMessageOptions.DontRequireReceiver);
        //txtScore.text = "SCORE : 00000";
        //txtMessage.text = "よーい...";
        txtMessage.text = "";
        //Elapsed = 5.0f;
        Elapsed = 3.0f;
        CountDown = "";
        // txtTime.text = Elapsed.ToString("f2") + "s";
        txtTime.text = "";
        txtNavigate.text = "";
        txtRank.text = "";
        MyAudio1.PlayOneShot(countdown); //
    }
    void GameStart() {
        // GameStatus = STS.PLAYING;
        isReady = false;
        isPlaying = true;
        //StartBox.SetActive(false); // スタート台を無効に
        PauseInfo.SetActive(true);
        txtNavigate.text = "";
        txtMessage.text = "";
        txtRank.text = "";
        Elapsed = 0.0f;

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in Enemies) {
            Enemy.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
        }
        Player.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
        
    }
    void Goal() {
        // GameStatus = STS.GOAL;
        isPlaying = false;
        isGoal = true;
        PauseInfo.SetActive(false);
        // Player.SendMessage("DontMove"); // プレイヤーに移動停止を告げる
        txtMessage.text = "ゴール！！";
        // txtTime.text = "0.00s";
        // txtNavigate.text = "Push 3 seconds to Title.";
        // Elapsed = 0.0f;
        Score = Elapsed;
        txtNavigate.text = "    をおしてね";
        EnterImage.SetActive(true);
        MyAudio.Stop(); // BGMを止める
        MyAudio.PlayOneShot(SE_Goal); // ゲームオーバーSEを再生
        MyAudio1.PlayOneShot(kansei); //
        if (playerRank <= 3) {
            GoalParticle.Play();
        }
    }
    void Update() {

        if (isPlaying) { // プレイ中であれば
            Elapsed += Time.deltaTime;
            if (Elapsed >= 1.0f) {
                txtMessage.text = "";
            } else {
                // txtMessage.text = "どん！";
                txtMessage.text = "GO！";
                txtMessage.color = new Color(1, 1, 1, 1.0f);
                txtMessage.fontSize = 168;
            }
            int minutes = Mathf.FloorToInt(Elapsed / 60F);
            int seconds = Mathf.FloorToInt(Elapsed - minutes * 60);
            int mseconds = Mathf.FloorToInt((Elapsed - minutes * 60 - seconds) * 1000);
            txtTime.text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
            // txtTime.text = Elapsed.ToString("f2") + "s";

            if (Input.GetButtonDown("Cancel"/*"Jump"*/)) { // 
                Pause();
            }

            /*
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            */
            int i = 0;
            /*
            CheckCountList[i] = pAction.checkCount;
            ProgressList[i] = pAction.progress;
            RankList[i] = i;
            */
            //Debug.Log("CheckCountList" + i + " : " + pAction.checkCount);
            //Debug.Log("ProgressList" + i + " : " + pAction.progress);
            i++;

            foreach (GameObject Enemy in Enemies) {
                eAction = Enemy.GetComponent<EnemyBAction>();
                if (eAction != null) {
                    CheckCountList[i] = eAction.checkCount;
                    ProgressList[i] = eAction.progress;
                    RankProgressList[i] = i;
                    //Debug.Log("CheckCountList" + i  + " : " + CheckCountList[i]);
                    //Debug.Log("ProgressList" + i + " : " + ProgressList[i]);
                    //Debug.Log("RankProgressList" + i + " : " + RankProgressList[i]);
                    i++;
                }
                // CheckCountList[i] = eAction.checkCount;
                // ProgressList[i] = eAction.progress;
            }
            int nRank = i;
            for (int j = (i-1); j > 0; j--) {
                if (pAction.checkCount < CheckCountList[j]) {
                } else if (pAction.checkCount == CheckCountList[j]) {
                    if (pAction.progress < ProgressList[j]) {
                        nRank--;
                    }
                } else {
                    nRank--;
                }
                /*
                if (pAction.progress < ProgressList[j] && pAction.checkCount >= CheckCountList[j]) {
                    nRank--;
                    // Debug.Log("pAction.progress:" + pAction.progress + " ProgressList[" + j + "]:" + ProgressList[j]);
                    // Debug.Log("pAction.checkCount:" + pAction.checkCount + " CheckCountList[" + j + "]:" + CheckCountList[j]);
                    // Debug.Log("nRank:" + j);
                }
                */
            }
            playerRank = nRank;
            /*
            if (nRank != i) {
                for (int j = (i - 2); j >= nRank; j--) {
                    RankProgressList[j + 1] = RankProgressList[j];
                }
                RankProgressList[nRank] = pAction.progress;
                playerRank = nRank;
            } else {
                playerRank = i;
            }
            */
            // Debug.Log("playerRank:" + playerRank);

            // int playerProgress = Player.SendMessage("getProgress");
            // float playerCheckCount = Player.SendMessage("getCheckCount");
            txtCheckCount.text = "" + pAction.checkCount;
            txtProgress.text = "" + pAction.progress;
            if (playerRank == 1) {
                txtRank.text = playerRank + "st";
                txtRank.color = new Color(218.0f / 255.0f, 179.0f / 255.0f, 0.0f / 255.0f, 1.0f);
            } else if (playerRank == 2) {
                txtRank.text = playerRank + "nd";
                txtRank.color = new Color(190.0f / 255.0f, 193.0f / 255.0f, 195.0f / 255.0f, 1.0f);
            } else if (playerRank == 3) {
                txtRank.text = playerRank + "rd";
                txtRank.color = new Color(196.0f / 255.0f, 112.0f / 255.0f, 34.0f / 255.0f, 1.0f);
            } else {
                txtRank.text = playerRank + "th";
                txtRank.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }

        } else { // プレイ中でなければ
            if (isPause) { // ポーズ中であれば
                if (!isHelp) {
                    menu1SelectText.gameObject.SetActive(false);
                    menu2SelectText.gameObject.SetActive(false);
                    menu3SelectText.gameObject.SetActive(false);
                    if (Input.GetAxis("Vertical") > 0.1f && !keyDown) {
                        upFlg = true;
                        keyDown = true;
                    }
                    if (Input.GetAxis("Vertical") < -0.1f && !keyDown) {
                        downFlg = true;
                        keyDown = true;
                    }
                    if (Input.GetAxis("Vertical") >= -0.1f && Input.GetAxis("Vertical") <= 0.1f) {
                        upFlg = false;
                        downFlg = false;
                        keyDown = false;
                    }
                    if (upFlg && keyDown) {
                        menuNo--;
                        MyAudio.PlayOneShot(cursor);
                        upFlg = false;
                    }
                    if (downFlg && keyDown) {
                        menuNo++;
                        MyAudio.PlayOneShot(cursor);
                        downFlg = false;
                    }
                    if (menuNo > 3) {
                        menuNo = 1;
                    }
                    if (menuNo < 1) {
                        menuNo = 3;
                    }
                    switch (menuNo) {
                        case 1:
                            menu1SelectText.gameObject.SetActive(true);
                            menu2SelectText.gameObject.SetActive(false);
                            menu3SelectText.gameObject.SetActive(false);
                            break;
                        case 2:
                            menu1SelectText.gameObject.SetActive(false);
                            menu2SelectText.gameObject.SetActive(true);
                            menu3SelectText.gameObject.SetActive(false);
                            break;
                        case 3:
                            menu1SelectText.gameObject.SetActive(false);
                            menu2SelectText.gameObject.SetActive(false);
                            menu3SelectText.gameObject.SetActive(true);
                            break;
                        default:
                            break;
                    }
                    if (/*Input.GetButtonDown("Fire2") || */Input.GetKeyDown(KeyCode.Return)) {
                        MyAudio.PlayOneShot(submit);
                        switch (menuNo) {
                            case 1:
                                UnPause();
                                break;
                            case 2:
                                ShowHelp();
                                break;
                            case 3:
                                SceneManager.LoadScene("Menu"); // メインシーンへ
                                break;
                            default:
                                break;
                        }
                    }
                    if (Input.GetButtonDown("Cancel"/*"Jump"*/)) {
                        MyAudio.PlayOneShot(submit);
                        UnPause();
                    }
                } else {
                    if (/*Input.GetButtonDown("Fire2") || */Input.GetKeyDown(KeyCode.Return)) {
                        MyAudio.PlayOneShot(submit);
                        HideHelp();
                    }
                }
            } else { // ポーズ中でなければ
                if (isTutorial) {
                    Elapsed += Time.deltaTime; // 時間を加算
                    Elapsed %= 1.0f; // 1秒ごとにリセット
                    menuSelectText.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
                    if (/*Input.GetButtonDown("Fire2") || */Input.GetKeyDown(KeyCode.Return)) {
                        tutorialMenu.gameObject.SetActive(false);
                        Ready();
                    }
                } else if (isReady) {
                    Elapsed -= Time.deltaTime;

                    if (Elapsed <= 0.0f) {
                        GameStart();
                    } else if (Elapsed < 3.0f) {
                        CountDown = "" + Mathf.Ceil(Elapsed); //切り上げた整数部を表示する
                        txtMessage.text = CountDown;
                        txtMessage.color = new Color(1, 1, 1, Elapsed - Mathf.Floor(Elapsed));
                        txtMessage.fontSize = Mathf.FloorToInt((1 + (Elapsed - Mathf.Floor(Elapsed))) * 96);
                    }
                } else if (isRanking) {
                    // メッセージ点滅表示
                    /*
                    Elapsed += Time.deltaTime; // 時間を加算
                    Elapsed %= 1.0f; // 1秒ごとにリセット
                    txtRankMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
                    */
                    if (/*Input.GetButtonDown("Fire2") || */Input.GetKeyDown(KeyCode.Return)) {
                        SceneManager.LoadScene("Menu"); // タイトルシーンに戻る
                    }

                } else if (isGoal) {
                    txtMessage.text = "ゴール！";
                    txtMessage.color = new Color(1, 1, 1, 1.0f);
                    txtMessage.fontSize = 168;
                    /*
                    Elapsed += Time.deltaTime; // 時間を加算
                    Elapsed %= 1.0f; // 1秒ごとにリセット
                    imgMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
                    */

                    if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Return)) {
                        ShowRanking();
                    }

                }
            }
        }
        /*
        switch (GameStatus) {
            case STS.READY:
                Elapsed -= Time.deltaTime;

                if (Elapsed <= 0.0f) {
                    GameStart();
                } else if (Elapsed < 3.0f) {
                    CountDown = "" + Mathf.Ceil(Elapsed); //切り上げた整数部を表示する
                    txtMessage.text = CountDown;
                    txtMessage.color = new Color(1, 1, 1, Elapsed - Mathf.Floor(Elapsed));
                    txtMessage.fontSize = Mathf.FloorToInt((1 + (Elapsed - Mathf.Floor(Elapsed))) * 60);
                }
                break;
            case STS.PLAYING:
                Elapsed += Time.deltaTime;
                if (Elapsed >= 1.0f) {
                    txtMessage.text = "";
                } else {
                    txtMessage.text = "START!";
                    txtMessage.color = new Color(1, 1, 1, 1.0f);
                    txtMessage.fontSize = 60;
                }
                txtTime.text = Elapsed.ToString("f2") + "s";
                break;
            case STS.GOAL:
                txtMessage.text = "GOAL!";
                txtMessage.color = new Color(1, 1, 1, 1.0f);
                txtMessage.fontSize = 60;
                break;
            default:
                break;
        }
        */
    }

    void Pause() {
        // 
        isPlaying = false;
        isPause = true;
        pauseMenu.gameObject.SetActive(true); // ポーズメニューUIを表示する
        menuNo = 1;
        menu1SelectText.gameObject.SetActive(false);
        menu2SelectText.gameObject.SetActive(false);
        menu3SelectText.gameObject.SetActive(false);
        // 各オブジェクトの制御
        Player.SendMessage("DontMove"); // プレイヤーオブジェクトの動作を止める
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in Enemies) {
            Enemy.SendMessage("DontMove", SendMessageOptions.DontRequireReceiver);
        }
        MyAudio.Stop(); // BGMを止める
        MyAudio.PlayOneShot(submit);
    }

    void UnPause() {
        // 
        isPlaying = true;
        isPause = false;
        pauseMenu.gameObject.SetActive(false); // ポーズメニューUIを非表示にする
        // 各オブジェクトの制御
        Player.SendMessage("Move"); // プレイヤーオブジェクトの動作を再開する
        // 各オブジェクトの制御
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in Enemies) {
            Enemy.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
        }
        MyAudio.Play(); // BGMを再生
    }

    void ShowHelp() {
        isHelp = true;
        helpMenu.gameObject.SetActive(true); // ヘルプメニューUIを表示する
    }

    void HideHelp() {
        isHelp = false;
        helpMenu.gameObject.SetActive(false); // ヘルプメニューUIを隠す
    }
    /*
    void PowerUp() {
        Attack++;
        AttackGauge[Attack].gameObject.SetActive(true);
    }
    */

    void ShowRanking() {
        if (PlayerPrefs.HasKey("R_B1")) {
            Debug.Log("DataLoad"); //スコアデータをロード
            for (idx = 1; idx <= 5; idx++) {
                Rank[idx] = PlayerPrefs.GetFloat("R_B" + idx);
                Rankin[idx] = PlayerPrefs.GetInt("R_BI" + idx);
                RankChara[idx] = PlayerPrefs.GetInt("R_BC" + idx);
                // Debug.Log("Load:Rank["+idx+"]" + Rank[idx]);
                // Debug.Log("Load:Rankin[" + idx + "]" + Rankin[idx]);
                // Debug.Log("Load:RankChara[" + idx + "]" + RankChara[idx]);
            }
        } else {
            Debug.Log("DataInit"); //初回起動なのでスコアデータ作成
            for (idx = 1; idx <= 5; idx++) {
                PlayerPrefs.SetFloat("R_B" + idx, (99.0f * 60.0f) + 59.999f); //全部ゼロ
                PlayerPrefs.SetInt("R_BI" + idx, 0); //全部ゼロ
                PlayerPrefs.SetInt("R_BC" + idx, 0); //全部ゼロ
                Rank[idx] = PlayerPrefs.GetFloat("R_B" + idx);
                Rankin[idx] = PlayerPrefs.GetInt("R_BI" + idx);
                RankChara[idx] = PlayerPrefs.GetInt("R_BC" + idx);
                // Debug.Log("Reset:Rank[" + idx + "]" + Rank[idx]);
                // Debug.Log("Reset:Rankin[" + idx + "]" + Rankin[idx]);
                // Debug.Log("Reset:RankChara[" + idx + "]" + RankChara[idx]);
            }
        }
        int NewRank = 0;
        for (idx = 5; idx > 0; idx--) {
            if (Score < Rank[idx]) {
                NewRank = idx;
            }
        }
        if (NewRank != 0) {
            for (idx = 4; idx >= NewRank; idx--) {
                Rank[idx + 1] = Rank[idx];
                Rankin[idx + 1] = Rankin[idx];
                RankChara[idx + 1] = RankChara[idx];
            }
            Rank[NewRank] = Score;
            Rankin[NewRank] = playerRank;
            RankChara[NewRank] = PlayerPrefs.GetInt("player1Character");
            for (idx = 1; idx <= 5; idx++) {
                PlayerPrefs.SetFloat("R_B" + idx, Rank[idx]);
                PlayerPrefs.SetInt("R_BI" + idx, Rankin[idx]);
                PlayerPrefs.SetInt("R_BC" + idx, RankChara[idx]);
            }
        }
        for (idx = 1; idx <= 5; idx++) {
            // Debug.Log("Rank[" + idx + "]" + Rank[idx]);
            // Debug.Log("Rankin[" + idx + "]" + Rankin[idx]);
            // Debug.Log("RankChara[" + idx + "]" + RankChara[idx]);
            if (NewRank == idx) {
                newText[idx].gameObject.SetActive(true);
                for (int i = 1; i <= 8; i++) {
                    if (i == PlayerPrefs.GetInt("player1Character")) {
                        switch (idx) {
                            case 1:
                                rankChara1[i].enabled = true;
                                break;
                            case 2:
                                rankChara2[i].enabled = true;
                                break;
                            case 3:
                                rankChara3[i].enabled = true;
                                break;
                            case 4:
                                rankChara4[i].enabled = true;
                                break;
                            case 5:
                                rankChara5[i].enabled = true;
                                break;
                            default:
                                break;
                        }
                    } else {
                        switch (idx) {
                            case 1:
                                rankChara1[i].enabled = false;
                                break;
                            case 2:
                                rankChara2[i].enabled = false;
                                break;
                            case 3:
                                rankChara3[i].enabled = false;
                                break;
                            case 4:
                                rankChara4[i].enabled = false;
                                break;
                            case 5:
                                rankChara5[i].enabled = false;
                                break;
                            default:
                                break;
                        }

                    }
                }
            } else {
                newText[idx].gameObject.SetActive(false);
                for (int i = 1; i <= 8; i++) {
                    if (i == RankChara[idx]) {
                        switch (idx) {
                            case 1:
                                rankChara1[i].enabled = true;
                                break;
                            case 2:
                                rankChara2[i].enabled = true;
                                break;
                            case 3:
                                rankChara3[i].enabled = true;
                                break;
                            case 4:
                                rankChara4[i].enabled = true;
                                break;
                            case 5:
                                rankChara5[i].enabled = true;
                                break;
                            default:
                                break;
                        }
                    } else {
                        switch (idx) {
                            case 1:
                                rankChara1[i].enabled = false;
                                break;
                            case 2:
                                rankChara2[i].enabled = false;
                                break;
                            case 3:
                                rankChara3[i].enabled = false;
                                break;
                            case 4:
                                rankChara4[i].enabled = false;
                                break;
                            case 5:
                                rankChara5[i].enabled = false;
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
            if (float.IsInfinity(Rank[idx]) || float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
                scoreText[idx].text = "--'--\"---";
            } else {
                int minutes = Mathf.FloorToInt(Rank[idx] / 60F);
                int seconds = Mathf.FloorToInt(Rank[idx] - minutes * 60);
                int mseconds = Mathf.FloorToInt((Rank[idx] - minutes * 60 - seconds) * 1000);
                scoreText[idx].text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                // scoreText[idx].text = Rank[idx].ToString("f2") + "s";
                /*
                if (string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds) == "16'39\"989") {
                    scoreText[idx].text = "--'--\"---";
                }
                */
            }

            if (Rankin[idx] == 0) {
                rankText[idx].text = "";
            } else if (Rankin[idx] == 1) {
                rankText[idx].text = Rankin[idx] + "st";
                rankText[idx].color = new Color(218.0f / 255.0f, 179.0f / 255.0f, 0.0f / 255.0f, 1.0f);
            } else if (Rankin[idx] == 2) {
                rankText[idx].text = Rankin[idx] + "nd";
                rankText[idx].color = new Color(190.0f / 255.0f, 193.0f / 255.0f, 195.0f / 255.0f, 1.0f);
            } else if (Rankin[idx] == 3) {
                rankText[idx].text = Rankin[idx] + "rd";
                rankText[idx].color = new Color(196.0f / 255.0f, 112.0f / 255.0f, 34.0f / 255.0f, 1.0f);
            } else {
                rankText[idx].text = Rankin[idx] + "th";
                rankText[idx].color = new Color(149.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
            }
        }

        isRanking = true;
        txtMessage.gameObject.SetActive(false);
        rankingMenu.gameObject.SetActive(true); // ランキングメニューUIを表示する
    }

    public void Dash() {
        float rd = Random.Range(1, 4);
        if (rd <= 1) {
            MyAudio1.PlayOneShot(cat1); //
        } else if (rd <= 2) {
            MyAudio1.PlayOneShot(cat2); //
        } else if (rd <= 3) {
            MyAudio1.PlayOneShot(cat3); //
        } else if (rd <= 4) {
            MyAudio1.PlayOneShot(cat4); //
        }
        MyAudio2.PlayOneShot(pyun); //
    }
    /*
    IEnumerator FishSpawner() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
            if (isPlaying) {
                float RanX = Random.Range(-25.0f, 25.0f);
                float RanZ = Random.Range(-15.0f, 15.0f);
                Vector3 pos = new Vector3(RanX, 0.0f, RanZ); // 生成位置決定
                Instantiate(Fish, pos, Quaternion.Euler(0, 0, 0));
            }
        }
    }
    */
    public void OnClickTutorialReturn() {
        MyAudio.PlayOneShot(submit);
        tutorialMenu.gameObject.SetActive(false);
        Ready();
    }
    public void OnClickEsc() {
        if (isPlaying) {
            MyAudio.PlayOneShot(submit);
            Pause();
        }
    }
    public void OnClickPauseMenu1() {
        MyAudio.PlayOneShot(submit);
        UnPause();
    }
    public void OnClickPauseMenu2() {
        MyAudio.PlayOneShot(submit);
        ShowHelp();
    }
    public void OnClickPauseMenu3() {
        MyAudio.PlayOneShot(submit);
        SceneManager.LoadScene("Menu"); // タイトルシーンに戻る
    }
    public void OnClickHelpReturn() {
        MyAudio.PlayOneShot(submit);
        HideHelp();
    }
    public void OnClickGoalReturn() {
        MyAudio.PlayOneShot(submit);
        ShowRanking();
    }
    public void OnClickRankingReturn() {
        MyAudio.PlayOneShot(submit);
        SceneManager.LoadScene("Menu"); // タイトルシーンに戻る
    }
}
