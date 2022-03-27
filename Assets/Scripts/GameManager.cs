using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

[DefaultExecutionOrder(100)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] GameObject collectibleParticle;
    [SerializeField] GameObject playerExplosionParticle;
    [SerializeField] Transform playerStart;

    [SerializeField] CinemachineVirtualCamera gameCamera;
    [SerializeField] CinemachineVirtualCamera cinematicCamera;

    [SerializeField] FadeToHighscore screenFader;
    [SerializeField] GameObject highScorePanel;

    [SerializeField] float playerImpulse = 180f;

#if UNITY_EDITOR
    public bool debugPlayerInvencible = false;
#endif

    private int score = 0;
    private bool isCinematic = false;

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
        if (particle != null)
        {
            particle.transform.position = playerObject.transform.position;
            particle.GetComponent<ParticleSystem>().Play();
        }

#if UNITY_EDITOR
        if (instance.debugPlayerInvencible) return;
#endif

        ObjectPools.DestroyObject(playerObject);

        if (instance != null) instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NewGame()
    {
        score = 0;
        isCinematic = false;
        FindObjectOfType<GameUI>().UpdateScoreText(score);

        screenFader.gameObject.SetActive(false);
        highScorePanel.SetActive(false);

        SkyboxChanger.SetSkybox();

        GameObject player = ObjectPools.GetObject("Player");
        player.transform.position = playerStart.position;
    }

    public static int GetScore()
    {
        return instance.score;
    }

    public static void ReachGate(GateControl gate)
    {
        ObjectPools.Clear("OpponentShip");
        ObjectPools.Clear("Meteor");
        instance.StartCoroutine(instance.PrepareForFinishingTheLevel(gate));
    }

    IEnumerator PrepareForFinishingTheLevel(GateControl gate)
    {
        yield return new WaitForSeconds(gate.finalWaitTime);
        gate.SetPlayerFinishPositionActive();
    }

    public static void PrepareFinalCinematic(PlayerShip player, GateControl gate)
    {
        if (!instance.isCinematic)
        {
            instance.isCinematic = true;
            player.RemovePlayerControl();
            player.MoveTo(gate.GetPlayerFinalPosition(), gate.GetFinalMoveSpeed(), instance.ReachedFinalPosition);
            gate.TurnOffFinishCollider();
        }
    }

    public void ReachedFinalPosition(PlayerShip player)
    {
        gameCamera.Priority = 0;
        cinematicCamera.Priority = 10;

        foreach(TrailRenderer trail in player.GetComponentsInChildren<TrailRenderer>())
        {
            trail.enabled = true;
        }

        PlayableDirector director = FindObjectOfType<PlayableDirector>();
        director.Play();
        StartCoroutine(WarpZone(player));
    }

    IEnumerator WarpZone(PlayerShip player)
    {
        yield return new WaitForSeconds(2.5f);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -9);
        player.GetComponent<Rigidbody>().AddForce(Vector3.forward * playerImpulse, ForceMode.Impulse);

        SoundEffectManager.PlayFinishStatic();

        screenFader.gameObject.SetActive(true);
        screenFader.StartFading(BringUpHighScore);
    }

    void BringUpHighScore()
    {
        highScorePanel.SetActive(true);
    }
}
