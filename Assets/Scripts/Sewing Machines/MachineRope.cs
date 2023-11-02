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

        StartCoroutine(ProduceCo());
    }

    private IEnumerator ProduceCo()
    {
        for (int i = 0; i < ropes.Length; i++)
        {
            ropes[i].transform.DOScale(Vector3.zero, duration);
            yield return new WaitForSeconds(duration);
        }
    }
}
