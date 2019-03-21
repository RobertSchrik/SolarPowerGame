using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//-- All code within this script stays after scene reload. -- //

public class ScoreInformation : MonoBehaviour{
    //SavedScores
    public static int CurrentScore;
    public static int MoneyBalance =  55;
    public static int SocialBalance = 55;
    public static int EnergyBalance = 55;

    private GameManager gameManager;

//    float TimeRemaining;

    public static void resetscorelists(){
        CurrentScore = 0;
        MoneyBalance = 55;
        SocialBalance = 55;
        EnergyBalance = 55;
    }

    void Awake(){
        DontDestroyOnLoad(this); // avoids destrucion on load

        if (FindObjectsOfType(GetType()).Length > 1){ //check if dontdestroy object exists more than ones if true destroy new object
            Destroy(gameObject);
        }
    }

//    public IEnumerator StartCountdown(float TotalTime = 10){
//        TimeRemaining = TotalTime;
//        while (TimeRemaining > 0){
//            Debug.Log("Countdown: " + TimeRemaining);
//            yield return new WaitForSeconds(1.0f);
//            TimeRemaining--;
//           Debug.Log(TimeRemaining);
//        }
//
//        if (TimeRemaining == 0){
//            Debug.Log("Oh, Hi mark");
//        }
//    }

//    void Start(){
//        StartCoroutine(StartCountdown());
//    }


}
