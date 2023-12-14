using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用

public class RankingAction : MonoBehaviour
{
    // UI
    public Text txtMessage; // メッセージ画像とテキスト
    public Text menuTitleText;
    public Text[] scoreText;
    public Text[] rankText;
    // パラメータ
    float Elapsed; // 経過時間

    float[] Rank = new float[6]; //内部的に保持するランキング 
    int[] Rankin = new int[6]; //内部的に保持するランキング 
    int[] RankChara = new int[6]; //内部的に保持するランキング 
    int idx;

    public Image leftNavi;
    public Image rightNavi;

    public Image[] rankChara1;
    public Image[] rankChara2;
    public Image[] rankChara3;
    public Image[] rankChara4;
    public Image[] rankChara5;

    public int menuNo;

    bool upFlg;
    bool downFlg;
    bool keyDown;
    bool changedFlg;

    // SE
    AudioSource MyAudio;                    // オーディオソース
    public AudioClip cursor;               // 
    public AudioClip submit;               // 

    // Start is called before the first frame update
    void Start()
    {
        Elapsed = 0.0f; // 経過時間をリセット
        menuNo = 1;
        changedFlg = true;

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
        for (idx = 1; idx <= 5; idx++) {
            if (float.IsInfinity(Rank[idx]) || float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
                scoreText[idx].text = "--'--\"---";
            } else {
                int minutes = Mathf.FloorToInt(Rank[idx] / 60F);
                int seconds = Mathf.FloorToInt(Rank[idx] - minutes * 60);
                int mseconds = Mathf.FloorToInt((Rank[idx] - minutes * 60 - seconds) * 1000);
                scoreText[idx].text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                // scoreText[idx].text = Rank[idx].ToString("f2") + "s";
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
            for (int i = 1; i <= 8; i++) {
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
            if (RankChara[idx] != 0) {
                switch (idx) {
                    case 1:
                        rankChara1[RankChara[idx]].enabled = true;
                        break;
                    case 2:
                        rankChara2[RankChara[idx]].enabled = true;
                        break;
                    case 3:
                        rankChara3[RankChara[idx]].enabled = true;
                        break;
                    case 4:
                        rankChara4[RankChara[idx]].enabled = true;
                        break;
                    case 5:
                        rankChara5[RankChara[idx]].enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }
        MyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio.Play(); // BGMを再生
    }

    // Update is called once per frame
    void Update() {
        Vector3 rotateValue = new Vector3(0, -0.03f, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;

        // メッセージ点滅表示
        Elapsed += Time.deltaTime; // 時間を加算
        Elapsed %= 1.0f; // 1秒ごとにリセット



        if (Input.GetAxis("Horizontal") > 0.1f && !keyDown) {
            upFlg = true;
            keyDown = true;
            rightNavi.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            leftNavi.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rightNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            leftNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        }
        if (Input.GetAxis("Horizontal") < -0.1f && !keyDown) {
            downFlg = true;
            keyDown = true;
            rightNavi.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            leftNavi.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rightNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            leftNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        }
        if (Input.GetAxis("Horizontal") >= -0.1f && Input.GetAxis("Horizontal") <= 0.1f) {
            upFlg = false;
            downFlg = false;
            keyDown = false;
            rightNavi.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            leftNavi.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rightNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            leftNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        }
        if (upFlg && keyDown) {
            menuNo--;
            upFlg = false;
            MyAudio.PlayOneShot(cursor);
            changedFlg = true;
            rightNavi.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            rightNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
        }
        if (downFlg && keyDown) {
            menuNo++;
            downFlg = false;
            MyAudio.PlayOneShot(cursor);
            changedFlg = true;
            leftNavi.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            leftNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
        }
        if (menuNo > 3) {
            menuNo = 1;
        }
        if (menuNo < 1) {
            menuNo = 3;
        }
        switch (menuNo) {
            case 1:
                if (changedFlg) {
                    menuTitleText.text = "だんちいっしゅうコース ランキング";
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
                    for (idx = 1; idx <= 5; idx++) {
                        if (float.IsInfinity(Rank[idx]) || float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
                            scoreText[idx].text = "--'--\"---";
                        } else {
                            int minutes = Mathf.FloorToInt(Rank[idx] / 60F);
                            int seconds = Mathf.FloorToInt(Rank[idx] - minutes * 60);
                            int mseconds = Mathf.FloorToInt((Rank[idx] - minutes * 60 - seconds) * 1000);
                            scoreText[idx].text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                            // scoreText[idx].text = Rank[idx].ToString("f2") + "s";
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
                        for (int i = 1; i <= 8; i++) {
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
                        if (RankChara[idx] != 0) {
                            switch (idx) {
                                case 1:
                                    rankChara1[RankChara[idx]].enabled = true;
                                    break;
                                case 2:
                                    rankChara2[RankChara[idx]].enabled = true;
                                    break;
                                case 3:
                                    rankChara3[RankChara[idx]].enabled = true;
                                    break;
                                case 4:
                                    rankChara4[RankChara[idx]].enabled = true;
                                    break;
                                case 5:
                                    rankChara5[RankChara[idx]].enabled = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    changedFlg = false;
                }
                // Chara1.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 2:
                if (changedFlg) {
                    menuTitleText.text = "こうえん８のじコース ランキング";
                    if (PlayerPrefs.HasKey("R_B1")) {
                        Debug.Log("DataLoad"); //スコアデータをロード
                        for (idx = 1; idx <= 5; idx++) {
                            Rank[idx] = PlayerPrefs.GetFloat("R_B" + idx);
                            Rankin[idx] = PlayerPrefs.GetInt("R_BI" + idx);
                            RankChara[idx] = PlayerPrefs.GetInt("R_BC" + idx);
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
                        }
                    }
                    for (idx = 1; idx <= 5; idx++) {
                        if (float.IsInfinity(Rank[idx]) || float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
                            scoreText[idx].text = "--'--\"---";
                        } else {
                            int minutes = Mathf.FloorToInt(Rank[idx] / 60F);
                            int seconds = Mathf.FloorToInt(Rank[idx] - minutes * 60);
                            int mseconds = Mathf.FloorToInt((Rank[idx] - minutes * 60 - seconds) * 1000);
                            scoreText[idx].text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                            // scoreText[idx].text = Rank[idx].ToString("f2") + "s";
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
                        for (int i = 1; i <= 8; i++) {
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
                        if (RankChara[idx] != 0) {
                            switch (idx) {
                                case 1:
                                    rankChara1[RankChara[idx]].enabled = true;
                                    break;
                                case 2:
                                    rankChara2[RankChara[idx]].enabled = true;
                                    break;
                                case 3:
                                    rankChara3[RankChara[idx]].enabled = true;
                                    break;
                                case 4:
                                    rankChara4[RankChara[idx]].enabled = true;
                                    break;
                                case 5:
                                    rankChara5[RankChara[idx]].enabled = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    changedFlg = false;
                }
                // Chara2.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 3:
                if (changedFlg) {
                    menuTitleText.text = "ぐるぐるかいだんコース ランキング";
                    if (PlayerPrefs.HasKey("R_C1")) {
                        Debug.Log("DataLoad"); //スコアデータをロード
                        for (idx = 1; idx <= 5; idx++) {
                            Rank[idx] = PlayerPrefs.GetFloat("R_C" + idx);
                            Rankin[idx] = PlayerPrefs.GetInt("R_CI" + idx);
                            RankChara[idx] = PlayerPrefs.GetInt("R_CC" + idx);
                        }
                    } else {
                        Debug.Log("DataInit"); //初回起動なのでスコアデータ作成
                        for (idx = 1; idx <= 5; idx++) {
                            PlayerPrefs.SetFloat("R_C" + idx, (99.0f * 60.0f) + 59.999f); //全部ゼロ
                            PlayerPrefs.SetInt("R_CI" + idx, 0); //全部ゼロ
                            PlayerPrefs.SetInt("R_CC" + idx, 0); //全部ゼロ
                            Rank[idx] = PlayerPrefs.GetFloat("R_C" + idx);
                            Rankin[idx] = PlayerPrefs.GetInt("R_CI" + idx);
                            RankChara[idx] = PlayerPrefs.GetInt("R_CC" + idx);
                        }
                    }
                    for (idx = 1; idx <= 5; idx++) {
                        if (float.IsInfinity(Rank[idx]) || float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
                            scoreText[idx].text = "--'--\"---";
                        } else {
                            int minutes = Mathf.FloorToInt(Rank[idx] / 60F);
                            int seconds = Mathf.FloorToInt(Rank[idx] - minutes * 60);
                            int mseconds = Mathf.FloorToInt((Rank[idx] - minutes * 60 - seconds) * 1000);
                            scoreText[idx].text = string.Format("{0:00}'{1:00}\"{2:000}", minutes, seconds, mseconds);
                            // scoreText[idx].text = Rank[idx].ToString("f2") + "s";
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
                        for (int i = 1; i <= 8; i++) {
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
                        if (RankChara[idx] != 0) {
                            switch (idx) {
                                case 1:
                                    rankChara1[RankChara[idx]].enabled = true;
                                    break;
                                case 2:
                                    rankChara2[RankChara[idx]].enabled = true;
                                    break;
                                case 3:
                                    rankChara3[RankChara[idx]].enabled = true;
                                    break;
                                case 4:
                                    rankChara4[RankChara[idx]].enabled = true;
                                    break;
                                case 5:
                                    rankChara5[RankChara[idx]].enabled = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    changedFlg = false;
                }
                // Chara2.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            default:
                break;
        }

        txtMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
        if (/*Input.GetButtonDown("Fire1") || */Input.GetKeyDown(KeyCode.Return)) { // Aボタンもしくはマウス左クリックで
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("Menu"); // タイトルシーンに戻る
        }
        if (Input.GetButtonDown("Fire3")) {
            Debug.Log("DataInit"); //初回起動なのでスコアデータ作成
            switch (menuNo) {
                case 1:
                    for (idx = 1; idx <= 5; idx++) {
                        PlayerPrefs.SetFloat("R" + idx, (99.0f * 60.0f) + 59.999f); //全部ゼロ
                        PlayerPrefs.SetInt("RI" + idx, 0); //全部ゼロ
                        PlayerPrefs.SetInt("RC" + idx, 0); //全部ゼロ
                        Rank[idx] = PlayerPrefs.GetFloat("R" + idx);
                        Rankin[idx] = PlayerPrefs.GetInt("RI" + idx);
                        RankChara[idx] = PlayerPrefs.GetInt("RC" + idx);
                        if (float.IsInfinity(Rank[idx]) ||
                     float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
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
                        for (int i = 1; i <= 8; i++) {
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
                    break;
                case 2:
                    for (idx = 1; idx <= 5; idx++) {
                        PlayerPrefs.SetFloat("R_B" + idx, (99.0f * 60.0f) + 59.999f); //全部ゼロ
                        PlayerPrefs.SetInt("R_BI" + idx, 0); //全部ゼロ
                        PlayerPrefs.SetInt("R_BC" + idx, 0); //全部ゼロ
                        Rank[idx] = PlayerPrefs.GetFloat("R_B" + idx);
                        Rankin[idx] = PlayerPrefs.GetInt("R_BI" + idx);
                        RankChara[idx] = PlayerPrefs.GetInt("R_BC" + idx);
                        if (float.IsInfinity(Rank[idx]) ||
                     float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
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
                        for (int i = 1; i <= 8; i++) {
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
                    break;
                case 3:
                    for (idx = 1; idx <= 5; idx++) {
                        PlayerPrefs.SetFloat("R_C" + idx, (99.0f * 60.0f) + 59.999f); //全部ゼロ
                        PlayerPrefs.SetInt("R_CI" + idx, 0); //全部ゼロ
                        PlayerPrefs.SetInt("R_CC" + idx, 0); //全部ゼロ
                        Rank[idx] = PlayerPrefs.GetFloat("R_C" + idx);
                        Rankin[idx] = PlayerPrefs.GetInt("R_CI" + idx);
                        RankChara[idx] = PlayerPrefs.GetInt("R_CC" + idx);
                        if (float.IsInfinity(Rank[idx]) ||
                     float.IsNaN(Rank[idx]) || (Rank[idx] > 999.0f) /*Rank[idx] == (99.0f * 60.0f) + 59.999f*/) {
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
                        for (int i = 1; i <= 8; i++) {
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
                    break;
                default:
                    break;
            }
        }
    }
    public void OnClickLeft() {
        menuNo--;
        upFlg = false;
        MyAudio.PlayOneShot(cursor);
        changedFlg = true;
        rightNavi.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        rightNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
        if (menuNo < 1) {
            menuNo = 3;
        }
    }
    public void OnClickRight() {
        menuNo++;
        downFlg = false;
        MyAudio.PlayOneShot(cursor);
        changedFlg = true;
        leftNavi.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        leftNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
        if (menuNo > 3) {
            menuNo = 1;
        }
    }
    public void OnClickReturn() {
        MyAudio.PlayOneShot(submit);
        SceneManager.LoadScene("Menu"); // タイトルシーンに戻る
    }
}
