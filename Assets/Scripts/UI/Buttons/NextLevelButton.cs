using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : ButtonBase
{
    [SerializeField] private GameObject panelElements;
    private GameManager gameManager;

    public override void Init()
    {
        ActionManager.GameEnd += OnGameEnd;
        gameManager = GameManager.Instance;
    }

    public override void DeInit()
    {
        ActionManager.GameEnd -= OnGameEnd;
    }

    private void OnGameEnd(bool check)
    {
        if (check)
        {
            panelElements.SetActive(true);
        }
    }

    public override void OnButtonClick()
    {
        gameManager.OnLevelSucceed();
        panelElements.SetActive(false);
    }
}
