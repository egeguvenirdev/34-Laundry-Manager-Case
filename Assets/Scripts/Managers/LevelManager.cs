using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    [Header("Level Props")]
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private bool forceLevel;
    [SerializeField] private int forceLevelIndex;
    private GameObject currentLevel;

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(ConstantVariables.LevelValue.Level);

        set => PlayerPrefs.SetInt(ConstantVariables.LevelValue.Level, PlayerPrefs.GetInt(ConstantVariables.LevelValue.Level) + value);
    }

    public void Init()
    {
        GenerateCurrentLevel();
    }

    public void DeInit()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }

    public void GenerateCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        if (forceLevel)
        {
            currentLevel = Instantiate(levelPrefabs[forceLevelIndex]);
            return;
        }

        if (LevelIndex >= levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)]);
            return;
        }

        currentLevel = Instantiate(levelPrefabs[LevelIndex]);
    }

    public void LevelUp()
    {
        LevelIndex = 1;

        if (LevelIndex >= levelPrefabs.Length)
        {
            LevelIndex = Random.Range(0, levelPrefabs.Length);
        }

        Init();
    }
}
