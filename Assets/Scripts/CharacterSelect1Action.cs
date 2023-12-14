using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用
using TMPro;

public class CharacterSelect1Action : MonoBehaviour
{
    // UI
    public Text characterNameText;
    public Text characterDetailText;
    public Text txtMessage;
    public Text specAccText;
    public Text specSpeedText;
    public Text specRotText;
    public TextMeshProUGUI characterName;

    public GameObject Chara1;
    public GameObject Chara2;
    public GameObject Chara3;
    public GameObject Chara4;
    public GameObject Chara5;
    public GameObject Chara6;
    public GameObject Chara7;
    public GameObject Chara8;
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
    void Start()
    {
        // DontDestroyOnLoad(this);
        Elapsed = 0.0f; // 経過時間をリセット
        menuNo = 1;
        Chara1.SetActive(false);
        Chara2.SetActive(false);
        Chara3.SetActive(false);
        Chara4.SetActive(false);
        Chara5.SetActive(false);
        Chara6.SetActive(false);
        Chara7.SetActive(false);
        Chara8.SetActive(false);
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
        if (menuNo > 8) {
            menuNo = 1;
        }
        if (menuNo < 1) {
            menuNo = 8;
        }
            switch (menuNo) {
            case 1:
                if (changedFlg) {
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
                }
                Chara1.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 2:
                if (changedFlg) {
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
                }
                Chara2.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 3:
                if (changedFlg) {
                    Chara1.SetActive(false);
                    Chara2.SetActive(false);
                    Chara3.SetActive(true);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara3.transform.position = new Vector3(0, -0.1f, 0);
                    Chara3.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "じゅうりっとるさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "しかくいけーすを じぶんのからだで いっぱいにすることができる。\nたまに しょくぱんになったゆめ をみるらしい。";
                    specAccText.text = "かそく ★　　";
                    specSpeedText.text = "ぱわー ★★★";
                    specRotText.text = "かーぶ ★★　";
                    changedFlg = false;
                }
                Chara3.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 4:
                if (changedFlg) {
                    Chara1.SetActive(false);
                    Chara2.SetActive(false);
                    Chara3.SetActive(false);
                    Chara4.SetActive(true);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara4.transform.position = new Vector3(0, -0.1f, 0);
                    Chara4.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "だっくすきゃっとさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "かすてらのはこが おきにいりのながーいこねこ。\nじつはふたごなのでは？ との もっぱらのうわさ。";
                    specAccText.text = "かそく ★★★";
                    specSpeedText.text = "ぱわー ★★　";
                    specRotText.text = "かーぶ ★　　";
                    changedFlg = false;
                }
                Chara4.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 5:
                if (changedFlg) {
                    Chara1.SetActive(false);
                    Chara2.SetActive(false);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(true);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara5.transform.position = new Vector3(0, -0.1f, 0);
                    Chara5.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "みっちりだんぼーるさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "いろんな だんぼーるばこの はいりごこちを ためしつづけている。\nすこしちいさいくらいが ちょうどいいらしい。";
                    specAccText.text = "かそく ★★　";
                    specSpeedText.text = "ぱわー ★★　";
                    specRotText.text = "かーぶ ★★　";
                    changedFlg = false;
                }
                Chara5.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 6:
                if (changedFlg) {
                    Chara1.SetActive(false);
                    Chara2.SetActive(false);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(true);
                    Chara7.SetActive(false);
                    Chara8.SetActive(false);
                    Chara6.transform.position = new Vector3(0, -0.1f, 0);
                    Chara6.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "すたーくりーなーさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "ろぼっとそうじきれーすの ちょうないきろくを めざしている。\nばってりーがたりなくなると いえにつれもどされるのがなやみのたね。";
                    specAccText.text = "かそく ★　　";
                    specSpeedText.text = "ぱわー ★★　";
                    specRotText.text = "かーぶ ★★★";
                    changedFlg = false;
                }
                Chara6.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 7:
                if (changedFlg) {
                    Chara1.SetActive(false);
                    Chara2.SetActive(false);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(true);
                    Chara8.SetActive(false);
                    Chara7.transform.position = new Vector3(0, -0.1f, 0);
                    Chara7.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "まんねんこたつさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "ふゆでも なつでも こたつでまるくなっている。\nくろいのは こたつのせいでは ないらしい。";
                    specAccText.text = "かそく ★★　";
                    specSpeedText.text = "ぱわー ★★★";
                    specRotText.text = "かーぶ ★　　";
                    changedFlg = false;
                }
                Chara7.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            case 8:
                if (changedFlg) {
                    Chara1.SetActive(false);
                    Chara2.SetActive(false);
                    Chara3.SetActive(false);
                    Chara4.SetActive(false);
                    Chara5.SetActive(false);
                    Chara6.SetActive(false);
                    Chara7.SetActive(false);
                    Chara8.SetActive(true);
                    Chara8.transform.position = new Vector3(0, -0.1f, 0);
                    Chara8.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    characterNameText.text = "ねこねこぱにっくさん";
                    characterName.text = characterNameText.text;
                    characterDetailText.text = "あなから まえあしやしっぽをだして こねこのあいてをしてくれる。\nこのしせいは けっこうつかれるらしい。";
                    specAccText.text = "かそく ★　　";
                    specSpeedText.text = "ぱわー ★★★";
                    specRotText.text = "かーぶ ★★　";
                    changedFlg = false;
                }
                Chara8.transform.Rotate(new Vector3(0, 0.3f, 0));
                break;
            default:
                break;
        }
        txtMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
        if (/*Input.GetButtonDown("Fire2") || */Input.GetKeyDown(KeyCode.Return)) {
            MyAudio.PlayOneShot(submit);
            PlayerPrefs.SetInt("player1Character", menuNo);
            if (PlayerPrefs.GetInt("stageSelect") == 2) {
                SceneManager.LoadScene("MainB"); // メニューへ
            } else if (PlayerPrefs.GetInt("stageSelect") == 3) {
                SceneManager.LoadScene("MainC"); // メニューへ
            } else {
                SceneManager.LoadScene("Main"); // メニューへ
            }
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

        if (Input.GetButtonDown("Cancel"/*"Jump"*/)) { // 
            MyAudio.PlayOneShot(submit);
            PauseInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            SceneManager.LoadScene("StageSelect1"); // メニューへ
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
            menuNo = 8;
        }
    }
    public void OnClickRight() {
        menuNo++;
        downFlg = false;
        MyAudio.PlayOneShot(cursor);
        changedFlg = true;
        leftNavi.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        leftNavi.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
        if (menuNo > 8) {
            menuNo = 1;
        }
    }
    public void OnClickReturn() {
        MyAudio.PlayOneShot(submit);
        PlayerPrefs.SetInt("player1Character", menuNo);
        if (PlayerPrefs.GetInt("stageSelect") == 2) {
            SceneManager.LoadScene("MainB"); // メニューへ
        } else if (PlayerPrefs.GetInt("stageSelect") == 3) {
            SceneManager.LoadScene("MainC"); // メニューへ
        } else {
            SceneManager.LoadScene("Main"); // メニューへ
        }
    }
    public void OnClickEsc() {
        MyAudio.PlayOneShot(submit);
        PauseInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SceneManager.LoadScene("StageSelect1"); // メニューへ
    }
}
