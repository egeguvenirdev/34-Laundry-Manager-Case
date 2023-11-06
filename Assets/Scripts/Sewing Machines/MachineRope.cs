using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MachineRope : MonoBehaviour
{
    [SerializeField] private GameObject[] ropes;
    [SerializeField] private Transform needle;
    [SerializeField] private float needleY;
    [SerializeField] private int needleSpeed;
    private float duration;

    private Tween needleTween;
    private Tween rotatorTween;

    public void Init(float refDuration)
    {
        duration = refDuration / ropes.Length;

        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i].transform.localScale = Vector3.one;
            ropes[i].SetActive(true);
        }

        StartCoroutine(ProduceCo(refDuration));
    }

    private IEnumerator ProduceCo(float refDuration)
    {
        needleTween = transform.DOLocalRotate(Vector3.up * 360, duration, RotateMode.FastBeyond360).SetLoops((int)refDuration, LoopType.Restart);
        rotatorTween = needle.DOLocalMoveY(needleY, duration / needleSpeed).SetLoops((int)refDuration * needleSpeed, LoopType.Yoyo);

        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i].transform.DOScale(Vector3.zero, duration);
            yield return new WaitForSeconds(duration);
        }
    }

    public void StopAnims()
    {
        needleTween.Kill();
        rotatorTween.Kill();
        StopAllCoroutines();
    }
}
