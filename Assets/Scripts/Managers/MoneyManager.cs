using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    [Header("Money Settings")]
    [SerializeField] private int addMoney = 0;
    private float moneyMultiplier = 1;

    public float MoneyMultipler
    {
        get => moneyMultiplier;
        private set => moneyMultiplier = value;
    }

    public float Money
    {
        get => PlayerPrefs.GetInt(ConstantVariables.TotalMoneyValue.TotalMoney, 0);
        private set
        {
            float calculatedMoney = value;
            if (value > 0)
            {
                calculatedMoney = value * moneyMultiplier;
            }
            PlayerPrefs.SetFloat(ConstantVariables.TotalMoneyValue.TotalMoney, PlayerPrefs.GetInt(ConstantVariables.TotalMoneyValue.TotalMoney, 0) + calculatedMoney);
            UIManager.Instance.SetMoneyUI(Money, true);
        }
    }

    public void Init(bool clearPlayerPrefs)
    {
        ActionManager.UpdateMoney += OnUpdataMoney;
        ActionManager.UpdateMoneyMultiplier += OnUpdataMoneyMultiplier;
        ActionManager.CheckMoneyAmount += OnCheckMoneyAmount;

        if (clearPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            Money = addMoney;
        }

        if (Money <= 0) Money = addMoney;
        Money = 0;
    }

    public void DeInit()
    {
        ActionManager.UpdateMoney -= OnUpdataMoney;
        ActionManager.UpdateMoneyMultiplier -= OnUpdataMoneyMultiplier;
        ActionManager.CheckMoneyAmount -= OnCheckMoneyAmount;
    }

    private void OnUpdataMoney(float value)
    {
        Money = value;
    }

    private void OnUpdataMoneyMultiplier(float value)
    {
        MoneyMultipler = value;
    }

    private bool OnCheckMoneyAmount(float value)
    {
        if (value <= Money) return true;
        return false;
    }
}
