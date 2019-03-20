using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // sliders
    public Slider EnergySlider;
    public Slider MoneySlider;
    public Slider SocialSlider;

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    [Space(10)]
    private Text questionText;

    [SerializeField]
    private Text CurrentScore; //current score of correct questions

    [SerializeField]
    [Space(10)]
    private Text textMoneyScore; //score one value

    [SerializeField]
    private Text textSocialScore; //score two value

    [SerializeField]
    private Text textEnergyScore; // score three value

    [SerializeField]
    [Space(10)]
    public float timeBetweemQuestions = 1f;

    //static yes answers.
    public static int AddMoneyTo; 
    public static int AddSocialTo;
    public static int AddEnergyTo; 

    //static no answers.
    public static int RemoveMoneyTo;
    public static int RemoveSocialTo;
    public static int RemoveEnergyTo;

    [SerializeField]
    [Space(10)]
    private RawImage NPCTextureSprite;

    public Transform DialogueManager;
    public Animator animator;

    void Start(){

        if (unansweredQuestions == null || unansweredQuestions.Count == 0){
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();

        //Put score information in text format on screen.
        CurrentScore.text = "Completed Tasks: " + ScoreInformation.CurrentScore.ToString();
       // textMoneyScore.text = "Balance: " + ScoreInformation.MoneyBalance.ToString() + "Million";
       // textSocialScore.text = "Social Status: " + ScoreInformation.SocialBalance.ToString();
       // textEnergyScore.text = "Energy: " + ScoreInformation.EnergyBalance.ToString() + "Kwh";

        //When player chooses yes add these scores to the total score.
        AddMoneyTo = currentQuestion.PositiveMoneyScore;
        AddSocialTo = currentQuestion.PositiveSocialScore;
        AddEnergyTo = currentQuestion.PositiveEnergyScore;

        //When player chooses no add these scores to the total score.
        RemoveMoneyTo = currentQuestion.negativeMoneyScore;
        RemoveSocialTo = currentQuestion.negativeSocialScore;
        RemoveEnergyTo = currentQuestion.negativeEnergyScore;

        // slider gets updated based on the current score.
        EnergySlider.value = ScoreInformation.EnergyBalance;
        MoneySlider.value = ScoreInformation.MoneyBalance;
        SocialSlider.value = ScoreInformation.SocialBalance;

        NPCTextureSprite.texture = currentQuestion.NPCSprite;
    }

    void SetCurrentQuestion(){
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        questionText.text = currentQuestion.question;
    }

    IEnumerator TransitionToNextQuestion (){
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweemQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    public void UserSelectYes(){
        if (currentQuestion.isTrue){
            //When yes selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += AddMoneyTo;
            ScoreInformation.SocialBalance += AddSocialTo;
            ScoreInformation.EnergyBalance += AddEnergyTo;
            
            
            
        } else{
            //When no selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += RemoveMoneyTo;
            ScoreInformation.SocialBalance += RemoveSocialTo;
            ScoreInformation.EnergyBalance += RemoveEnergyTo;
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectNo(){
        if (!currentQuestion.isTrue){
            //When yes selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += AddMoneyTo;
            ScoreInformation.SocialBalance += AddSocialTo;
            ScoreInformation.EnergyBalance += AddEnergyTo;
        } else{
            //When no selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += RemoveMoneyTo;
            ScoreInformation.SocialBalance += RemoveSocialTo;
            ScoreInformation.EnergyBalance += RemoveEnergyTo;
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && animator.GetBool("IsOpen"))
        {
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(currentQuestion.dialogue);
        }
    }

}