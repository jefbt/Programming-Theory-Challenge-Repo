using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameManager instance = null;

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
        }
    }

    public static void GetCollectible()
    {

    }

    public static void PlayerCrash(GameObject playerObject)
    {
        Destroy(playerObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
