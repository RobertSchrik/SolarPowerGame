using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Question {

    public string question;
    public Texture NPCSprite;
    public bool isTrue;

    [Space(10)]
    //Positive Scores
    public int PositiveMoneyScore;
    public int PositiveSocialScore;
    public int PositiveEnergyScore;

    [Space(10)]
    //Negative Scores
    public int negativeMoneyScore;
    public int negativeSocialScore;
    public int negativeEnergyScore;
    [Space(10)]

    public Dialogue dialogue;

    [Space(10)]
    public Transform DialogueManager;
    public Animator animator;
}

