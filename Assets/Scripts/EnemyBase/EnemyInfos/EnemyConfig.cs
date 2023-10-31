using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct EnemyConfig
{
    public readonly float Size;
    public readonly float Power;
    public readonly int Money;

    public EnemyConfig(float size, float power, int money)
    {
        Size = size;
        Power = power;
        Money = money;
    }
}
