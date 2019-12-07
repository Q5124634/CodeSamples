using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnd : MonoBehaviour
{

    public static bool IsFinished = false;
    public float speed = 0.005F;
    public GameObject Door;
    public GameObject DoorEnd;
    public GameObject DoorStart;
    public static Text FinalScore;
    public static Text SavedTime;
    private float startTime;
    private float journeyLength;
    public bool DoorIsOpen;
    public bool StartTimer = false;
    public static int SavedScore;


    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(Door.transform.position, DoorEnd.transform.position);
    }

    void Update()
    {

        if (DoorIsOpen)
        {
            float distCovered = (Time.deltaTime) * speed;
            float fracJourney = distCovered / journeyLength;
            Door.transform.position = Vector3.Lerp(Door.transform.position, DoorEnd.transform.position, fracJourney);
            StartTimer = true;
            StartCoroutine(WaitOneSecond());
        }

        if (gameObject.CompareTag("CanClose"))
        {
            if (!DoorIsOpen)
            {
                float distCovered = (Time.deltaTime) * speed;
                float fracJourney = distCovered / journeyLength;
                Door.transform.position = Vector3.Lerp(Door.transform.position, DoorStart.transform.position, fracJourney);
            }
        }
    }
        void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.transform.CompareTag("Controller"))
        {
            DoorIsOpen = true;
            GameObject.Find("Start / ResetButton").GetComponent<DoorController>().StartTimer = false;
            IsFinished = true;
            GetScore();
        }
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(3);
        DoorIsOpen = false;
    }

    public static void GetScore()
    {
        SavedScore = ScoreSystem.TotalScore;
        FinalScore.text = SavedScore.ToString();
    }

    public static void GetTime()
    {
        SavedTime.text = string.Format("{0:00} : {1:00}", ScoreSystem.minuites, ScoreSystem.seconds);
    }
}
