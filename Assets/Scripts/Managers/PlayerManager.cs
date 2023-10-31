using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private MoneyManager moneyManager;

    Sequence sequence;

    public void Init()
    {
        gameManager = GameManager.Instance;
        moneyManager = MoneyManager.Instance;
    }

    public void DeInit()
    {

    }

    #region Upgrade

    private void IncomeUpgrade(float value)
    {
        if (value < 1)
        {
            ActionManager.UpdateMoneyMultiplier?.Invoke(1);
            return;
        }
        ActionManager.UpdateMoneyMultiplier?.Invoke(value);
    }

    private void FireRangeUpgrade(float value)
    {

    }

    private void FireRateUpgrade(float value)
    {

    }

    #endregion
}
