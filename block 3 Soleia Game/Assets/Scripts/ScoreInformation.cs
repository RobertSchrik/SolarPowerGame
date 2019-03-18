using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-- All code within this script stays after scene reload. -- //

public class ScoreInformation : MonoBehaviour
{
    public static int CurrentScore; // player score
    public static int ScoreOne; // score one
    public static int ScoreTwo; // score two
    public static int ScoreThree; // score three
    public static int ScoreFour; // score four

    void Awake()
    {
        DontDestroyOnLoad(this); // avoids destrucion on load

        if (FindObjectsOfType(GetType()).Length > 1) //check if dontdestroy object exists more than ones if true destroy new object
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        //CurrentScore = 0;
    }
}
