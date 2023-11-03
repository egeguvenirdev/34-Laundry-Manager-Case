using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    [SerializeField] private Thread[] threads;
    private ObjectPooler pooler;

    public void Init()
    {
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Init();
        }
    }

    public void DeInit()
    {
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].DeInit();
        }
    }
}
