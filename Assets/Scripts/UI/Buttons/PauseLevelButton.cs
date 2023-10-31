using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLevelButton : ButtonBase
{
    public override void Init()
    {

    }

    public override void DeInit()
    {

    }

    public override void OnButtonClick()
    {
        gameObject.SetActive(false);
    }
}
