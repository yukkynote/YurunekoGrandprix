using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用

public class Splash2Action : MonoBehaviour
{
    // パラメータ
    float Elapsed; // 経過時間

    // Start is called before the first frame update
    void Start() {
        Elapsed = 0.0f; // 経過時間をリセット
    }

    // Update is called once per frame
    void Update() {
        Elapsed += Time.deltaTime; // 時間を加算
        if (Elapsed >= 3.0f || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Return)) { // Aボタンもしくはマウス左クリックで

            if (Random.Range(0.0f, 2.0f) > 1.0f) {
                SceneManager.LoadScene("Title2"); // タイトルシーンへ
            } else {
                SceneManager.LoadScene("Title"); // タイトルシーンへ
            }
        }

    }
}
