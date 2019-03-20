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

    public bool PlayerInRange;
    public Dialogue dialogue;
    public Transform DialogueManager;

    public Animator animator;

    void update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && animator.GetBool("IsOpen"))
        {
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}

