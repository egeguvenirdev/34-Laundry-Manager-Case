using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private List<ButtonBase> panels = new List<ButtonBase>();

    [Header("Level & Money")]
    [SerializeField] private TMP_Text currentLV;
    [SerializeField] private TMP_Text totalMoneyText;

    [Header("Health & Xp Bars")]
    [SerializeField] private Image progressBarImage;

    private LevelManager levelManager;

    private float smoothMoneyNumbers = 0;
    private Tweener smoothTween;

    public void Init()
    {
        levelManager = LevelManager.Instance;

        LevelText();

        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].Init();
        }
    }

    public void DeInit()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            //panels[i].DeInit();
        }
    }

    public void LevelText()
    {
        int levelInt = levelManager.LevelIndex + 1;
        currentLV.text = "Level " + levelInt;
    }

    public void SetProgress(float progress)
    {
        progressBarImage.fillAmount = progress;
    }

    #region Money
    public void SetMoneyUI(float totalMoney, bool setSmoothly)
    {
        //totalMoneyText.text = money;

        if (setSmoothly)
        {
            smoothTween.Kill();
            smoothTween = DOTween.To(() => smoothMoneyNumbers, x => smoothMoneyNumbers = x, totalMoney, 0.5f).SetSpeedBased(false).OnUpdate(() => { UpdateMoneyText(); });
        }
        else
        {
            smoothTween.Kill();
            smoothMoneyNumbers = totalMoney;
            UpdateMoneyText();
        }
    }

    private void UpdateMoneyText()
    {
        totalMoneyText.text = FormatFloatToReadableString(smoothMoneyNumbers);
    }
    #endregion

    public string FormatFloatToReadableString(float value)
    {
        float number = value;
        if (number < 1000)
        {
            return ((int)number).ToString();
        }
        string result = number.ToString();

        if (result.Contains(","))
        {
            result = result.Substring(0, 4);
            result = result.Replace(",", string.Empty);
        }
        else
        {
            result = result.Substring(0, 3);
        }

        do
        {
            number /= 1000;
        }
        while (number >= 1000);
        number = Mathf.CeilToInt(number);
        if (value >= 1000000000000000)
        {
            result = result + "Q";
        }
        else if (value >= 1000000000000)
        {
            result = result + "T";
        }
        else if (value >= 1000000000)
        {
            result = result + "B";
        }
        else if (value >= 1000000)
        {
            result = result + "M";
        }
        else if (value >= 1000)
        {
            result = result + "K";
        }

        if (((int)number).ToString().Length > 0 && ((int)number).ToString().Length < 3)
        {
            result = result.Insert(((int)number).ToString().Length, ".");
        }
        return result;
    }
}
