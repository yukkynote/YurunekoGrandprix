using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //uGUIを利用するのに必要
using UnityEngine.SceneManagement; // シーン制御用

public class GameManager2:MonoBehaviour
{
    // ゲームオブジェクト
    GameObject StartBox; // スタート台
    GameObject Player1; // プレイヤー
    GameObject Player2; // プレイヤー
    GameObject[] Enemies;
    Player1Action p1Action;
    Player2Action p2Action;
    Enemy2Action e2Action;
    public GameObject Fish;
    public GameObject Enemy0;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Enemy6;
    public GameObject Enemy7;

    public GameObject MobileJoystick1;
    public GameObject MobileJoystick2;

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
    public Text txtMessage1;
    public Text txtMessage2;
    public Text txtNavigate;
    public Text txtTime;
    public Text txtTime2;
    public Text txtRank;
    public Text txtRank2;
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
    public Image[] rankChara1;
    public Image[] rankChara2;
    public Image[] rankChara3;
    public Image[] rankChara4;
    public Image[] rankChara5;
    public Text[] rankPlayerText;
    public GameObject EnterImage;

    public GameObject PauseInfo;

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
    float Elapsed1; // 経過時間
    float Elapsed2; // 経過時間
    int myScore; // スコア
    int myScore1; // スコア
    int myScore2; // スコア
    string CountDown;
    public float Score;
    public float Score1;
    public float Score2;
    bool isPlaying;                         // プレイ状態(true:プレイしている)
    bool isTutorial;
    bool isPlaying1;
    bool isPlaying2;
    bool isReady;
    bool isGoal;                           // ゴール状態(true:ゴールしている)
    bool isGoal1;
    bool isGoal2;
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
    int playerRank1;
    int playerRank2;

    public GameObject PatGoal1;
    ParticleSystem GoalParticle1;
    public GameObject PatGoal2;
    ParticleSystem GoalParticle2;


