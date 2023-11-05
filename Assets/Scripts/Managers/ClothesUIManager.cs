using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesUIManager : MonoBehaviour
{
    [Header("Clothes UI Props")]
    [SerializeField] private GameObject uiBase;
    private List<ClothesUI> uis = new List<ClothesUI>();

    [Header("Colors")]
    [SerializeField] private Color blue1;
    [SerializeField] private Color blue2;
    [SerializeField] private Color green1;
    [SerializeField] private Color green2;

    public void DeInit()
    {
        for (int i = 0; i < uis.Count; i++)
        {
            Destroy(uis[i].gameObject);
        }
        uis.Clear();
    }

    public void InstantiateClothesUI(ColorType uiColor, Sprite uiImage)
    {
        ClothesUI newUI = Instantiate(uiBase).GetComponent<ClothesUI>();
        newUI.transform.parent = transform;
        newUI.transform.localScale = Vector3.one;
        newUI.Init(GetColor(uiColor), uiImage);
        uis.Add(newUI);
    }

    private Color GetColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.Blue1:
                return blue1;
            case ColorType.Blue2:
                return blue2;
            case ColorType.Green1:
                return green1;
            case ColorType.Green2:
                return green2;

            default:
                Debug.Log("NOTHING");
                return blue1;
        }
    }
}
