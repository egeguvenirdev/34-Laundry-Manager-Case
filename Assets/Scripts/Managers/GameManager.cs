using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    private PlayerManager playerManager;
    private LevelManager levelManager;
    private UpdateManager updateManager;
    private CamManager camManager;
    private MoneyManager moneyManager;
    private UIManager uIManager;
    private BandRotator bandRotator;
    private MachineManager machineManager;
    private DyeManager dyeMachineManager;
    private ThreadManager threadManager;
    private ClothManager clothManager;

    void Start()
    {
        SetInits();
    }

    private void SetInits()
    {
        levelManager = LevelManager.Instance;
        levelManager.Init();

        uIManager = UIManager.Instance;
        uIManager.Init();

        moneyManager = MoneyManager.Instance;
        moneyManager.Init(clearPlayerPrefs);

        updateManager = FindObjectOfType<UpdateManager>();
        updateManager.Init();

        camManager = FindObjectOfType<CamManager>();

        bandRotator = FindObjectOfType<BandRotator>();

        machineManager = FindObjectOfType<MachineManager>();
        machineManager.Init();

        dyeMachineManager = FindObjectOfType<DyeManager>();
        dyeMachineManager.Init();

        threadManager = FindObjectOfType<ThreadManager>();
        threadManager.Init();

        clothManager = FindObjectOfType<ClothManager>();
        clothManager.Init();
    }

    private void DeInits()
    {
        levelManager.DeInit();
        uIManager.DeInit();
        updateManager.DeInit();
        playerManager.DeInit();
        camManager.DeInit();
        bandRotator.DeInit();
        machineManager.DeInit();
        dyeMachineManager.DeInit();
        threadManager.DeInit();
        clothManager.DeInit();
    }

    public void OnStartTheGame()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.Init();
        camManager.Init();
    }

    public void OnLevelSucceed()
    {
        levelManager.LevelUp();
        DeInits();
        SetInits();
    }

    public void OnLevelFailed()
    {
        DeInits();
        SetInits();
    }

    public void FinishTheGame(bool check)
    {
        playerManager.DeInit();
        uIManager.DeInit();

        if (check)
        {
            ActionManager.GameEnd?.Invoke(true);
            return;
        }
        ActionManager.GameEnd?.Invoke(false);
    }
}
