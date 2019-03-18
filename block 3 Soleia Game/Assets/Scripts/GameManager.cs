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

    }

    void Update(){

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