using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewingMachineBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public Collider col;
    [SerializeField] public MachineRope machineRope;

    [Header("Properties")]
    [SerializeField] private ClothType clothType;

    [Header("Buy Interface")]
    [SerializeField] private GameObject lockUI;
    [SerializeField] private GameObject moneyUI;
    [SerializeField] private GameObject machineSymbol;
    [SerializeField] private ParticleSystem unlockParticle;

    //Machine Props
    protected float produceDuration;
    protected float unlockLevel;
    protected float moneyValue;

    private bool buyable = false;
    private bool canProduce = false;

    private ObjectPooler pooler;
    private MoneyManager moneyManager;

    public bool CanProduce
    {
        get => canProduce;
        private set => canProduce = value;
    }

    public ClothType GetClothType
    {
        get => clothType;
    }

    public void Init(int currentLevel)
    {
        pooler = ObjectPooler.Instance;
        SetProperties();

        if(currentLevel >= unlockLevel)
        {
            lockUI.SetActive(false);
            if (!UnlockCheck ) 
            {
                if (moneyUI != null)
                {
                    moneyUI.SetActive(true);
                    buyable = true;
                }
                else
                {
                    UnlockTheMachine();
                }
            }
        }
    }

    public void DeInit()
    {

    }

    private void OnMouseDown()
    {
        if (buyable && !UnlockCheck)
        {
            if(ActionManager.CheckMoneyAmount(moneyValue))
            {
                UnlockTheMachine();
            }
        }
    }

    #region Unlock Check
    protected bool UnlockCheck
    {
        get
        {
            if (PlayerPrefs.GetInt(ConstantVariables.BuyCheck.UnlockCheck, 0) == 0)
            {
                return false;
            }
            return true;
        }

        set
        {
            if (value) PlayerPrefs.GetInt(ConstantVariables.BuyCheck.UnlockCheck, 1);
        }
    }

    protected void UnlockTheMachine()
    {
        CanProduce = true;
        unlockParticle.Play();
        moneyUI.SetActive(false);
        UnlockCheck = true;
        machineSymbol.SetActive(true);
    }

    #endregion

    #region Produce
    public IEnumerator ProduceClothes()
    {
        machineRope.Init(produceDuration);
        yield return new WaitForSeconds(produceDuration);
        PlayClothAnim();
    }

    private void PlayClothAnim()
    {

    }
    #endregion

    protected void PlayMoneyText()
    {
        var text = ObjectPooler.Instance.GetPooledText();
        text.gameObject.SetActive(true);
        text.SetTheText(moneyValue, Color.green, null, transform.position + Vector3.back);
    }

    protected void SetProperties()
    {
        var machineProps = MachineUtility.GetEnemyConfigByType(GetClothType);
        produceDuration = machineProps.ProduceDuration;
        unlockLevel = machineProps.UnlockLevel;
        moneyValue = machineProps.MoneyValue;
    }
}