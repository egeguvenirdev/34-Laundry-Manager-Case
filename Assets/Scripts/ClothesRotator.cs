using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothesRotator : MonoBehaviour
{
    [SerializeField] private int rotateCount = 5;

    public void Init(float refDuration)
    {
        transform.DOLocalRotate(Vector3.up * 360, refDuration / rotateCount, RotateMode.FastBeyond360).SetLoops(rotateCount, LoopType.Restart);
    }
}