    void Start() {
        Application.targetFrameRate = 30;
        // パラメータを初期化
        isPlaying = false; // プレイ状態を初期化
        isPlaying1 = false; // プレイ状態を初期化
        isPlaying2 = false; // プレイ状態を初期化
        isTutorial = false;
        isReady = false; // プレイ前状態を初期化
        isGoal = false; // ゴール状態を初期化
        isGoal1 = false; // ゴール状態を初期化
        isGoal2 = false; // ゴール状態を初期化
        isPause = false; // ポーズ状態を初期化
        isHelp = false; // ヘルプ状態を初期化
        isRanking = false; // ランキング表示状態を初期化

        pauseMenu.gameObject.SetActive(false); // ポーズメニューUIを非表示にする
        helpMenu.gameObject.SetActive(false); // ヘルプメニューUIを非表示にする
        rankingMenu.gameObject.SetActive(false); // ランキングメニューUIを非表示にする

        // EnterImage.enabled = false;
        EnterImage.SetActive(false);
        PauseInfo.SetActive(false);

        MobileJoystick1.SetActive(Device.isMobile);
        MobileJoystick2.SetActive(Device.isMobile);

        MyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio1 = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio2 = GetComponent<AudioSource>(); // オーディオソースを取得

        Player1 = GameObject.FindGameObjectWithTag("Player1");
        p1Action = Player1.GetComponent<Player1Action>();
        Player2 = GameObject.FindGameObjectWithTag("Player2");
        p2Action = Player2.GetComponent<Player2Action>();
        //StartBox = GameObject.FindGameObjectWithTag("Box");
        // StartCoroutine("FishSpawner"); // 魚生成処理を起動

        GoalParticle1 = PatGoal1.GetComponent<ParticleSystem>();
        GoalParticle1.Stop();

        GoalParticle2 = PatGoal2.GetComponent<ParticleSystem>();
        GoalParticle2.Stop();

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
        if (PlayerPrefs.HasKey("player2Character") && (PlayerPrefs.GetInt("player1Character") != PlayerPrefs.GetInt("player2Character"))) {
            switch (PlayerPrefs.GetInt("player2Character")) {
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
        isPlaying1 = false;
        isPlaying2 = false;
        tutorialMenu.gameObject.SetActive(true);
    }
    void Ready() {
        // GameStatus = STS.READY;
        isTutorial = false;
        isReady = true;
        isPlaying = false;
        isPlaying1 = false;
        isPlaying2 = false;
        //StartBox.SetActive(true); // スタート台を有効に
        Player1.SendMessage("Ready", SendMessageOptions.DontRequireReceiver);
        Player2.SendMessage("Ready", SendMessageOptions.DontRequireReceiver);
        //txtScore.text = "SCORE : 00000";
        // txtMessage.text = "よーい...";
        txtMessage.text = "";
        // Elapsed = 5.0f;
        Elapsed = 3.0f;
        Elapsed1 = Elapsed;
        Elapsed2 = Elapsed;
        CountDown = "";
        // txtTime.text = Elapsed.ToString("f2") + "s";
        txtTime.text = "";
        txtTime2.text = "";
        txtNavigate.text = "";
        txtRank.text = "";
        txtRank2.text = "";
        MyAudio1.PlayOneShot(countdown); //
    }
    void GameStart() {
        // GameStatus = STS.PLAYING;
        isReady = false;
        isPlaying = true;
        isPlaying1 = true;
        isPlaying2 = true;
        //StartBox.SetActive(false); // スタート台を無効に
        PauseInfo.SetActive(true);
        txtNavigate.text = "";
        txtMessage.text = "";
        txtRank.text = "";
        txtRank2.text = "";
        Elapsed = 0.0f;
        Elapsed1 = Elapsed;
        Elapsed2 = Elapsed;

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in Enemies) {
            Enemy.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
        }
        Player1.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
        Player2.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
    }
    void Goal() {
        // GameStatus = STS.GOAL;
        isPlaying = false;
        isGoal = true;
        // Player1.SendMessage("DontMove"); // プレイヤーに移動停止を告げる
        // txtMessage.text = "ゴール！！";
        // txtTime.text = "0.00s";
        // txtNavigate.text = "Push 3 seconds to Title.";
        // Elapsed = 0.0f;
        Score = Elapsed;
        // txtNavigate.text = "    をおしてね";
        // EnterImage.enabled = true;
        // EnterImage.SetActive(true);
        MyAudio.Stop(); // BGMを止める
        MyAudio.PlayOneShot(SE_Goal); // ゲームオーバーSEを再生
    }
    void Goal1() {
        // GameStatus = STS.GOAL;
        isPlaying = false;
        isGoal = true;
        isGoal1 = true;
        // Player1.SendMessage("DontMove"); // プレイヤーに移動停止を告げる
        // txtMessage.text = "ゴール！！";
        txtMessage1.text = "ゴール！！";
        txtMessage1.color = new Color(1, 1, 1, 1.0f);
        txtMessage1.fontSize = 128;
        // txtTime.text = "0.00s";
        // txtNavigate.text = "Push 3 seconds to Title.";
        // Elapsed = 0.0f;
        Score = Elapsed;
        Score1 = Elapsed1;
        //txtNavigate.text = "エンターキーをおしてね";
        MyAudio.Stop(); // BGMを止める
        MyAudio1.PlayOneShot(kansei); //
        MyAudio.PlayOneShot(SE_Goal); // ゲームオーバーSEを再生
        if (playerRank1 <= 3) {
            GoalParticle1.Play();
        }
    }
    void Goal2() {
        // GameStatus = STS.GOAL;
        isPlaying = false;
        isGoal = true;
        isGoal2 = true;
        // Player1.SendMessage("DontMove"); // プレイヤーに移動停止を告げる
        // txtMessage.text = "ゴール！！";
        txtMessage2.text = "ゴール！！";
        txtMessage2.color = new Color(1, 1, 1, 1.0f);
        txtMessage2.fontSize = 128;
        // txtTime.text = "0.00s";
        // txtNavigate.text = "Push 3 seconds to Title.";
        // Elapsed = 0.0f;
        Score = Elapsed;
        Score2 = Elapsed2;
        //txtNavigate.text = "エンターキーをおしてね";
        MyAudio.Stop(); // BGMを止める
        MyAudio2.PlayOneShot(kansei); //
        MyAudio.PlayOneShot(SE_Goal); // ゲームオーバーSEを再生
        if (playerRank2 <= 3) {
            GoalParticle2.Play();
        }
    }
    void Update() {

        if (isPlaying) { // プレイ中であれば
            Elapsed += Time.deltaTime;
            Elapsed1 = Elapsed;
            Elapsed2 = Elapsed;
            if (Elapsed >= 1.0f) {
                txtMessage.text = "";
            } else {
                // txtMessage.text = "どん！";
                txtMessage.text = "GO！";
                txtMessage.color = new Color(1, 1, 1, 1.0f);
                txtMessage.fontSize = 168;
            }
            if (!isGoal1) {
                int minutes = Mathf.FloorToInt(Elapsed1 / 60F);
                int seconds = Mathf.FloorToInt(Elapsed1 - minutes * 60);
                int mseconds = Mathf.FloorToInt((Elapsed1 - minutes * 60 - seconds) * 1000);
                txtTime.text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                // txtTime.text = Elapsed.ToString("f2") + "s";
            } else {

            }
            if (!isGoal2) {
                int minutes = Mathf.FloorToInt(Elapsed2 / 60F);
                int seconds = Mathf.FloorToInt(Elapsed2 - minutes * 60);
                int mseconds = Mathf.FloorToInt((Elapsed2 - minutes * 60 - seconds) * 1000);
                txtTime2.text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                // txtTime2.text = Elapsed.ToString("f2") + "s";
            } else {

            }
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
                e2Action = Enemy.GetComponent<Enemy2Action>();
                if (e2Action != null) {
                    CheckCountList[i] = e2Action.checkCount;
                    ProgressList[i] = e2Action.progress;
                    RankProgressList[i] = i;
                    //Debug.Log("CheckCountList" + i  + " : " + CheckCountList[i]);
                    //Debug.Log("ProgressList" + i + " : " + ProgressList[i]);
                    //Debug.Log("RankProgressList" + i + " : " + RankProgressList[i]);
                    i++;
                }
                // CheckCountList[i] = e2Action.checkCount;
                // ProgressList[i] = e2Action.progress;
            }
            /*
            CheckCountList[i] = p1Action.checkCount;
            ProgressList[i] = p1Action.progress;
            RankProgressList[i] = i;
            i++;
            
            CheckCountList[i] = p2Action.checkCount;
            ProgressList[i] = p2Action.progress;
            RankProgressList[i] = i;
            i++;
            */
            int playerOrder1 = 1;
            int playerOrder2 = 2;
            if (p2Action.checkCount < p1Action.checkCount) {

            } else if (p2Action.checkCount == p1Action.checkCount) {
                if (p2Action.progress < p1Action.progress) {
                    playerOrder1 = 2;
                    playerOrder2 = 1;
                }
            } else {
                playerOrder1 = 2;
                playerOrder2 = 1;
            }
            if (playerOrder1 == 1) {

                int nRank1 = i;
                for (int j = (i - 1); j > 0; j--) {
                    if (p1Action.checkCount < CheckCountList[j]) {
                    } else if (p1Action.checkCount == CheckCountList[j]) {
                        if (p1Action.progress < ProgressList[j]) {
                            nRank1--;
                        }
                    } else {
                        nRank1--;
                    }
                }
                playerRank1 = nRank1;

                CheckCountList[i] = p1Action.checkCount;
                ProgressList[i] = p1Action.progress;
                RankProgressList[i] = i;
                i++;

                int nRank2 = i;
                for (int j = (i - 1); j > 0; j--) {
                    if (p2Action.checkCount < CheckCountList[j]) {
                    } else if (p2Action.checkCount == CheckCountList[j]) {
                        if (p2Action.progress < ProgressList[j]) {
                            nRank2--;
                        }
                    } else {
                        nRank2--;
                    }
                }
                playerRank2 = nRank2;
            } else {

                int nRank2 = i;
                for (int j = (i - 1); j > 0; j--) {
                    if (p2Action.checkCount < CheckCountList[j]) {
                    } else if (p2Action.checkCount == CheckCountList[j]) {
                        if (p2Action.progress < ProgressList[j]) {
                            nRank2--;
                        }
                    } else {
                        nRank2--;
                    }
                }
                playerRank2 = nRank2;

                CheckCountList[i] = p2Action.checkCount;
                ProgressList[i] = p2Action.progress;
                RankProgressList[i] = i;
                i++;

                int nRank1 = i;
                for (int j = (i - 1); j > 0; j--) {
                    if (p1Action.checkCount < CheckCountList[j]) {
                    } else if (p1Action.checkCount == CheckCountList[j]) {
                        if (p1Action.progress < ProgressList[j]) {
                            nRank1--;
                        }
                    } else {
                        nRank1--;
                    }
                }
                playerRank1 = nRank1;
            }

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
            txtCheckCount.text = "" + p1Action.checkCount;
            txtProgress.text = "" + p1Action.progress;
            if (playerRank1 == 1) {
                txtRank.text = playerRank1 + "st";
                txtRank.color = new Color(218.0f / 255.0f, 179.0f / 255.0f, 0.0f / 255.0f, 1.0f);
            } else if (playerRank1 == 2) {
                txtRank.text = playerRank1 + "nd";
                txtRank.color = new Color(190.0f / 255.0f, 193.0f / 255.0f, 195.0f / 255.0f, 1.0f);
            } else if (playerRank1 == 3) {
                txtRank.text = playerRank1 + "rd";
                txtRank.color = new Color(196.0f / 255.0f, 112.0f / 255.0f, 34.0f / 255.0f, 1.0f);
            } else {
                txtRank.text = playerRank1 + "th";
                txtRank.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            if (playerRank2 == 1) {
                txtRank2.text = playerRank2 + "st";
                txtRank2.color = new Color(218.0f / 255.0f, 179.0f / 255.0f, 0.0f / 255.0f, 1.0f);
            } else if (playerRank2 == 2) {
                txtRank2.text = playerRank2 + "nd";
                txtRank2.color = new Color(190.0f / 255.0f, 193.0f / 255.0f, 195.0f / 255.0f, 1.0f);
            } else if (playerRank2 == 3) {
                txtRank2.text = playerRank2 + "rd";
                txtRank2.color = new Color(196.0f / 255.0f, 112.0f / 255.0f, 34.0f / 255.0f, 1.0f);
            } else {
                txtRank2.text = playerRank2 + "th";
                txtRank2.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
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
                    // txtMessage.text = "ゴール！";
                    txtMessage.color = new Color(1, 1, 1, 1.0f);
                    txtMessage.fontSize = 84;
                    /*
                    Elapsed += Time.deltaTime; // 時間を加算
                    Elapsed %= 1.0f; // 1秒ごとにリセット
                    imgMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
                    */
                    if (isGoal1 && isGoal2) {
                        txtNavigate.text = "    をおしてね";
                        // EnterImage.enabled = true;
                        EnterImage.SetActive(true);
                        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Return)) {
                            ShowRanking();
                        }
                    } else {
                        Elapsed += Time.deltaTime;
                        if (!isGoal1) {
                            Elapsed1 = Elapsed;
                            int minutes = Mathf.FloorToInt(Elapsed1 / 60F);
                            int seconds = Mathf.FloorToInt(Elapsed1 - minutes * 60);
                            int mseconds = Mathf.FloorToInt((Elapsed1 - minutes * 60 - seconds) * 1000);
                            txtTime.text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                            // txtTime.text = Elapsed.ToString("f2") + "s";
                        } else {

                        }
                        if (!isGoal2) {
                            Elapsed2 = Elapsed;
                            int minutes = Mathf.FloorToInt(Elapsed2 / 60F);
                            int seconds = Mathf.FloorToInt(Elapsed2 - minutes * 60);
                            int mseconds = Mathf.FloorToInt((Elapsed2 - minutes * 60 - seconds) * 1000);
                            txtTime2.text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                            // txtTime2.text = Elapsed.ToString("f2") + "s";
                        } else {

                        }
                        if (Input.GetButtonDown("Cancel"/*"Jump"*/)) { // 
                            Pause();
                        }
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
        Player1.SendMessage("DontMove"); // プレイヤーオブジェクトの動作を止める
        Player2.SendMessage("DontMove"); // プレイヤーオブジェクトの動作を止める
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
        Player1.SendMessage("Move"); // プレイヤーオブジェクトの動作を再開する
        Player2.SendMessage("Move"); // プレイヤーオブジェクトの動作を再開する
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
        if (PlayerPrefs.HasKey("R1")) {
            Debug.Log("DataLoad"); //スコアデータをロード
            for (idx = 1; idx <= 5; idx++) {
                Rank[idx] = PlayerPrefs.GetFloat("R" + idx);
                Rankin[idx] = PlayerPrefs.GetInt("RI" + idx);
                RankChara[idx] = PlayerPrefs.GetInt("RC" + idx);
            }
        } else {
            Debug.Log("DataInit"); //初回起動なのでスコアデータ作成
            for (idx = 1; idx <= 5; idx++) {
                PlayerPrefs.SetFloat("R" + idx, (99.0f * 60.0f) + 59.999f); //全部ゼロ
                PlayerPrefs.SetInt("RI" + idx, 0); //全部ゼロ
                PlayerPrefs.SetInt("RC" + idx, 0); //全部ゼロ
                Rank[idx] = PlayerPrefs.GetFloat("R" + idx);
                Rankin[idx] = PlayerPrefs.GetInt("RI" + idx);
                RankChara[idx] = PlayerPrefs.GetInt("RC" + idx);
            }
        }
        // Score1がScore2より小さい場合：(逆パターンも作る)
        int NewRank = 0;
        int NewRank1 = 0;
        int NewRank2 = 0;
        if (Score1 <= Score2) {
            for (idx = 5; idx > 0; idx--) {
                if (Score1 < Rank[idx]) {
                    NewRank1 = idx;
                }
            }
            if (NewRank1 != 0) {
                for (idx = 4; idx >= NewRank1; idx--) {
                    Rank[idx + 1] = Rank[idx];
                    Rankin[idx + 1] = Rankin[idx];
                    RankChara[idx + 1] = RankChara[idx];
                }
                Rank[NewRank1] = Score1;
                Rankin[NewRank1] = playerRank1;
                RankChara[NewRank1] = PlayerPrefs.GetInt("player1Character");
                for (idx = 1; idx <= 5; idx++) {
                    PlayerPrefs.SetFloat("R" + idx, Rank[idx]);
                    PlayerPrefs.SetInt("RI" + idx, Rankin[idx]);
                    PlayerPrefs.SetInt("RC" + idx, RankChara[idx]);
                }
            }
            for (idx = 5; idx > 0; idx--) {
                if (Score2 < Rank[idx]) {
                    NewRank2 = idx;
                }
            }
            if (NewRank2 != 0) {
                for (idx = 4; idx >= NewRank2; idx--) {
                    Rank[idx + 1] = Rank[idx];
                    Rankin[idx + 1] = Rankin[idx];
                    RankChara[idx + 1] = RankChara[idx];
                }
                Rank[NewRank2] = Score2;
                Rankin[NewRank2] = playerRank2;
                RankChara[NewRank2] = PlayerPrefs.GetInt("player2Character");
                for (idx = 1; idx <= 5; idx++) {
                    PlayerPrefs.SetFloat("R" + idx, Rank[idx]);
                    PlayerPrefs.SetInt("RI" + idx, Rankin[idx]);
                    PlayerPrefs.SetInt("RC" + idx, RankChara[idx]);
                }
            }


        } else {
            for (idx = 5; idx > 0; idx--) {
                if (Score2 < Rank[idx]) {
                    NewRank2 = idx;
                }
            }
            if (NewRank2 != 0) {
                for (idx = 4; idx >= NewRank2; idx--) {
                    Rank[idx + 1] = Rank[idx];
                    Rankin[idx + 1] = Rankin[idx];
                    RankChara[idx + 1] = RankChara[idx];
                }
                Rank[NewRank2] = Score2;
                Rankin[NewRank2] = playerRank2;
                RankChara[NewRank2] = PlayerPrefs.GetInt("player2Character");
                for (idx = 1; idx <= 5; idx++) {
                    PlayerPrefs.SetFloat("R" + idx, Rank[idx]);
                    PlayerPrefs.SetInt("RI" + idx, Rankin[idx]);
                    PlayerPrefs.SetInt("RC" + idx, RankChara[idx]);
                }
            }
            for (idx = 5; idx > 0; idx--) {
                if (Score1 < Rank[idx]) {
                    NewRank1 = idx;
                }
            }
            if (NewRank1 != 0) {
                for (idx = 4; idx >= NewRank1; idx--) {
                    Rank[idx + 1] = Rank[idx];
                    Rankin[idx + 1] = Rankin[idx];
                    RankChara[idx + 1] = RankChara[idx];
                }
                Rank[NewRank1] = Score1;
                Rankin[NewRank1] = playerRank1;
                RankChara[NewRank1] = PlayerPrefs.GetInt("player1Character");
                for (idx = 1; idx <= 5; idx++) {
                    PlayerPrefs.SetFloat("R" + idx, Rank[idx]);
                    PlayerPrefs.SetInt("RI" + idx, Rankin[idx]);
                    PlayerPrefs.SetInt("RC" + idx, RankChara[idx]);
                }
            }
        }

        for (idx = 1; idx <= 5; idx++) {
            if (NewRank1 == idx) {
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
                rankPlayerText[idx].text = "1P";
                rankPlayerText[idx].color = new Color(255.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, 1.0f);
            } else if (NewRank2 == idx) {
                newText[idx].gameObject.SetActive(true);
                for (int i = 1; i <= 8; i++) {
                    if (i == PlayerPrefs.GetInt("player2Character")) {
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
                rankPlayerText[idx].text = "2P";
                rankPlayerText[idx].color = new Color(128.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
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
            if (float.IsInfinity(Rank[idx]) || float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*/*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
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
