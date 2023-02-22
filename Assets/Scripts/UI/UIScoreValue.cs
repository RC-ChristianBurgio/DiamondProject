using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreValue : MonoBehaviour
{
 
    [SerializeField] private TextMeshProUGUI scoreText;
    private int totalScore = 0;

    public void GetScore(int count)
    {
        totalScore += count;
        scoreText.text = totalScore.ToString();
    }
}
