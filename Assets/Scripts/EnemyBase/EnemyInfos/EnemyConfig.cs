using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct EnemyConfig
{
    public readonly float ProduceDuration;
    public readonly float PaintDuration;
    public readonly float MoneyValue;

    public EnemyConfig(float produceDuration, float paintDuration, float money)
    {
        ProduceDuration = produceDuration;
        PaintDuration = paintDuration;
        MoneyValue = money;
    }
}
