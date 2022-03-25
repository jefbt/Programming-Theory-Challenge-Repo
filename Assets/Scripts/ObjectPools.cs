using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    private static ObjectPools instance = null;

    [SerializeField] List<GameObject> poolingObjects = new List<GameObject>();
    [SerializeField] List<int> poolingSize = new List<int>();
    [SerializeField] int defaultPoolSize = 20;

    private List<string> poolingTag;
    private Dictionary<string, List<GameObject>> pooledObjects;

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

            SetPoolTags();
            AdjustPoolSize();

            CreatePools();
        }
    }

    void SetPoolTags()
    {
        poolingTag = new List<string>();
        foreach (GameObject o in poolingObjects)
        {
            poolingTag.Add(o.tag);
        }
    }

    void AdjustPoolSize()
    {
        if (poolingSize.Count != poolingObjects.Count)
        {
            poolingSize = new List<int>();
            while (poolingSize.Count < poolingObjects.Count)
            {
                poolingSize.Add(defaultPoolSize);
            }
        }
    }

    void CreatePools()
    {
        pooledObjects = new Dictionary<string, List<GameObject>>();
        foreach (GameObject o in poolingObjects)
        {
            int index = poolingObjects.IndexOf(o);
            int amount = poolingSize[index];

            while(amount > 0)
            {
                GameObject newObject = Instantiate(o, transform);
                newObject.SetActive(false);
                if (!pooledObjects.ContainsKey(o.tag))
                {
                    pooledObjects.Add(o.tag, new List<GameObject>());
                }
                pooledObjects[o.tag].Add(newObject);
                amount--;
            }
        }
    }

    public static void DestroyObject(GameObject triggeringObject)
    {
        triggeringObject.SetActive(false);
    }

    public static GameObject GetObject(string tag)
    {
        foreach(GameObject o in instance.pooledObjects[tag])
        {
            if (o.activeInHierarchy == false)
            {
                o.SetActive(true);
                return o;
            }
        }

        return null;
    }

}
