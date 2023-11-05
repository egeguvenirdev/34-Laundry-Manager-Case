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
    private LevelPropsManager levelProps;

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
            currentLevel.GetComponent<LevelPropsManager>().SetLevelPropsUI();
            return;
        }

        if (LevelIndex >= levelPrefabs.Length)
        {
            int randomNumber = Random.Range(0, levelPrefabs.Length);
            currentLevel = Instantiate(levelPrefabs[randomNumber]);
            currentLevel.GetComponent<LevelPropsManager>().SetLevelPropsUI();
            return;
        }

        currentLevel = Instantiate(levelPrefabs[LevelIndex]);
        currentLevel.GetComponent<LevelPropsManager>().SetLevelPropsUI();
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
