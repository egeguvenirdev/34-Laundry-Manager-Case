using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MachineRope : MonoBehaviour
{
    [SerializeField] private GameObject[] ropes;
    private float duration;
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
        transform.DOLocalRotate(Vector3.up * 360, duration, RotateMode.FastBeyond360).SetLoops((int)refDuration, LoopType.Restart);
        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i].transform.DOScale(Vector3.zero, duration);
            yield return new WaitForSeconds(duration);
        }
    }
}
