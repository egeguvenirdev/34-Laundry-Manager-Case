using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonBase : MonoBehaviour
{
    public abstract void Init();

    public abstract void DeInit();

    public abstract void OnButtonClick();
}
