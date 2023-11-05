using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothesBase : MonoBehaviour
{
    [Header("Clothes Settings")]
    [SerializeField] private MeshRenderer wobbleRenderer;
    [SerializeField] private float clothMoneyValue;
    private Material wobbleMat;

    [Header("Produce Settings")]
    [SerializeField] private ClothType clothType;
    [SerializeField] private float startValue;
    [SerializeField] private float endValue;
    [SerializeField] private float placementDuration = 1f;

    private float currentValue;

    private ColorType colorType;

    public ClothType GetClothesType
    {
        get => clothType;
    }

    public ColorType ClothesColorType
    {
        get => colorType;
        set => colorType = value;
    }

    public void Init(Vector3 instantiatePos, float produceDuration)
    {
        colorType = ColorType.nullColor;
        wobbleMat = wobbleRenderer.material;
        wobbleMat.SetFloat("_Fill", 0);
        transform.position = instantiatePos;
        StartProducing(produceDuration);
    }

    public void DeInit()
    {

    }

    private void StartProducing(float duration)
    {
        currentValue = startValue;
        DOTween.To(() => currentValue, x => currentValue = x, endValue, duration)
            .OnUpdate(() =>
            {
                wobbleMat.SetFloat("_Fill", currentValue);
            });
    }

    public float MoveToTarget(Vector3 target)
    {
        StartCoroutine(MoveCo(target));
        return placementDuration;
    }

    private IEnumerator MoveCo(Vector3 target)
    {
        transform.DOMove(target, placementDuration);
        yield return new WaitForSeconds(placementDuration);
        DeInit();
    }
}
