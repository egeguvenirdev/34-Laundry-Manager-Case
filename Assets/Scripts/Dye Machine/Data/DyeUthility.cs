using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeUthility : MonoBehaviour
{
    //Dictionary
    private static readonly Dictionary<ColorType, DyeConfig> dyeConfigByType = new Dictionary<ColorType, DyeConfig>()
    {
        {
            ColorType.Blue1,
            new DyeConfig(7.5f, 1, 100f)
        },
        {
            ColorType.Blue2,
            new DyeConfig(12f, 2, 150f)
        },
        {
            ColorType.Blue2,
            new DyeConfig(20f, 4, 250f)
        },
        {
            ColorType.Blue2,
            new DyeConfig(32f, 6, 500f)
        }
    };

    private static readonly DyeConfig defaultConfig = new DyeConfig(10f, 1, 100f);

    public static DyeConfig GetDyeConfigByType(ColorType refType)
    {
        if (dyeConfigByType.TryGetValue(refType, out var dyeConfig)) return dyeConfig;
        Debug.LogWarning("There is no config by given enemy level, default config returned!");
        return defaultConfig;
    }
}
