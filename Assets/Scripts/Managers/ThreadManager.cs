using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThreadManager : MonoBehaviour
{
    [Header("Thread Settings")]
    [SerializeField] private int currentThreadCount;
    [SerializeField] private int giftThreadCount;

    [Header("Placement Settings")]
    [SerializeField] private Transform[] threadPlacementTransforms;
    [SerializeField] private Transform threadStackTransform;
    [SerializeField] private float placementDuration;

    private List<Thread> threads = new List<Thread>();
    private ObjectPooler pooler;

    private Thread currentThread;

    public void Init()
    {
        ActionManager.GainRope += OnEarnThread;
        ActionManager.ThreadSelection += OnThreadSelection;
        ActionManager.ClearThreadSelection += OnClearThreadSelection;
        ActionManager.GetSelectedThread += OnGetSelectedThread;
        pooler = ObjectPooler.Instance;

        for (int i = 0; i < currentThreadCount; i++)
        {
            OnEarnThread();
        }
    }

    public void DeInit()
    {
        ActionManager.GainRope -= OnEarnThread;
        ActionManager.ThreadSelection -= OnThreadSelection;
        ActionManager.ClearThreadSelection -= OnClearThreadSelection;
        ActionManager.GetSelectedThread -= OnGetSelectedThread;

        for (int i = 0; i < threads.Count; i++)
        {
            threads[i].DeInit();
        }
    }

    private void OnEarnThread()
    {
        Thread newThread = pooler.GetPooledThread();
        newThread.gameObject.SetActive(true);
        newThread.transform.position = threadStackTransform.position;
        newThread.Init();
        threads.Add(newThread);

        for (int i = 0; i < threadPlacementTransforms.Length; i++)
        {
            if (i >= threads.Count) return; 
            threads[i].transform.DOMove(threadPlacementTransforms[i].position, placementDuration);
        }
    }

    private void OnThreadSelection(Thread selectedThread)
    {
        currentThread = selectedThread;
    }

    private void OnClearThreadSelection()
    {
        currentThread = null;
    }

    private void OnGetSelectedThread(SewingMachineBase machine)
    {
        if(currentThread != null)
        {
            threads.Remove(currentThread);
            StartCoroutine(machine.ProduceClothes(currentThread.MoveToTarget(machine.GetThreadTransform))); 
        }
    }
}
