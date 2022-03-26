using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] string prefixScoreText = "SCORE: ";

    public void UpdateScoreText(int score)
    {
        scoreText.text = prefixScoreText + score;
    }

    public void UpdateScoreText()
    {
        scoreText.text = prefixScoreText + GameManager.GetScore();
    }

    public void ToMenu()
    {
        SoundEffectManager.PlayCloseStatic();
        SceneManager.LoadScene("Menu");
    }
}
