using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用

public class MenuAction : MonoBehaviour
{
    // UI
    /*
    public Text menu1SelectText;
    public Text menu2SelectText;
    public Text menu3SelectText;
    public Text menu4SelectText;
    public Text menu5SelectText;
    public Text menu6SelectText;
    public Text menu7SelectText;
    public Text menu8SelectText;
    */
    public Image[] menuSelect;
    public Image[] menuSelectBack;
    public Image[] menuImage;
    public Text[] menuSelectText;
    /*
    public Image menu1Select;
    public Image menu2Select;
    public Image menu3Select;
    public Image menu4Select;
    public Image menu5Select;
    public Image menu6Select;
    public Image menu7Select;
    public Image menu8Select;
    public Image menu1SelectBack;
    public Image menu2SelectBack;
    public Image menu3SelectBack;
    public Image menu4SelectBack;
    public Image menu5SelectBack;
    public Image menu6SelectBack;
    public Image menu7SelectBack;
    public Image menu8SelectBack;
    public Image menu1Image;
    public Image menu2Image;
    public Image menu3Image;
    public Image menu4Image;
    public Image menu5Image;
    public Image menu6Image;
    public Image menu7Image;
    public Image menu8Image;
    public Text menu1SelectText;
    public Text menu2SelectText;
    public Text menu3SelectText;
    public Text menu4SelectText;
    public Text menu5SelectText;
    public Text menu6SelectText;
    public Text menu7SelectText;
    public Text menu8SelectText;
    */
    // public UIButton menu1button;
    public int menuNo;

    public bool upFlg;
    public bool downFlg;
    public bool leftFlg;
    public bool rightFlg;
    public bool keyDown;

    // パラメータ
    float Elapsed; // 経過時間

    // SE
    AudioSource MyAudio;                    // オーディオソース
    public AudioClip cursor;               // 
    public AudioClip submit;               // 

    // Start is called before the first frame update
    void Start()
    {
        Elapsed = 0.0f; // 経過時間をリセット
        menuNo = 1;
        keyDown = false;
        for (int i = 1; i <= 8; i++) {
            menuSelect[i].gameObject.SetActive(false);
            menuSelectBack[i].gameObject.SetActive(false);
        }
        /*
        menu1Select.gameObject.SetActive(false);
        menu2Select.gameObject.SetActive(false);
        menu3Select.gameObject.SetActive(false);
        menu4Select.gameObject.SetActive(false);
        menu5Select.gameObject.SetActive(false);
        menu6Select.gameObject.SetActive(false);
        menu7Select.gameObject.SetActive(false);
        menu8Select.gameObject.SetActive(false);
        menu1SelectBack.gameObject.SetActive(false);
        menu2SelectBack.gameObject.SetActive(false);
        menu3SelectBack.gameObject.SetActive(false);
        menu4SelectBack.gameObject.SetActive(false);
        menu5SelectBack.gameObject.SetActive(false);
        menu6SelectBack.gameObject.SetActive(false);
        menu7SelectBack.gameObject.SetActive(false);
        menu8SelectBack.gameObject.SetActive(false);
        */
        MyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio.Play(); // BGMを再生

        // menu1button = GameObject.Find("menu1Select").GetComponent<UIButton>();
        // EventDelegate.Add(menu1button.onClick, onClickButton);
    }

