using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothesRotator : MonoBehaviour
{
    [SerializeField] private int rotateCount = 5;

    public void Init(float refDuration)
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.DOLocalRotate(Vector3.up * 360, 1, RotateMode.FastBeyond360).SetLoops((int)refDuration, LoopType.Restart);
    }
}
