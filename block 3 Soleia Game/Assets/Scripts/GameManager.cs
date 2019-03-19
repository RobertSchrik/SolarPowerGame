using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text CurrentScore; //current score of correct questions

    [SerializeField]
    private Text textMoneyScore; //score one value

    [SerializeField]
    private Text textSocialScore; //score two value

    [SerializeField]
    private Text textEnergyScore; // score three value

    [SerializeField]
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
    private Animator animator;

    [SerializeField]
    private RawImage NPCTextureSprite;

    void Start(){

        if (unansweredQuestions == null || unansweredQuestions.Count == 0){
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();

        //Put score information in text format on screen.
        CurrentScore.text = "Completed Tasks: " + ScoreInformation.CurrentScore.ToString();
        textMoneyScore.text = "Balance: " + ScoreInformation.MoneyBalance.ToString() + "Million";
        textSocialScore.text = "Social Status: " + ScoreInformation.SocialBalance.ToString();
        textEnergyScore.text = "Energy: " + ScoreInformation.EnergyBalance.ToString() + "Kwh";

        //When player chooses yes add these scores to the total score.
        AddMoneyTo = currentQuestion.PositiveMoneyScore;
        AddSocialTo = currentQuestion.PositiveSocialScore;
        AddEnergyTo = currentQuestion.PositiveEnergyScore;

        //When player chooses no add these scores to the total score.
        RemoveMoneyTo = currentQuestion.negativeMoneyScore;
        RemoveSocialTo = currentQuestion.negativeSocialScore;
        RemoveEnergyTo = currentQuestion.negativeEnergyScore;

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
        animator.SetTrigger("Yes");
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
        animator.SetTrigger("No");
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
}