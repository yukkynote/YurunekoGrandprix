using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用

public class HelpAction : MonoBehaviour
{
    // パラメータ
    float Elapsed; // 経過時間
    public Text txtMessage;

    // SE
    AudioSource MyAudio;                    // オーディオソース
    public AudioClip submit;               // 

    // Start is called before the first frame update
    void Start()
    {
        Elapsed = 0.0f; // 経過時間をリセット
        MyAudio = GetComponent<AudioSource>(); // オーディオソースを取得
        MyAudio.Play(); // BGMを再生

    }

    // Update is called once per frame
    void Update() {
        Vector3 rotateValue = new Vector3(0, -0.03f, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;

        Elapsed += Time.deltaTime; // 時間を加算
        Elapsed %= 1.0f; // 1秒ごとにリセット

        txtMessage.gameObject.SetActive(Elapsed < 0.7f); // メッセージを点滅表示させる
        if (/*Input.GetButtonDown("Fire1") || */Input.GetKeyDown(KeyCode.Return)) { // Aボタンもしくはマウス左クリックで
            MyAudio.PlayOneShot(submit);
            SceneManager.LoadScene("Menu"); // メインシーンへ
        }

    }
    public void OnClickReturn() {
        MyAudio.PlayOneShot(submit);
        SceneManager.LoadScene("Menu"); // タイトルシーンに戻る
    }
}
