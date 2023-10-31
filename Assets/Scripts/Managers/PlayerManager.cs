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
    public void OnUpgrade(UpgradeType type, float value)
    {
        switch (type)
        {
            case UpgradeType.Income:
                IncomeUpgrade(value);
                break;
            case UpgradeType.FireRange:
                FireRangeUpgrade(value);
                break;
            case UpgradeType.FireRate:
                FireRateUpgrade(value);
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }
    }

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
