using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SoundEffectManager.PlayStartStatic();
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        SoundEffectManager.PlayCloseStatic();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
