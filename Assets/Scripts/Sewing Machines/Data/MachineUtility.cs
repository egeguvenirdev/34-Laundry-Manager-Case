using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class MachineUtility
{
    //Dictionary
    private static readonly Dictionary<ClothType, MachineConfig> machineConfigByType = new Dictionary<ClothType, MachineConfig>()
    {
        {
            ClothType.Sock,
            new MachineConfig(10f, 1, 100f)
        },
        {
            ClothType.Bra,
            new MachineConfig(15f, 3, 150f)
        },
        {
            ClothType.Slip,
            new MachineConfig(25f, 5, 250f)
        },
        {
            ClothType.Short,
            new MachineConfig(40f, 10, 500f)
        }
    };

    private static readonly MachineConfig defaultConfig = new MachineConfig(10f, 1, 100f);

    public static MachineConfig GetEnemyConfigByType(ClothType refType)
    {
        if (machineConfigByType.TryGetValue(refType, out var enemyConfig)) return enemyConfig;
        Debug.LogWarning("There is no config by given enemy level, default config returned!");
        return defaultConfig;
    }
}
