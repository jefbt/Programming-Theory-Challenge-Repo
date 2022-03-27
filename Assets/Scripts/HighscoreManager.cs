using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] Text playerScoreText;
    [SerializeField] Text playerBestScoreText;

    [SerializeField] Text bestScoreNamesText;
    [SerializeField] Text bestScorePointsText;

    [SerializeField] int bestScoresToShow = 3;

    private void OnEnable()
    {
        playerScoreText.text = GameManager.GetScore().ToString();
    }
}
