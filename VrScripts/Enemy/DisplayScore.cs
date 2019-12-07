using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {
    public int SavedScore;
    public Text FinalScore;
    public Text FinalTime;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonEnd.IsFinished == true)
        {
                SavedScore = ScoreSystem.TotalScore;
                FinalScore.text = SavedScore.ToString();
                FinalTime.text = string.Format("{0:00} : {1:00}", ScoreSystem.minuites, ScoreSystem.seconds);
        }
    }

}
