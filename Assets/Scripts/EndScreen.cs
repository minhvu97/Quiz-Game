using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reviewText;
    [SerializeField] GameObject playAgainBtn;
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void ShowFinalResult()
    {
        reviewText.text = "Congratulations!\nYou scored " + scoreKeeper.CalculateScore().ToString() + " %";
    }
}
