using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesUI : MonoBehaviour
{
    [SerializeField] private Image clothesImage;

    public void Init(Color refColor, Sprite refImage)
    {
        clothesImage.sprite = refImage;
        clothesImage.color = refColor;
    }
}