using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewingMachineBase : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public ClothType clothType;

    protected float produceDuration;
    protected float paintDuration;
    protected float moneyValue;

    private ObjectPooler pooler;

    protected void Init()
    {
        SetProperties();
        pooler = ObjectPooler.Instance;
    }

    protected void DeInit()
    {

    }

    protected virtual void OnDrag()
    {

    }

    protected void PlayMoneyText()
    {
        var text = ObjectPooler.Instance.GetPooledText();
        text.gameObject.SetActive(true);
        text.SetTheText(moneyValue, Color.green, null, transform.position + Vector3.back);
    }

    private void SetProperties()
    {
        var enemyConfig = EnemyConfigUtility.GetEnemyConfigByLevel(clothType);
        produceDuration = enemyConfig.ProduceDuration;
        paintDuration = enemyConfig.PaintDuration;
        moneyValue = enemyConfig.MoneyValue;
    }
}