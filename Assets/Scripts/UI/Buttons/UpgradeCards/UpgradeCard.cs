using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public abstract class UpgradeCard : ButtonBase
{
    [Header("Button Settings")]
    [SerializeField] protected UpgradeType upgradeType;
    [SerializeField] protected Button button;
    [SerializeField] protected Color32 white = Color.white;
    [SerializeField] protected Color32 red = Color.red;

    [Header("Button Prices")]
    [SerializeField] protected int startPrice = 100;
    [SerializeField] protected int incrementalBasePrice = 10;
    protected int priceIncrementalValue;

    [Header("Button Upgrade Values")]
    [SerializeField] protected float upgradeValue = 1f;
    [SerializeField] protected float upgradeBaseIncrementalValue = 0.1f;
    protected float upgradeIncrementalValue;

    [Header("Panel Settings")]
    [SerializeField] protected TMP_Text levelText;
    [SerializeField] protected TMP_Text priceText;

    private UIManager uiManager;
    private GameManager gameManager;
    private PlayerManager playerManager;
    private VibrationManager vibrationManager;

    public override void Init()
    {
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
        vibrationManager = VibrationManager.Instance;
        playerManager = FindObjectOfType<PlayerManager>();

        SetButtonText();
        SetButtonApperence();
        ApplyUpgrades();
    }

    public override void DeInit()
    {

    }

    public void OnPurchase()
    {
        SetButtonApperence();
        SetButtonText();
    }

    public override void OnButtonClick()
    {
        vibrationManager.SoftVibration();

        playerManager.OnUpgrade(upgradeType, UpgradeCurrentValue);
        ActionManager.UpdateMoney?.Invoke(-CurrentPrice);
        SkillLevel = 1;

        transform.DOKill(true);
        transform.DOScale(Vector3.one, 0);
        button.enabled = false;
        SetButtons();
        uiManager.UpgradeButtons();
        transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 6).SetUpdate(true);
    }

    public void SetButtons()
    {
        SetButtonPrice();
        SetUpgrades();
    }

    protected void SetButtonPrice()
    {
        //CurrentPrice = startPrice + incrementalBasePrice * (SkillLevel - 1);
        CurrentPrice = startPrice + IncrementalPrice;
        IncrementalPrice = IncrementalPrice;
    }

    protected void SetUpgrades()
    {
        UpgradeIncrementalValue = upgradeBaseIncrementalValue * (SkillLevel - 1);
        UpgradeCurrentValue = upgradeValue + UpgradeIncrementalValue;
    }

    protected void SetButtonApperence()
    {
        if (gameManager == null) gameManager = GameManager.Instance;

        if (ActionManager.CheckMoneyAmount(CurrentPrice))
        {
            button.enabled = false;
            priceText.color = red;
        }
        else
        {
            button.enabled = true;
            priceText.color = white;
        }
    }

    protected void SetButtonText()
    {
        if (uiManager == null) uiManager = UIManager.Instance;
        levelText.text = SkillLevel + ConstantVariables.LevelStats.Lv;
        priceText.text = "" + uiManager.FormatFloatToReadableString(CurrentPrice);
    }

    protected void ApplyUpgrades()
    {
        playerManager.OnUpgrade(upgradeType, UpgradeCurrentValue);
    }

    protected int SkillLevel
    {
        get => PlayerPrefs.GetInt(upgradeType.ToString() + ConstantVariables.LevelStats.SkillLevel, 1);
        set => PlayerPrefs.SetInt(upgradeType.ToString() + ConstantVariables.LevelStats.SkillLevel, PlayerPrefs.GetInt(upgradeType.ToString()
            + ConstantVariables.LevelStats.SkillLevel, 1) + value);
    }

    protected int CurrentPrice
    {
        get => PlayerPrefs.GetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.CurrentPrice, startPrice);
        set => PlayerPrefs.SetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.CurrentPrice, value);
    }

    protected int IncrementalPrice
    {
        get => PlayerPrefs.GetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.IncrementalPrice, incrementalBasePrice);
        set => PlayerPrefs.SetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.IncrementalPrice, PlayerPrefs.GetInt(upgradeType.ToString()
            + ConstantVariables.UpgradePrices.IncrementalPrice, 0) + value);
    }

    protected float UpgradeCurrentValue
    {
        get => PlayerPrefs.GetFloat(upgradeType.ToString() + ConstantVariables.UpgradeValues.UpgradeCurrentValue, upgradeValue);
        set => PlayerPrefs.SetFloat(upgradeType.ToString() + ConstantVariables.UpgradeValues.UpgradeCurrentValue, value);
    }

    protected float UpgradeIncrementalValue
    {
        get => PlayerPrefs.GetFloat(upgradeType.ToString() + ConstantVariables.UpgradeValues.UpgradeIncrementalValue, upgradeIncrementalValue);
        set => PlayerPrefs.SetFloat(upgradeType.ToString() + ConstantVariables.UpgradeValues.UpgradeIncrementalValue, value);
    }

    protected void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.LevelStats.SkillLevel);
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.UpgradePrices.CurrentPrice);
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.UpgradePrices.IncrementalPrice);
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.UpgradeValues.UpgradeCurrentValue);
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.UpgradeValues.UpgradeIncrementalValue);
    }
}