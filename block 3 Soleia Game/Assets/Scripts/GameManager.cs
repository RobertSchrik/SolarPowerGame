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
    private Text textScoreOne; //score one value

    [SerializeField]
    private Text textScoreTwo; //score two value

    [SerializeField]
    private Text textScoreThree; // score three value

    [SerializeField]
    private Text textScoreFour; // score four value

    [SerializeField]
    public float timeBetweemQuestions = 1f;

    public static int addScoreOneTo; // int to static int converter for scoreone
    public static int addScoreTwoTo; // int to static int converter for scoretwo
    public static int addScoreThreeTo; // int to static int converter for scorethree
    public static int addScoreFourTo; // int to static int converter for scorefour

    [SerializeField]
    private Animator animator;

    void Start()
    {

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        CurrentScore.text = "Completed Tasks: " + ScoreInformation.CurrentScore.ToString(); //set UI on screen
        textScoreOne.text = "Balance: " + ScoreInformation.ScoreOne.ToString() + "Million"; //set UI on screen
        textScoreTwo.text = "Social Status: " + ScoreInformation.ScoreTwo.ToString(); //set UI on screen
        textScoreThree.text = "Energy: " + ScoreInformation.ScoreThree.ToString() + "Kwh"; //set UI on screen
        textScoreFour.text = "Score: " + ScoreInformation.ScoreFour.ToString(); //set UI on screen

        addScoreOneTo = currentQuestion.addScoreOne; // set question score to game manager addscoreonto *static int required.
        addScoreTwoTo = currentQuestion.addScoreTwo; // set question score to game manager addscoreonto *static int required.
        addScoreThreeTo = currentQuestion.addScoreThree; // set question score to game manager addscoreonto *static int required.
        addScoreFourTo = currentQuestion.addScoreFour; // set question score to game manager addscoreonto *static int required.
    }

    void Update(){
        addScoreOneTo = currentQuestion.addScoreOne;
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        questionText.text = currentQuestion.question;
    }

    IEnumerator TransitionToNextQuestion ()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweemQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void UserSelectYes()
    {
        animator.SetTrigger("Yes");
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
            ScoreInformation.CurrentScore += 1;  //Add Score to Scoreinformation
            CurrentScore.text = "Score: " + ScoreInformation.CurrentScore.ToString();  //add Scoreinformation to UI
            ScoreInformation.ScoreOne += addScoreOneTo; //update score on correct
            ScoreInformation.ScoreTwo += addScoreTwoTo; //update score on correct
            ScoreInformation.ScoreThree += addScoreThreeTo; //update score on correct
            ScoreInformation.ScoreFour += addScoreFourTo; //update score on correct
        }
        else
        {
            ScoreInformation.CurrentScore += 1;  //Add Score to Scoreinformation
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());

    }

    public void UserSelectNo()
    {
        animator.SetTrigger("No");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
            ScoreInformation.CurrentScore += 1; //Add Score to Scoreinformation
            ScoreInformation.ScoreOne += addScoreOneTo; //update score on correct
            ScoreInformation.ScoreTwo += addScoreTwoTo; //update score on correct
            ScoreInformation.ScoreThree += addScoreThreeTo; //update score on correct
            ScoreInformation.ScoreFour += addScoreFourTo; //update score on correct
            CurrentScore.text = "Score: " + ScoreInformation.CurrentScore.ToString(); //add Scoreinformation to UI
        }
        else
        {
            ScoreInformation.CurrentScore += 1;  //Add Score to Scoreinformation
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());

    }
}