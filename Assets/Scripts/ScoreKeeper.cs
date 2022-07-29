using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionsSeen = 0;
    private int totalQuestion = 1;
    public int GetCorrectAnswer()
    {
        return correctAnswers;
    }
    public void IncreaseCorrectAnswer()
    {
        correctAnswers++;
    }
    public void ClearCorrectAnswer()
    {
        correctAnswers = 0;
    }
    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }
    public void IncreaseQuestionsSeen()
    {
        questionsSeen++;
    }
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)totalQuestion * 100);
    }
    public void SetTotalQuestion(int number)
    {
        totalQuestion = number;
    }
}
