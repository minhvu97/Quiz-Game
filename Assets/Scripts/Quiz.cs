using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestionSO;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    private bool isAnswerSelected = false;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    private bool isComplete = false;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.SetTotalQuestion(questions.Count);
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Start()
    {
        GetNextQuestion();
    }

    private void Update()
    {
        // update timer UI count down
        timerImage.fillAmount = timer.GetFillFraction();

        // change to next question
        if (timer.ShouldLoadNextQuestion() == true)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            timer.SetLoadNextQuestion(false);
            isAnswerSelected = false;
            GetNextQuestion();
            // On Complete Game
        }

        // timeout without getting any answer
        if (isAnswerSelected == false && !timer.IsAnsweringQuestion())
        {
            OnTimeOut();
        }
    }

    private void OnTimeOut()
    {
        DisplayAnswer(-1); // -1 is for case Timeout
        SetButtonState(false);

        // increase progress
        progressBar.value++;
    }
    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);

        // increase progress
        progressBar.value++;

        // stop timer early
        isAnswerSelected = true;
        timer.CancelTimer();

        // Display score
        scoreText.text = "Score: " + scoreKeeper.CalculateScore().ToString() + "%";

    }
    private void DisplayAnswer(int index)
    {
        if (index == currentQuestionSO.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            scoreKeeper.IncreaseCorrectAnswer();
        }
        else if (index >= 0)
        {
            questionText.text = "Sorry, the correct answer was:\n" + currentQuestionSO.GetAnswer(currentQuestionSO.GetCorrectAnswerIndex());
        }
        else // index == -1
        {
            questionText.text = "Timeout!";
        }
        Image buttonImage = answerButtons[currentQuestionSO.GetCorrectAnswerIndex()].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
        scoreKeeper.IncreaseQuestionsSeen();
    }
    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestionSO = questions[index];
        if (questions.Contains(currentQuestionSO))
            questions.Remove(currentQuestionSO);
    }
    private void DisplayQuestion()
    {
        questionText.text = currentQuestionSO.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestionSO.GetAnswer(i);
        }
    }
    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
    public bool GameFinish()
    {
        return isComplete;
    }
}
