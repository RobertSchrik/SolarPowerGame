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
    public Text GameOverText;

    [SerializeField]
    public Text EventsText;

    [SerializeField]
    [Space(10)]
    public float timeBetweemQuestions = 1f;

    [SerializeField]
    public float timeBetweenEvent = 2f;

    //static yes answers.
    public static int AddMoneyTo; 
    public static int AddSocialTo;
    public static int AddEnergyTo; 

    //static no answers.
    public static int RemoveMoneyTo;
    public static int RemoveSocialTo;
    public static int RemoveEnergyTo;

    public static int PositiveCheckMoney = 0;
    public static int PositiveCheckSocial = 0;
    public static int PositiveCheckEnergy = 0;

    public static int NegativeCheckMoney = 0;
    public static int NegativeCheckSocial = 0;
    public static int NegativeCheckEnergy = 0;

    [SerializeField]
    [Space(10)]
    private RawImage NPCTextureSprite;

    [SerializeField]
    private RawImage PlayBackground;

    public Texture BadBackGroundImages;
    public Texture NormalBackGroundImages;
    public Texture GoodBackGroundImages;

    public Transform DialogueManager;
    public Animator animator;

    public GameObject gameOverScreen;
    public GameObject EventsScreen;

    public GameObject RawimageEnergy;
    public GameObject RawimageMoney;
    public GameObject RawimageSocial;

    [Space(10)]
    public GameObject MoneyInceaseText;
    public GameObject MoneyDecreaseText;
    public GameObject MoneyNoChangeText;

    [Space(10)]
    public GameObject SocialInceaseText;
    public GameObject SocialDecreaseText;
    public GameObject SocialNoChangeText;

    [Space(10)]
    public GameObject EnergyInceaseText;
    public GameObject EnergyDecreaseText;
    public GameObject EnergyNoChangeText;

    public Button yesstartButton;
    public Button nostartButton;

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

        PositiveCheckMoney += AddMoneyTo;
        PositiveCheckEnergy += AddEnergyTo;
        PositiveCheckSocial += AddSocialTo;

        NegativeCheckMoney += RemoveMoneyTo;
        NegativeCheckEnergy += RemoveSocialTo;
        NegativeCheckSocial += RemoveEnergyTo;

        //When player chooses no add these scores to the total score.
        RemoveMoneyTo = currentQuestion.negativeMoneyScore;
        RemoveSocialTo = currentQuestion.negativeSocialScore;
        RemoveEnergyTo = currentQuestion.negativeEnergyScore;

        // slider gets updated based on the current score.
        EnergySlider.value = ScoreInformation.EnergyBalance;
        MoneySlider.value = ScoreInformation.MoneyBalance;
        SocialSlider.value = ScoreInformation.SocialBalance;

        NPCTextureSprite.texture = currentQuestion.NPCSprite;

        GameOver();

        Events();

        ChangeBackGround();

        FindObjectOfType<DialogueManager>().StartDialogue(currentQuestion.dialogue);
    }

    void SetCurrentQuestion(){
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        questionText.text = currentQuestion.question;
    }

    IEnumerator TransitionToNextQuestion (){
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweemQuestions);
        DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        PositiveCheckMoney = 0;
        PositiveCheckSocial = 0;
        PositiveCheckEnergy = 0;
        NegativeCheckMoney = 0;
        NegativeCheckSocial = 0;
        NegativeCheckEnergy = 0;
        yesstartButton.interactable = !yesstartButton.interactable;
        nostartButton.interactable = !nostartButton.interactable;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   IEnumerator EventTrans (){
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenEvent);
        DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        PositiveCheckMoney = 0;
        PositiveCheckSocial = 0;
        PositiveCheckEnergy = 0;
        NegativeCheckMoney = 0;
        NegativeCheckSocial = 0;
        NegativeCheckEnergy = 0;
        EventsScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectYes(){
        if (currentQuestion.isTrue){
            //When yes selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += AddMoneyTo;
            ScoreInformation.SocialBalance += AddSocialTo;
            ScoreInformation.EnergyBalance += AddEnergyTo;

            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            FindObjectOfType<DialogueManager>().StartDialogueEndGood(currentQuestion.dialogueendgood);

            yesstartButton.interactable = !yesstartButton.interactable;
            nostartButton.interactable = !nostartButton.interactable;
        } else{
            //When no selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += RemoveMoneyTo;
            ScoreInformation.SocialBalance += RemoveSocialTo;
            ScoreInformation.EnergyBalance += RemoveEnergyTo;

            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            FindObjectOfType<DialogueManager>().StartDialogueEndBad(currentQuestion.dialogueendbad);

            yesstartButton.interactable = !yesstartButton.interactable;
            nostartButton.interactable = !nostartButton.interactable;
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

            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            FindObjectOfType<DialogueManager>().StartDialogueEndGood(currentQuestion.dialogueendgood);

            yesstartButton.interactable = !yesstartButton.interactable;
            nostartButton.interactable = !nostartButton.interactable;

        } else {
            //When no selected add the values to ScoreInforamtion
            ScoreInformation.CurrentScore += 1;
            ScoreInformation.MoneyBalance += RemoveMoneyTo;
            ScoreInformation.SocialBalance += RemoveSocialTo;
            ScoreInformation.EnergyBalance += RemoveEnergyTo;

            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            FindObjectOfType<DialogueManager>().StartDialogueEndBad(currentQuestion.dialogueendbad);

            yesstartButton.interactable = !yesstartButton.interactable;
            nostartButton.interactable = !nostartButton.interactable;

        }
        StartCoroutine(TransitionToNextQuestion());
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Return) && animator.GetBool("IsOpen")){
            DialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        }else if (Input.GetKeyDown(KeyCode.Return)){
           FindObjectOfType<DialogueManager>().StartDialogue(currentQuestion.dialogue);
        }
    }


    public void Events()
    {
        // if you want to trigger the event between a certan set of numbers other wise use the
        // == 25 if you want to trigger the event only on that number
        /*if(Mathf.Clamp(ScoreInformation.MoneyBalance,20,26) == ScoreInformation.MoneyBalance)
        {
            Debug.Log("money25");
        }*/

        // money 
        if (ScoreInformation.MoneyBalance == 25){
            Debug.Log("money25");
            EventsScreen.SetActive(true);
            EventsText.text = "your money is really low have some += 25";
            ScoreInformation.MoneyBalance += 25;
            RawimageEnergy.SetActive(false);
            RawimageMoney.SetActive(true);
            RawimageSocial.SetActive(false);
            StartCoroutine(EventTrans());
        }


        // social        
        if (ScoreInformation.SocialBalance == 50){
            Debug.Log("social50");
            EventsScreen.SetActive(true);           
            EventsText.text = "your social is medium have sum + 15";
            ScoreInformation.SocialBalance += 15;
            RawimageEnergy.SetActive(false);
            RawimageMoney.SetActive(false);
            RawimageSocial.SetActive(true);
            StartCoroutine(EventTrans());
        }


        // energy 
        if (ScoreInformation.EnergyBalance == 75){
            Debug.Log("energy75");
            EventsScreen.SetActive(true);                       
            EventsText.text = "your energy is to high, fuck you -25 energy";
            ScoreInformation.EnergyBalance -= 50;
            RawimageEnergy.SetActive(true);
            RawimageMoney.SetActive(false);
            RawimageSocial.SetActive(false);
            StartCoroutine(EventTrans()); 
        }
    }

    public void GameOver(){

        if (ScoreInformation.MoneyBalance >= 100){
            Debug.Log("Moneydead+");
            gameOverScreen.SetActive(true);
            GameOverText.text = "You died having to much money";
        }
        if (ScoreInformation.SocialBalance >= 100){
            Debug.Log("SocialDead+");
            gameOverScreen.SetActive(true);
            GameOverText.text = "You died having to much social";
        }
        if (ScoreInformation.EnergyBalance >= 100){
            Debug.Log("EnergyDead+");
            gameOverScreen.SetActive(true);
            GameOverText.text = "You died having to much energy";
        }
        if (ScoreInformation.MoneyBalance <= 0){
            Debug.Log("Moneydead-");
            gameOverScreen.SetActive(true);
            GameOverText.text = "You died having to little money";
        }
        if (ScoreInformation.SocialBalance <= 0){
            Debug.Log("SocialDead-");
            gameOverScreen.SetActive(true);
            GameOverText.text = "You died having to little social";
        }
        if (ScoreInformation.EnergyBalance <= 0){
            Debug.Log("EnergyDead-");
            gameOverScreen.SetActive(true);
            GameOverText.text = "You died having to little energy";
        }
    }

    public void MassResetOfDeathAndDestruction(){
        ScoreInformation.resetscorelists();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOverAccept(){
        if (PositiveCheckMoney > 0){
            MoneyInceaseText.SetActive(true);
        } else if(PositiveCheckMoney < 0){
            MoneyDecreaseText.SetActive(true);
        } else if(PositiveCheckMoney == 0){
            MoneyNoChangeText.SetActive(true);
        }

        if (PositiveCheckEnergy > 0){
            SocialInceaseText.SetActive(true);
        }
        else if (PositiveCheckEnergy < 0){
            SocialDecreaseText.SetActive(true);
        }
        else if (PositiveCheckEnergy == 0){
            SocialNoChangeText.SetActive(true);
        }

        if (PositiveCheckSocial > 0){
            EnergyInceaseText.SetActive(true);
        }
        else if (PositiveCheckSocial < 0){
            EnergyDecreaseText.SetActive(true);
        }
        else if (PositiveCheckSocial == 0){
            EnergyNoChangeText.SetActive(true);
        }
    }

    public void OnMouseOverDenied(){
        if (NegativeCheckMoney > 0){
            MoneyInceaseText.SetActive(true);
        }
        else if (NegativeCheckMoney < 0){
            MoneyDecreaseText.SetActive(true);
        }
        else if (NegativeCheckMoney == 0){
            MoneyNoChangeText.SetActive(true);
        }

        if (NegativeCheckEnergy > 0){
            SocialInceaseText.SetActive(true);
        }
        else if (NegativeCheckEnergy < 0){
            SocialDecreaseText.SetActive(true);
        }
        else if (NegativeCheckEnergy == 0){
            SocialNoChangeText.SetActive(true);
        }

        if (NegativeCheckSocial > 0){
            EnergyInceaseText.SetActive(true);
        }
        else if (NegativeCheckSocial < 0){
            EnergyDecreaseText.SetActive(true);
        }
        else if (NegativeCheckSocial == 0){
            EnergyNoChangeText.SetActive(true);
        }
    }

    public void OnMouseExit(){
        MoneyInceaseText.SetActive(false);
        MoneyDecreaseText.SetActive(false);
        MoneyNoChangeText.SetActive(false);

        SocialInceaseText.SetActive(false);
        SocialDecreaseText.SetActive(false);
        SocialNoChangeText.SetActive(false);

        EnergyInceaseText.SetActive(false);
        EnergyDecreaseText.SetActive(false);
        EnergyNoChangeText.SetActive(false);
    }

    public void ChangeBackGround(){
        if(ScoreInformation.EnergyBalance <= 25){
            PlayBackground.texture = BadBackGroundImages;
        }else if (ScoreInformation.EnergyBalance >= 75){
            PlayBackground.texture = GoodBackGroundImages;
        }else{
            PlayBackground.texture = NormalBackGroundImages;
        }
    }
}