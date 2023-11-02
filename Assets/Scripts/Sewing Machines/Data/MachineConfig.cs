using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct MachineConfig
{
    public readonly float ProduceDuration;
    public readonly int UnlockLevel;
    public readonly float MoneyValue;

    public MachineConfig(float produceDuration, int unlockLevel, float money)
    {
        ProduceDuration = produceDuration;
        UnlockLevel = unlockLevel;
        MoneyValue = money;
    }
}
