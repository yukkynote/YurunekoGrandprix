using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用
using TMPro;

public class StageSelect1Action : MonoBehaviour
{
    GameObject Target;
    // UI
    public Text characterNameText;
    public Text characterDetailText;
    public Text txtMessage;
    public Text specAccText;
    public Text specSpeedText;
    public Text specRotText;
    public TextMeshProUGUI characterName;
    /*
    public GameObject Chara1;
    public GameObject Chara2;
    public GameObject Chara3;
    public GameObject Chara4;
    public GameObject Chara5;
    public GameObject Chara6;
    public GameObject Chara7;
    public GameObject Chara8;
    */
    public Image leftNavi;
    public Image rightNavi;

    public GameObject PauseInfo;

    public int menuNo;

    bool upFlg;
    bool downFlg;
    bool keyDown;
    bool changedFlg;

    // パラメータ
    float Elapsed; // 経過時間

    // SE
    AudioSource MyAudio;                    // オーディオソース
    public AudioClip cursor;               // 
    public AudioClip submit;               // 

    // Start is called before the first frame update
    void Start() {
        Target = GameObject.FindGameObjectWithTag("Player");
        // DontDestroyOnLoad(this);
        Elapsed = 0.0f; // 経過時間をリセット
        menuNo = 1;
        /*
        Chara1.SetActive(false);
        Chara2.SetActive(false);
        Chara3.SetActive(false);
        Chara4.SetActive(false);
        Chara5.SetActive(false);
        Chara6.SetActive(false);
        Chara7.SetActive(false);
        Chara8.SetActive(false);
        */
        changedFlg = true;
        /*
        menu1SelectText.gameObject.SetActive(false);
        menu2SelectText.gameObject.SetActive(false);
        menu3SelectText.gameObject.SetActive(false);
        menu4SelectText.gameObject.SetActive(false);
        menu5SelectText.gameObject.SetActive(false);
        */


        MyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio.Play(); // BGMを再生

    }

    // Update is called once per frame
    void Update() {
        /*
        Vector3 rotateValue = new Vector3(0, -0.03f, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
        */

        Elapsed += Time.deltaTime; // 時間を加算
        Elapsed %= 1.0f; // 1秒ごとにリセット
        /*
        menu1SelectText.gameObject.SetActive(false);
        menu2SelectText.gameObject.SetActive(false);
        menu3SelectText.gameObject.SetActive(false);
        menu4SelectText.gameObject.SetActive(false);
        menu5SelectText.gameObject.SetActive(false);
        */
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
                    /*
                    Chara1.SetActive(true);
                    Chara2.SetActive(false);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara1.transform.position = new Vector3(0, -0.1f, 0);
                    Chara1.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

                    characterNameText.text = "てぃっしゅろけっとさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "しんぴんでも ようしゃなく なかみをぜんぶだしてしまうので\nあきばこが じょうびされているらしい。";
                    specAccText.text = "かそく ★★★";
                    specSpeedText.text = "ぱわー ★　　";
                    specRotText.text = "かーぶ ★★　";
                    changedFlg = false;
                    */
                    characterNameText.text = "だんちいっしゅうコース";
                    characterDetailText.text = "ちょくせんたっぷりの ハイスピードコース。\nスピードをあげすぎて まがりかどにぶつかるねこが たえないらしい。";
                }
                // Chara1.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 2:
                if (changedFlg) {
                    /*
                    Chara1.SetActive(false);
                    Chara2.SetActive(true);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara2.transform.position = new Vector3(0, -0.1f, 0);
                    Chara2.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "ひとりねこなべさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "ひんやりした しょっかんが きもちいいらしい。\nにひきまではなんとかいける。さんびきはむりだった。";
                    specAccText.text = "かそく ★★　";
                    specSpeedText.text = "ぱわー ★　　";
                    specRotText.text = "かーぶ ★★★";
                    changedFlg = false;
                    */
                    characterNameText.text = "こうえん８のじコース";
                    characterDetailText.text = "とんねるたんけんや、プチとざんがたのしめる カーブのおおいコース。\nすなばに はいってはいけないという なぞのるーるがあるらしい。";
                }
                // Chara2.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 3:
                if (changedFlg) {
                    /*
                    Chara1.SetActive(false);
                    Chara2.SetActive(true);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara2.transform.position = new Vector3(0, -0.1f, 0);
                    Chara2.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "ひとりねこなべさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "ひんやりした しょっかんが きもちいいらしい。\nにひきまではなんとかいける。さんびきはむりだった。";
                    specAccText.text = "かそく ★★　";
                    specSpeedText.text = "ぱわー ★　　";
                    specRotText.text = "かーぶ ★★★";
                    changedFlg = false;
                    */
                    characterNameText.text = "ぐるぐるかいだんコース";
                    characterDetailText.text = "いちばんうえの ねぐらから しんせつなおたくに　おじゃまするコース。\nいっちゃくになると えさがちょっぴり ごうからしい。";
                }
                // Chara2.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            default:
                break;
        }
        txtMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
        if (/*Input.GetButtonDown("Fire2") || */Input.GetKeyDown(KeyCode.Return)) {
            MyAudio.PlayOneShot(submit);
            PlayerPrefs.SetInt("stageSelect", menuNo);
            SceneManager.LoadScene("CharacterSelect1"); // メニューへ
            // SceneManager.LoadScene("Main"); // メインシーンへ
            /*
            switch (menuNo) {
                case 1:
                    SceneManager.LoadScene("Main"); // メインシーンへ
                    break;
                case 2:
                    SceneManager.LoadScene("Ranking"); // メインシーンへ
                    break;
                case 3:
                    SceneManager.LoadScene("Help"); // メインシーンへ
                    break;
                case 4:
                    SceneManager.LoadScene("Credit"); // Creditシーンへ
                    break;
                case 5:
                    SceneManager.LoadScene("Title"); // タイトルシーンへ
                    break;
                default:
                    break;
            }
            */
        }
        if (menuNo == 2) {
            transform.position = new Vector3(-14.0f, 10.7f, -36.0f);
            transform.rotation = Quaternion.Euler(23.0f, 270.0f, 0.0f);
        } else if (menuNo == 3) {
            transform.position = new Vector3(-12.06f, 12.98f, -21.94f);
            transform.rotation = Quaternion.Euler(10.541f, -23.039f, -1.698f);
        } else {
            transform.position = new Vector3(0.0f, 1.0f, 63.0f);
            transform.rotation = Quaternion.Euler(-2.5f, 180.0f, 0.0f);
        }
        
        Elapsed += Time.deltaTime; // 時間を加算
        Elapsed %= 1.0f; // 1秒ごとにリセット

        if (Input.GetButtonDown("Cancel"/*"Jump"*/)) { // 
            MyAudio.PlayOneShot(submit);
            PauseInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            SceneManager.LoadScene("Menu"); // メニューへ
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
        PlayerPrefs.SetInt("stageSelect", menuNo);
        SceneManager.LoadScene("CharacterSelect1"); // 
    }
    public void OnClickEsc() {
        MyAudio.PlayOneShot(submit);
        PauseInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SceneManager.LoadScene("Menu"); // メニューへ
    }
}
