using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPropsManager : MonoBehaviour
{
    [SerializeField] private LevelProps level;
    private ClothesUIManager clothesManager;

    public void SetLevelPropsUI()
    {
        clothesManager = FindObjectOfType<ClothesUIManager>();

        for (int i = 0; i < level.GetLevelPrefs.Length; i++)
        {
            clothesManager.InstantiateClothesUI(level.GetLevelPrefs[i].colorType, level.GetLevelPrefs[i].image);
        }
    }
}
