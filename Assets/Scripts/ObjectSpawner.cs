using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float spawnTime = 3f;
    [SerializeField] float startDelay = 3f;
    [SerializeField] int harderSpawnCount = 3;
    [SerializeField] float harderTimerDecrease = 0.1f;

    private float spawnZ;
    private float spawnLimitPosition;
    private float nextSpawnTimer;
    
    private Coroutine spawnCoroutine;

    private bool isSpawning = false;

    private int spawnCount = 0;

    private void Awake()
    {
        spawnTime *= 1f - MainMenuManager.difficulty / 10f;
        startDelay *= 1f - MainMenuManager.difficulty / 10f;
        harderTimerDecrease *= 1f + MainMenuManager.difficulty / 10f;

        SetSpawnLimits();
        StartSpawning();
    }

    void SetSpawnLimits()
    {
        Vector3 bounds = GetComponent<BoxCollider>().size;
        spawnLimitPosition = bounds.x / 2;
        spawnZ = transform.position.z;
    }

    IEnumerator NextSpawn()
    {
        while(isSpawning)
        {
            yield return new WaitForSeconds(nextSpawnTimer);
            Spawn();
            nextSpawnTimer = spawnTime;
        }
    }

    void Spawn()
    {
        GameObject spawningObject = ObjectPools.GetObject(spawnObject.tag);
        if (spawningObject != null)
        {
            spawningObject.transform.position = RandomSpawnPosition();
        }
        VerifyDifficulty();
    }

    void VerifyDifficulty()
    {
        spawnCount++;
        if (spawnCount > harderSpawnCount)
        {
            spawnCount = 0;
            spawnTime -= harderTimerDecrease;
            if (spawnTime < 0.5f)
            {
                spawnTime = 0.5f;
            }
        }
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnLimitPosition, spawnLimitPosition), 0, spawnZ);
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopCoroutine(spawnCoroutine);
    }

    public void StartSpawning()
    {
        nextSpawnTimer = startDelay;
        isSpawning = true;
        spawnCoroutine = StartCoroutine(NextSpawn());
    }
}
