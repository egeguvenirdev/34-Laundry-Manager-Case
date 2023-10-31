using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class EnemyConfigUtility
{
    //Dictionary
    private static readonly Dictionary<ClothType, EnemyConfig> enemyConfigByLevel = new Dictionary<ClothType, EnemyConfig>()
    {
        {
            ClothType.Sock,
            new EnemyConfig(10f, 5f, 100f)
        },
        {
            ClothType.Tshirt,
            new EnemyConfig(15f, 7.5f, 500f)
        }
    };

    private static readonly EnemyConfig defaultConfig = new EnemyConfig(10f, 5f, 100f);

    public static EnemyConfig GetEnemyConfigByLevel(ClothType refType)
    {
        if (enemyConfigByLevel.TryGetValue(refType, out var enemyConfig)) return enemyConfig;
        Debug.LogWarning("There is no config by given enemy level, default config returned!");
        return defaultConfig;
    }
}
