using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectPooledItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}
