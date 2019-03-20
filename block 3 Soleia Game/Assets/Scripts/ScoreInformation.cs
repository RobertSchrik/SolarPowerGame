using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//-- All code within this script stays after scene reload. -- //

public class ScoreInformation : MonoBehaviour{
    //SavedScores
    public static int CurrentScore;
    public static int MoneyBalance = 50;
    public static int SocialBalance = 50;
    public static int EnergyBalance = 50;

    private GameManager gameManager;

//    float TimeRemaining;

    void Awake(){
        DontDestroyOnLoad(this); // avoids destrucion on load

        if (FindObjectsOfType(GetType()).Length > 1){ //check if dontdestroy object exists more than ones if true destroy new object
            Destroy(gameObject);
        }
    }

    public static void GameOver(){
        if (MoneyBalance >= 100)
        {
            Debug.Log("Moneydead+");
        }
        if (SocialBalance >= 100)
        {
            Debug.Log("SocialDead+");
        }
        if (EnergyBalance >= 100)
        {
            Debug.Log("EnergyDead+");
        }
        if (MoneyBalance <= 0)
        {
            Debug.Log("Moneydead-");
        }
        if (SocialBalance <= 0)
        {
            Debug.Log("SocialDead-");
        }
        if (EnergyBalance <= 0)
        {
            Debug.Log("EnergyDead-");
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
