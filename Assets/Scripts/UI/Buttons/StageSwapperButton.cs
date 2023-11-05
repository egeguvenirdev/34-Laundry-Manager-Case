using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageSwapperButton : ButtonBase
{
    [SerializeField] private TMP_Text text;
    private bool mainStage = true;

    public override void Init()
    {
        
    }

    public override void DeInit()
    {
        
    }

    public override void OnButtonClick()
    {
        if (mainStage)
        {
            mainStage = false;
            text.text = "Sew";
            ActionManager.SewScreen.Invoke();
            return;
        }
        mainStage = true;
        text.text = "Dye";
        ActionManager.DyeScreen.Invoke();
    }
}
