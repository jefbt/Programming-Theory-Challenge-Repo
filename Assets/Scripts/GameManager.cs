using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(100)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] GameObject collectibleParticle;
    [SerializeField] GameObject playerExplosionParticle;
    [SerializeField] Transform playerStart;

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
        
        GameObject particle = ObjectPools.GetObject("CollectibleParticle");
        particle.transform.position = collectibleObject.transform.position;
        particle.GetComponent<ParticleSystem>().Play();

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

        GameObject particle = ObjectPools.GetObject("PlayerExplosion");
        particle.transform.position = playerObject.transform.position;
        particle.GetComponent<ParticleSystem>().Play();

        ObjectPools.DestroyObject(playerObject);

        if (instance != null) instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NewGame()
    {
        score = 0;
        FindObjectOfType<GameUI>().UpdateScoreText(score);

        GameObject player = ObjectPools.GetObject("Player");
        player.transform.position = playerStart.position;
    }

    public static int GetScore()
    {
        return instance.score;
    }

    public static void ReachGate()
    {

    }
}
