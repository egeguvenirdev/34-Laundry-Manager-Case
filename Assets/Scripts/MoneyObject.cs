using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyObject : MonoBehaviour
{
    public void GoToUi(float money)
    {
        Vector3 target = UIManager.Instance.GetMoneyUiTransform.position;
        target.z = (transform.position - Camera.main.transform.position).z;
        Vector3 result = Camera.main.ScreenToWorldPoint(target);
        Debug.Log(" " +result);
        transform.DOMove(result, 0.75f).OnComplete(() =>
        {
            ActionManager.UpdateMoney?.Invoke(money);
            gameObject.SetActive(false);
        });
    }
}