    // Update is called once per frame
    void Update() {
        Vector3 rotateValue = new Vector3(0, -0.03f, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;

        Elapsed += Time.deltaTime; // 時間を加算
        Elapsed %= 1.0f; // 1秒ごとにリセット
        for (int i = 1; i <= 8; i++) {
            menuSelect[i].gameObject.SetActive(false);
            menuSelectBack[i].gameObject.SetActive(false);
        }
        /*
        menu1Select.gameObject.SetActive(false);
        menu2Select.gameObject.SetActive(false);
        menu3Select.gameObject.SetActive(false);
        menu4Select.gameObject.SetActive(false);
        menu5Select.gameObject.SetActive(false);
        menu6Select.gameObject.SetActive(false);
        menu7Select.gameObject.SetActive(false);
        menu8Select.gameObject.SetActive(false);
        menu1SelectBack.gameObject.SetActive(false);
        menu2SelectBack.gameObject.SetActive(false);
        menu3SelectBack.gameObject.SetActive(false);
        menu4SelectBack.gameObject.SetActive(false);
        menu5SelectBack.gameObject.SetActive(false);
        menu6SelectBack.gameObject.SetActive(false);
        menu7SelectBack.gameObject.SetActive(false);
        menu8SelectBack.gameObject.SetActive(false);
        */
        if (Input.GetKeyDown(KeyCode.RightArrow) && !keyDown)
        {
            rightFlg = true;
            keyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !keyDown)
        {
            leftFlg = true;
            keyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !keyDown)
        {
            upFlg = true;
            keyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !keyDown)
        {
            downFlg = true;
            keyDown = true;
        }
        if (rightFlg && Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightFlg = false;
            keyDown = false;
        }
        if (leftFlg && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftFlg = false;
            keyDown = false;
        }
        if (upFlg && Input.GetKeyUp(KeyCode.UpArrow))
        {
            upFlg = false;
            keyDown = false;
        }
        if (downFlg && Input.GetKeyUp(KeyCode.DownArrow))
        {
            downFlg = false;
            keyDown = false;
        }
        /*
        if (Input.GetAxis("Vertical") > 0.1f && !keyDown) {
            upFlg = true;
            keyDown = true;
        } else if (Input.GetAxis("Horizontal") > 0.1f && !keyDown) {
            rightFlg = true;
            keyDown = true;
        } else if (Input.GetAxis("Vertical") < -0.1f && !keyDown) {
            downFlg = true;
            keyDown = true;
        } else if (Input.GetAxis("Horizontal") < -0.1f && !keyDown) {
            leftFlg = true;
            keyDown = true;
        } else if (Input.GetAxis("Vertical") >= -0.1f && Input.GetAxis("Vertical") <= 0.1f && Input.GetAxis("Horizontal") >= -0.1f && Input.GetAxis("Horizontal") <= 0.1f && keyDown) {
            upFlg = false;
            downFlg = false;
            rightFlg = false;
            leftFlg = false;
            keyDown = false;
        }
        */
        if (upFlg && keyDown) {

            if (menuNo >= 5)
            {
                menuNo -= 4;
                MyAudio.PlayOneShot(cursor);
            }
            keyDown = false;
        } else if (downFlg && keyDown) {

            if (menuNo <= 4)
            {
                menuNo += 4;
                MyAudio.PlayOneShot(cursor);
            }
            keyDown = false;
        } else if (rightFlg && keyDown) {
            if (menuNo % 4 != 0) {
                menuNo++;
                MyAudio.PlayOneShot(cursor);
            }
            keyDown = false;
        } else if (leftFlg && keyDown) {
            if (menuNo % 4 != 1)
            {
                menuNo--;
                MyAudio.PlayOneShot(cursor);
            }
            keyDown = false;
        }
        if (menuNo > 8) {
            menuNo = 8;
        } else if (menuNo < 1) {
            menuNo = 1;
        }
        for (int i = 1; i <= 8; i++) {
            if (i == menuNo) {
                menuSelect[i].gameObject.SetActive(true);
                menuSelectBack[i].gameObject.SetActive(true);
                menuSelectText[i].gameObject.SetActive(true);
                menuImage[i].gameObject.SetActive(true);
                menuSelect[i].gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
                menuSelectBack[i].gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
                menuSelectText[i].gameObject.transform.localScale = new Vector3(1.1f, 1.65f, 1.0f);
                menuImage[i].gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
            } else {
                menuSelect[i].gameObject.SetActive(false);
                menuSelectBack[i].gameObject.SetActive(false);
                menuSelectText[i].gameObject.SetActive(false);
                menuImage[i].gameObject.SetActive(false);
                menuSelect[i].gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                menuSelectBack[i].gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                menuSelectText[i].gameObject.transform.localScale = new Vector3(1.0f, 1.5f, 1.0f);
                menuImage[i].gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        /*
        switch (menuNo) {
            case 1:
                menu1Select.gameObject.SetActive(true);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(true);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(true);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(true);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(false);
                break;
            case 2:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(true);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(true);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(true);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(true);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(false);
                break;
            case 3:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(true);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(true);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(true);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(true);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(false);
                break;
            case 4:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(true);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(true);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(true);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(true);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(false);
                break;
            case 5:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(true);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(true);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(true);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(true);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(false);
                break;
            case 6:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(true);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(true);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(true);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(true);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(false);
                break;
            case 7:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(true);
                menu8Select.gameObject.SetActive(false);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(true);
                menu8SelectBack.gameObject.SetActive(false);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(true);
                menu8SelectText.gameObject.SetActive(false);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(true);
                menu8Image.gameObject.SetActive(false);
                break;
            case 8:
                menu1Select.gameObject.SetActive(false);
                menu2Select.gameObject.SetActive(false);
                menu3Select.gameObject.SetActive(false);
                menu4Select.gameObject.SetActive(false);
                menu5Select.gameObject.SetActive(false);
                menu6Select.gameObject.SetActive(false);
                menu7Select.gameObject.SetActive(false);
                menu8Select.gameObject.SetActive(true);
                menu1SelectBack.gameObject.SetActive(false);
                menu2SelectBack.gameObject.SetActive(false);
                menu3SelectBack.gameObject.SetActive(false);
                menu4SelectBack.gameObject.SetActive(false);
                menu5SelectBack.gameObject.SetActive(false);
                menu6SelectBack.gameObject.SetActive(false);
                menu7SelectBack.gameObject.SetActive(false);
                menu8SelectBack.gameObject.SetActive(true);
                menu1SelectText.gameObject.SetActive(false);
                menu2SelectText.gameObject.SetActive(false);
                menu3SelectText.gameObject.SetActive(false);
                menu4SelectText.gameObject.SetActive(false);
                menu5SelectText.gameObject.SetActive(false);
                menu6SelectText.gameObject.SetActive(false);
                menu7SelectText.gameObject.SetActive(false);
                menu8SelectText.gameObject.SetActive(true);
                menu1Image.gameObject.SetActive(false);
                menu2Image.gameObject.SetActive(false);
                menu3Image.gameObject.SetActive(false);
                menu4Image.gameObject.SetActive(false);
                menu5Image.gameObject.SetActive(false);
                menu6Image.gameObject.SetActive(false);
                menu7Image.gameObject.SetActive(false);
                menu8Image.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        */
        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Return)) {
            MyAudio.PlayOneShot(submit);
            switch (menuNo) {
                case 1:
                    SceneManager.LoadScene("StageSelect1"); // キャラクター選択シーンへ
                    break;
                case 2:
                    SceneManager.LoadScene("CharacterSelect21"); // キャラクター選択シーンへ
                    break;
                case 3:
                    SceneManager.LoadScene("Ranking"); // メインシーンへ
                    break;
                case 4:
                    SceneManager.LoadScene("Character"); // Characterシーンへ
                    break;
                case 5:
                    SceneManager.LoadScene("Help"); // メインシーンへ
                    break;
                case 6:
                    SceneManager.LoadScene("Credit"); // Creditシーンへ
                    break;
                case 7:
                    if (Random.Range(0.0f, 2.0f) > 1.0f) {
                        SceneManager.LoadScene("Title2"); // タイトルシーンへ
                    } else {
                        SceneManager.LoadScene("Title"); // タイトルシーンへ
                    }
                    break;
                case 8:
                    Quit();
                    // SceneManager.LoadScene("Quit"); // 終了シーンへ
                    break;
                default:
                    break;
            }
        }

    }
    /*
    void Awake() {
        menu1button = GameObject.Find("menu1Select").GetComponent<UIButton>();
        EventDelegate.Add(menu1button.onClick, onClickButton);
    }
    */
    /*
    public void onClickButton() {
        Debug.Log("onClickButton()");
    }
    */
    public void OnClickMenu1() {
        Debug.Log("Menu1 click!");
        if (menuNo == 1) {
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("StageSelect1"); // キャラクター選択シーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 1;
        }
    }
    public void OnClickMenu2() {
        Debug.Log("Menu2 click!");
        if (menuNo == 2) {
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("CharacterSelect21"); // キャラクター選択シーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 2;
        }
    }
    public void OnClickMenu3() {
        Debug.Log("Menu3 click!");
        if (menuNo == 3) {
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("Ranking"); // メインシーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 3;
        }
    }
    public void OnClickMenu4() {
        Debug.Log("Menu4 click!");
        if (menuNo == 4) {
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("Character"); // Characterシーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 4;
        }
    }
    public void OnClickMenu5() {
        Debug.Log("Menu5 click!");
        if (menuNo == 5) {
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("Help"); // メインシーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 5;
        }
    }
    public void OnClickMenu6() {
        Debug.Log("Menu6 click!");
        if (menuNo == 6) {
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("Credit"); // Creditシーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 6;
        }
    }
    public void OnClickMenu7() {
        Debug.Log("Menu7 click!");
        if (menuNo == 7) {
            MyAudio.PlayOneShot(submit);
            if (Random.Range(0.0f, 2.0f) > 1.0f) {
                SceneManager.LoadScene("Title2"); // タイトルシーンへ
            } else {
                SceneManager.LoadScene("Title"); // タイトルシーンへ
            }
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 7;
        }
    }
    public void OnClickMenu8() {
        Debug.Log("Menu8 click!");
        if (menuNo == 8) {
            MyAudio.PlayOneShot(submit);
            Quit();
            // SceneManager.LoadScene("Quit"); // 終了シーンへ
        } else {
            MyAudio.PlayOneShot(cursor);
            menuNo = 8;
        }
    }
    void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#else
      Application.Quit();
#endif
    }
}
