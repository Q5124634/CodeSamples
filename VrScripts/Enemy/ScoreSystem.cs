using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    public static int TotalScore = 0;  // Use this for initialization
    public Text TotalScoreText;
    public bool StartTimer = false;
    public Text TimerText;
    public static float StartedTimer = 0;
    private static float NewTime;
    public string SavedTime;
    public string SavedScore;
    public static float SavedScoreValue;
    public static float minuites = NewTime / 60;
    public static float seconds = NewTime % 60;
    public static float millSecs = (NewTime * 100) % 100;

    // Update is called once per frame
    void Update ()
    {
        if (ButtonEnd.IsFinished == false)
        {
            TotalScoreText.text = TotalScore.ToString();

            if (StartedTimer >= 0)
                TimerText.text = string.Format("{0:00} : {1:00}", minuites, seconds);
        }
    }

    public static void AddScore(int Score)
    {
        if (ButtonEnd.IsFinished == false)
        {
            TotalScore += Score;
        }
    }

    public static void  StartTiming()
    {
        if (StartedTimer >= 0)
        {
            NewTime += Time.deltaTime;
            minuites = NewTime / 60;
            seconds = NewTime % 60;
            millSecs = (NewTime * 100) % 100;
        }
    }

    public static void ResetTimer()
    {
        NewTime = 0;
        StartedTimer = 0;
        TotalScore = 0;
        minuites = 0;
        seconds = 0;
        millSecs = 0;
    }
}