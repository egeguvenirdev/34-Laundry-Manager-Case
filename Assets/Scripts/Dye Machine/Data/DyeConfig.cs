using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeConfig : MonoBehaviour
{
    public readonly float ProduceDuration;
    public readonly int UnlockLevel;
    public readonly float MoneyValue;

    public DyeConfig(float produceDuration, int unlockLevel, float money)
    {
        ProduceDuration = produceDuration;
        UnlockLevel = unlockLevel;
        MoneyValue = money;
    }
}
