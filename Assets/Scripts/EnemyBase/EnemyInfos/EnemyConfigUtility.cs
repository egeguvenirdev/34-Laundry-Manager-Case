using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class EnemyConfigUtility
{
    //Dictionary
    private static readonly Dictionary<byte, EnemyConfig> enemyConfigByLevel = new Dictionary<byte, EnemyConfig>()
    {
        {
            1,
            new EnemyConfig(3, 35, 500)
        },
        {
            2,
            new EnemyConfig(4, 45, 1000)
        }
    };

    private static readonly EnemyConfig defaultConfig = new EnemyConfig(3, 35, 500);

    public static EnemyConfig GetEnemyConfigByLevel(byte level)
    {
        if (enemyConfigByLevel.TryGetValue(level, out var enemyConfig)) return enemyConfig;
        Debug.LogWarning("There is no config by given enemy level, default config returned!");
        return defaultConfig;
    }
}
