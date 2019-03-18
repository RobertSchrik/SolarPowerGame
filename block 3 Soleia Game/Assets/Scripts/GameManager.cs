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
    private Text CurrentScore;

    [SerializeField]
    private Text ScoreOne;

    [SerializeField]
    private Text ScoreTwo;

    [SerializeField]
    private Text ScoreThree;

    [SerializeField]
    private Text ScoreFour;

    [SerializeField]
    public float timeBetweemQuestions = 1f;

    
    [SerializeField]
    private Animator animator;

    void Start()
    {

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        CurrentScore.text = "Score: " + ScoreInformation.CurrentScore.ToString(); //set UI on screen
        ScoreOne.text = "Score: " + ScoreInformation.ScoreOne.ToString(); //set UI on screen
        ScoreTwo.text = "Score: " + ScoreInformation.ScoreTwo.ToString(); //set UI on screen
        ScoreThree.text = "Score: " + ScoreInformation.ScoreThree.ToString(); //set UI on screen
        ScoreFour.text = "Score: " + ScoreInformation.ScoreFour.ToString(); //set UI on screen

    }

    void Update(){
        ScoreOne.text = "hello world1"; //Score one
        ScoreTwo.text = "hello world2"; //Score Two
        ScoreThree.text = "hello world3"; //Score Three
        ScoreFour.text = "hello world4"; // Score Four
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
        }
        else
        {
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
            CurrentScore.text = "Score: " + ScoreInformation.CurrentScore.ToString(); //add Scoreinformation to UI
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());

    }
}