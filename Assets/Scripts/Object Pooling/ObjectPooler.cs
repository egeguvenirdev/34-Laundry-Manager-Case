using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [SerializeField] private List<ObjectPooledItem> itemsToPool;
    [SerializeField] private List<ObjectPooledItem> enemyiesToPool;
    [SerializeField] private GameObject pooledObjectHolder;

    private List<GameObject> pooledObjects;
    private List<GameObject> pooledEnemies;

    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPooledItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
    public GameObject GetPooledObjectWithTag(string tag)
    {
        for (int i = pooledObjects.Count - 1; i > -1; i--)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        //pooledObjects.First(o => o.activeInHierarchy && o.tag == tag);
        foreach (ObjectPooledItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    obj.transform.SetParent(pooledObjectHolder.transform);
                    return obj;
                }
            }
        }
        return null;
    }

    public GameObject GetPooledEnemy(EnemyType enemyType)
    {
        for (int i = pooledEnemies.Count - 1; i > -1; i--)
        {
            if (!pooledEnemies[i].activeInHierarchy && pooledEnemies[i].GetComponent<EnemyBase>().enemyType == enemyType)
            {
                return pooledEnemies[i];
            }
        }
        //pooledObjects.First(o => o.activeInHierarchy && o.tag == tag);
        foreach (ObjectPooledItem item in enemyiesToPool)
        {
            if (item.objectToPool.GetComponent<EnemyBase>().enemyType == enemyType)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledEnemies.Add(obj);
                    obj.transform.SetParent(pooledObjectHolder.transform);
                    return obj;
                }
            }
        }
        return null;
    }
}