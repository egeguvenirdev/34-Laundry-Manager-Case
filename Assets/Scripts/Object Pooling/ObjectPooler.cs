using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [Header("Item Types")]
    [SerializeField] private List<ObjectPooledItem> clothesToPool;
    [SerializeField] private List<ObjectPooledItem> slideTextToPool;
    [SerializeField] private List<ObjectPooledItem> threadsToPool;

    [Header("Holder")]
    [SerializeField] private GameObject pooledObjectHolder;

    private List<SewingMachineBase> pooledClothes;
    private List<GameObject> pooledThreads;
    private List<SlideText> pooledText;

    private void Awake()
    {
        //produce the items
        pooledClothes = new List<SewingMachineBase>();
        foreach (ObjectPooledItem item in clothesToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);
                pooledClothes.Add(obj.GetComponent<SewingMachineBase>());
            }
        }

        pooledText = new List<SlideText>();
        foreach (ObjectPooledItem item in slideTextToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);

                SlideText slideText = obj.GetComponent<SlideText>();
                slideText.Init();
                pooledText.Add(slideText);
            }
        }

        foreach (ObjectPooledItem item in threadsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);
                pooledThreads.Add(obj);
            }
        }
    }

    public SewingMachineBase GetPooledCloth(ClothType clothesType)
    {
        //search for the target item
        for (int i = pooledClothes.Count - 1; i > -1; i--)
        {
            if (!pooledClothes[i].gameObject.activeInHierarchy && pooledClothes[i].GetClothType == clothesType)
            {
                return pooledClothes[i];
            }
        }

        //if tthe pool not enough and can expand
        foreach (ObjectPooledItem item in clothesToPool)
        {
            if (item.objectToPool.GetComponent<SewingMachineBase>().GetClothType == clothesType)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.transform.SetParent(pooledObjectHolder.transform);
                    obj.SetActive(false);
                    SewingMachineBase producedCloth = obj.GetComponent<SewingMachineBase>();
                    pooledClothes.Add(producedCloth);
                    return producedCloth;
                }
            }
        }
        return null;
    }

    public GameObject GetPooledRope()
    {
        for (int i = pooledThreads.Count - 1; i > -1; i--)
        {
            if (!pooledThreads[i].activeInHierarchy)
            {
                return pooledThreads[i];
            }
        }
        //pooledObjects.First(o => o.activeInHierarchy && o.tag == tag);
        foreach (ObjectPooledItem item in threadsToPool)
        {
            if (item.shouldExpand)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledThreads.Add(obj);
                obj.transform.SetParent(pooledObjectHolder.transform);
                return obj;
            }
        }
        return null;
    }

    public SlideText GetPooledText()
    {
        //search for the target item
        for (int i = pooledText.Count - 1; i > -1; i--)
        {
            if (!pooledText[i].gameObject.activeInHierarchy)
            {
                return pooledText[i];
            }
        }

        //if tthe pool not enough and can expand
        foreach (ObjectPooledItem item in clothesToPool)
        {
            if (item.shouldExpand)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);

                SlideText slideText = obj.GetComponent<SlideText>();
                slideText.Init();
                pooledText.Add(slideText);

                return slideText;
            }
        }
        return null;
    }
}