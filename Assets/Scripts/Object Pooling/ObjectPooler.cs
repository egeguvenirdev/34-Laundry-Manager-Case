using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [Header("Item Types")]
    [SerializeField] private List<ObjectPooledItem> clothesToPool;
    [SerializeField] private List<ObjectPooledItem> slideTextToPool;

    [Header("Holder")]
    [SerializeField] private GameObject pooledObjectHolder;

    private List<SewingMachineBase> pooledClothes;
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
    }

    public SewingMachineBase GetPooledCloth(ClothType clothesType)
    {
        //search for the target item
        for (int i = pooledClothes.Count - 1; i > -1; i--)
        {
            if (!pooledClothes[i].gameObject.activeInHierarchy && pooledClothes[i].clothType == clothesType)
            {
                return pooledClothes[i];
            }
        }

        //if tthe pool not enough and can expand
        foreach (ObjectPooledItem item in clothesToPool)
        {
            if (item.objectToPool.GetComponent<SewingMachineBase>().clothType == clothesType)
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