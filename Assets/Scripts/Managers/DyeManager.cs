using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeManager : MonoBehaviour
{
    [SerializeField] private DyeMachineBase[] machines;
    private LevelManager levelManager;

    public void Init()
    {
        levelManager = LevelManager.Instance;
        int levelIndex = levelManager.LevelIndex + 1;
        for (int i = 0; i < machines.Length; i++)
        {
            machines[i].Init(levelIndex);
        }
    }

    public void DeInit()
    {
        for (int i = 0; i < machines.Length; i++)
        {
            machines[i].DeInit();
        }
    }
}
