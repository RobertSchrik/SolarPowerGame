using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Question {

    public string question;
    public bool isTrue;

    public Texture NPCSprite;

     //Positive Scores
    public int PositiveMoneyScore;
    public int PositiveSocialScore;
    public int PositiveEnergyScore;

    //Negative Scores
    public int negativeMoneyScore;
    public int negativeSocialScore;
    public int negativeEnergyScore;
}

