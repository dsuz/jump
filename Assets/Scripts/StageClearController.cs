using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームをクリアしたかどうか判定するクラス
/// </summary>
public class StageClearController : MonoBehaviour {
    private bool _isClear;
    /// <summary>ゲームをクリアした後、次の処理を行う前に待つ時間（秒）</summary>
    [SerializeField] private float _stageClearWaitTime = 3.0f;
    /// <summary>待つ時間を計上するためのタイマー</summary>
    private float _timer;

    void Update()
    {
        if (_isClear)
        {
            _timer += Time.deltaTime;
            if (_timer > _stageClearWaitTime)
            {
                Debug.Log("Clear.");

                // Game Jam Menu Template が作ったオブジェクトを破棄する
                GameObject uiObject = GameObject.Find("UI");
                if (uiObject != null)
                    Destroy(uiObject);

                // Game Jam Menu Template のタイトルシーンに戻る
                Application.LoadLevel("title");
            }
        }
    }

    /// <summary>
    /// ゲームクリア時に呼び出す
    /// </summary>
    public void Clear()
    {
        _isClear = true;
    }
}
