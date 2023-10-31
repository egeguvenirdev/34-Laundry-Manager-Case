using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothBase : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public ClothType clothType;

    protected float produceDuration;
    protected float paintDuration;
    protected float moneyValue;

    protected void Init()
    {
        SetProperties();
    }

    protected void DeInit()
    {

    }

    protected virtual void OnDrag()
    {

    }

    protected void PlayDamageText(float hitAmount)
    {
        if (hitAmount <= 0) return;
        var text = ObjectPooler.Instance.GetPooledObjectWithTag("DamageText");
        text.transform.position = transform.position;
        text.GetComponent<MoneyText>().SetTheText((int)hitAmount);
        text.SetActive(true);
    }

    private void SetProperties()
    {
        var enemyConfig = EnemyConfigUtility.GetEnemyConfigByLevel(clothType);
        produceDuration = enemyConfig.ProduceDuration;
        paintDuration = enemyConfig.PaintDuration;
        moneyValue = enemyConfig.MoneyValue;
    }
}