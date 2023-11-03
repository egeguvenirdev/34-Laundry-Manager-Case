using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    [SerializeField] private List<Thread> threads = new List<Thread>();
    private ObjectPooler pooler;

    public void Init()
    {
        ActionManager.GainRope += OnEarnThread;
        pooler = ObjectPooler.Instance;

        for (int i = 0; i < threads.Count; i++)
        {
            threads[i].Init();
        }
    }

    public void DeInit()
    {
        ActionManager.GainRope -= OnEarnThread;

        for (int i = 0; i < threads.Count; i++)
        {
            threads[i].DeInit();
        }
    }

    private void OnEarnThread()
    {

    }
}
