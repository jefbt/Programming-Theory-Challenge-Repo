using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI playerBestScoreText;

    [SerializeField] TextMeshProUGUI bestScoreNamesText;
    [SerializeField] TextMeshProUGUI bestScorePointsText;

    [SerializeField] int bestScoresToShow = 3;

    int[] bestScores;
    string[] bestNames;

    int playerBestScore;
    string playerName;

    private void OnEnable()
    {
        LoadBestScores();
    }

    void LoadBestScores()
    {
        if (PlayerPrefs.HasKey("PlayerBestScore"))
        {
            playerBestScore = PlayerPrefs.GetInt("PlayerBestScore");
        }
        else
        {
            playerBestScore = 0;
        }

        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            playerName = "";
        }

        bestScores = new int[bestScoresToShow];
        bestNames = new string[bestScoresToShow];

        for (int i = 0; i < bestScoresToShow; i++)
        {
            if (PlayerPrefs.HasKey("BestName" + i))
            {
                bestNames[i] = PlayerPrefs.GetString("BestName" + i);
            }
            else
            {
                bestNames[i] = "Player #" + i;
            }
            if (PlayerPrefs.HasKey("BestScore" + i))
            {
                bestScores[i] = PlayerPrefs.GetInt("BestScore" + i);
            }
            else
            {
                bestScores[i] = 21 - i * 3;
            }

        }

        ShowScores();
    }

    void SaveScores()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerBestScore", playerBestScore);

        for (int i = 0; i < bestScoresToShow; i++)
        {
            PlayerPrefs.SetString("BestName" + i, bestNames[i]);
            PlayerPrefs.SetInt("BestScore" + i, bestScores[i]);
        }
    }

    public void ShowScores()
    {
        playerScoreText.text = GameManager.GetScore().ToString();
        if (playerName == "")
        {
            playerBestScoreText.text = playerBestScore + " : YOUR BEST";
        }
        else
        {
            playerBestScoreText.text = playerBestScore + " : " + playerName +"'s BEST";
        }

        bestScoreNamesText.text = "";
        bestScorePointsText.text = "";

        for (int i = 0; i < bestScoresToShow; i++)
        {
            bestScoreNamesText.text += bestNames[i] + " :\n";
            bestScorePointsText.text += bestScores[i] + "\n";
        }
    }

    public void UpdateScores(TMP_InputField inputName)
    {
        if (GameManager.GetScore() > playerBestScore)
        {
            playerBestScore = GameManager.GetScore();
            playerName = inputName.text;
        }

        for (int i = 0; i < bestScoresToShow; i++)
        {
            if (GameManager.GetScore() > bestScores[i])
            {
                for (int j = bestScoresToShow - 1; j > i; j--)
                {
                    bestScores[j] = bestScores[j - 1];
                    bestNames[j] = bestNames[j - 1];
                }
                bestScores[i] = GameManager.GetScore();
                bestNames[i] = playerName;
                break;
            }
        }

        SaveScores();

        ShowScores();
    }
}
