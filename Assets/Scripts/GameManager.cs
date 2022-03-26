using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private int score = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            NewGame();
        }
    }

    public static void GetCollectible(GameObject collectibleObject)
    {
        SoundEffectManager.PlayCollectibleStatic();
        ObjectPools.DestroyObject(collectibleObject);
        if (instance != null) instance.Score();
    }

    void Score(int points = 1)
    {
        score += points;
        FindObjectOfType<GameUI>().UpdateScoreText(score);
    }

    public static void PlayerCrash(GameObject playerObject)
    {
        SoundEffectManager.PlayCrashStatic();
        Destroy(playerObject);
        if (instance != null) instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NewGame()
    {
        score = 0;
        Debug.Log("Score: " + score);
    }

    public static int GetScore()
    {
        return instance.score;
    }
}
