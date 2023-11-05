using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesUI : MonoBehaviour
{
    [SerializeField] private Image clothesImage;
    private ColorType uiColor;
    private ClothType clothType;

    public ClothType GetClothType
    {
        get => clothType;
    }

    public ColorType GetColorType
    {
        get => uiColor;
    }

    public void Init(Color refColor, Sprite refImage, ClothType refClothType, ColorType refUiColor)
    {
        uiColor = refUiColor;
        clothType = refClothType;
        clothesImage.sprite = refImage;
        clothesImage.color = refColor;
    }
}