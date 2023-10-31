using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Income,
    FireRate,
    FireRange
}

public enum EnemyType
{
    Melee,
    Range
}

[System.Flags]
public enum DropType : int
{
    Nothing = 0x00,
    Coin = 0x01,
    Health = 0x02,
    Armor = 0x04
}

public static class Utilities
{

}