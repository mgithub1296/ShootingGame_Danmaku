using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // スコアを表示するGUIText
    public GameObject scoreGUIText;

    // ハイスコアを表示するGUIText
    public GameObject highScoreGUIText;

    // スコア
    private float score;

    // ハイスコア
    private float highScore;

    // PlayerPrefsで保存するためのキー
    private string highScoreKey = "highScore";

    int count;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        // スコアがハイスコアより大きければ
        if (highScore < score)
        {
            highScore = score;
        }

        // スコア・ハイスコアを表示する
        scoreGUIText.GetComponent<Text>().text = score.ToString();
        highScoreGUIText.GetComponent<Text>().text = "HighScore : " + highScore.ToString();
    }

    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;

        // ハイスコアを取得する。保存されてなければ0を取得する。
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    // ポイントの追加
    public void AddPoint(float point)
    {
        //初回実行時に実行されちゃうから？
        if (count > 0)
        {
            score = score + point;
        }        
        count = 1;
    }

    // ハイスコアの保存
    public void Save()
    {
        // ハイスコアを保存する
        PlayerPrefs.SetFloat(highScoreKey, highScore);
        PlayerPrefs.Save();

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}
