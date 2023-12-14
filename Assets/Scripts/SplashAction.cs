using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI制御用
using UnityEngine.SceneManagement; // シーン制御用

public class SplashAction : MonoBehaviour
{
    // パラメータ
    float Elapsed; // 経過時間

    // Start is called before the first frame update
    void Start() {
        Elapsed = 0.0f; // 経過時間をリセット
    }

    // Update is called once per frame
    void Update() {
        if (Random.Range(0.0f, 2.0f) > 1.0f) {
            SceneManager.LoadScene("Title2"); // タイトルシーンへ
        } else {
            SceneManager.LoadScene("Title"); // タイトルシーンへ
        }

    }
}
