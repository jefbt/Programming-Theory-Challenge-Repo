using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float spawnTime = 3f;
    [SerializeField] float startDelay = 3f;

    private float spawnZ;
    private float spawnLimitPosition;
    private float nextSpawnTimer;
    private Coroutine spawnCoroutine;

    private bool isSpawning = false;

    private void Awake()
    {
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
