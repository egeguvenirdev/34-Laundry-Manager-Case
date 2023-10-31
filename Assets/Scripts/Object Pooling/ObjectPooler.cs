using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [Header("Item Types")]
    [SerializeField] private List<ObjectPooledItem> clothesToPool;

    [Header("Holder")]
    [SerializeField] private GameObject pooledObjectHolder;

    private List<ClothBase> pooledClothes;

    private void Awake()
    {
        //produce the items
        pooledClothes = new List<ClothBase>();
        foreach (ObjectPooledItem item in clothesToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);
                pooledClothes.Add(obj.GetComponent<ClothBase>());
            }
        }
    }

    public ClothBase GetPooledCloth(ClothType clothesType)
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
            if (item.objectToPool.GetComponent<ClothBase>().clothType == clothesType)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.transform.SetParent(pooledObjectHolder.transform);
                    obj.SetActive(false);
                    ClothBase producedCloth = obj.GetComponent<ClothBase>();
                    pooledClothes.Add(producedCloth);
                    return producedCloth;
                }
            }
        }
        return null;
    }
}