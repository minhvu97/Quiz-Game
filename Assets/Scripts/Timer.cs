using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timerToCompleteQuestion = 30f;
    [SerializeField] float timerToJumpToNextQuestion = 10;
    private bool isAnsweringQuestion = true;
    private bool loadNextQuestion = false;
    private float fillFraction = 1;
    private float timerValue;

    private void Start()
    {
        timerValue = timerToCompleteQuestion;
    }
    private void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        if (timerValue > 0) // doing the count down
        {
            fillFraction = timerValue / (isAnsweringQuestion ? timerToCompleteQuestion : timerToJumpToNextQuestion);
        }
        else
        {
            if (!isAnsweringQuestion) // Start timer to complete question
            {
                timerValue = timerToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
            else // delay a while before moving on to next question
            {
                timerValue = timerToJumpToNextQuestion;
                isAnsweringQuestion = false;
            }
        }
        timerValue -= Time.deltaTime;
        // Debug.Log(timerValue);
    }

    public bool IsAnsweringQuestion()
    {
        return isAnsweringQuestion;
    }

    public bool ShouldLoadNextQuestion()
    {
        return loadNextQuestion;
    }

    public void SetLoadNextQuestion(bool value)
    {
        loadNextQuestion = value;
    }

    public float GetFillFraction()
    {
        return fillFraction;
    }
}
