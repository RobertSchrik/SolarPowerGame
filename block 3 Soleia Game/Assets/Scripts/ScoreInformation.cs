using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//-- All code within this script stays after scene reload. -- //

public class ScoreInformation : MonoBehaviour
{
    public static int CurrentScore; // player score
    public static int ScoreOne = 100; // score one
    public static int ScoreTwo = 100; // score two
    public static int ScoreThree = 0; // score three
    public static int ScoreFour; // score four

    float TimeRemaining;

    void Awake(){
        DontDestroyOnLoad(this); // avoids destrucion on load

        if (FindObjectsOfType(GetType()).Length > 1){ //check if dontdestroy object exists more than ones if true destroy new object
            Destroy(gameObject);
        }
    }

    public IEnumerator StartCountdown(float TotalTime = 10){
        TimeRemaining = TotalTime;
        while (TimeRemaining > 0){
            Debug.Log("Countdown: " + TimeRemaining);
            yield return new WaitForSeconds(1.0f);
            TimeRemaining--;
            Debug.Log(TimeRemaining);
        }

        if (TimeRemaining == 0){
            Debug.Log("Oh, Hi mark");
        }
    }

    void Start(){
        StartCoroutine(StartCountdown());
    }


}
