using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyText : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TextMeshPro tmpPro;
    [SerializeField] private Color32 colorFade;

    private void Start()
    {
        DOTween.Init();
    }

    public void SetTheText(int value)
    {
        colorFade = Color.green;
        text.text = value + "$";
        MovementAndColor();
        Invoke("KillObject", lifeTime);
    }

    private void MovementAndColor()
    {

        transform.DOLocalMoveY(transform.localPosition.y + 1, lifeTime).SetEase(Ease.Linear);
        DOTween.ToAlpha(() => colorFade, x => colorFade = x, 0, lifeTime).OnUpdate(() =>
        {
            tmpPro.color = colorFade;
        });
    }

    private void KillObject()
    {
        gameObject.SetActive(false);
    }
}