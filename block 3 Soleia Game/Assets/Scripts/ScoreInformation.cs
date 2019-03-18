using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreInformation : MonoBehaviour
{
    public static int CurrentScore; // player score
    public static int ScoreOne;
    public static int ScoreTwo;
    public static int ScoreThree;
    public static int ScoreFour;

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
