using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public abstract class SewingMachineBase : MonoBehaviour
{
    [Header("Components")]
    private Collider col;
    [SerializeField] public MachineRope machineRope;

    [Header("Properties")]
    [SerializeField] private ClothType clothType;
    [SerializeField] private Transform threadTargetPos;
    [SerializeField] private GameObject machineSymbol;

    [Header("Lock Interface")]
    [SerializeField] private GameObject lockUI;
    [SerializeField] private TMP_Text levelText;

    [Header("Buy Interface")]
    [SerializeField] private GameObject moneyUI;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private ParticleSystem unlockParticle;

    [Header("Produce Settings")]
    [SerializeField] private SpriteRenderer machineSymbolBorder;
    [SerializeField] private Transform producePos;
    private Color white = Color.white;
    private Color green = Color.green;

    //Machine Props
    protected float produceDuration;
    protected float unlockLevel;
    protected float moneyValue;

    private bool buyable = false;
    private bool canProduce = false;
    private bool producedClothes = false;

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

    public Vector3 GetThreadTransform
    {
        get => threadTargetPos.position;
    }

    public void Init(int currentLevel)
    {
        col = GetComponent<Collider>();
        pooler = ObjectPooler.Instance;
        SetProperties();

        if (currentLevel >= unlockLevel)
        {
            lockUI.SetActive(false);
            if (!UnlockCheck)
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

    protected void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (buyable && !UnlockCheck)
        {
            if (ActionManager.CheckMoneyAmount(moneyValue))
            {
                UnlockTheMachine();
            }
            return;
        }

        if (producedClothes)
        {
            GetClothes();
        }

        if (canProduce)
        {
            ActionManager.GetSelectedThread?.Invoke(this);
            ActionManager.ClearThreadSelection?.Invoke();
            return;
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
        //unlockParticle.Play();
        if (moneyUI != null) moneyUI.SetActive(false);
        UnlockCheck = true;
        machineSymbol.SetActive(true);
    }

    #endregion

    #region Produce
    public IEnumerator ProduceClothes(float delay)
    {
        CanProduce = false;
        StartProduce();
        yield return new WaitForSeconds(delay);
        machineRope.Init(produceDuration);
        yield return new WaitForSeconds(produceDuration);
        producedClothes = true;
        PlayClothAnim();
    }

    protected abstract void StartProduce();

    private void PlayClothAnim()
    {
        Material spriteMat = machineSymbolBorder.material;
        TurnToGreen(spriteMat);
    }

    private void TurnToGreen(Material refMat)
    {
        refMat.DOColor(white, 0.5f).OnComplete( () => { TurnToWhite(refMat); } );
    }

    private void TurnToWhite(Material refMat)
    {
        refMat.DOColor(green, 0.5f).OnComplete(() => { TurnToGreen(refMat); });
    }

    public void GetClothes()
    {
        CanProduce = true;
        DOTween.KillAll();
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

        levelText.text = "Level" + unlockLevel;
        moneyText.text = moneyValue + "$";
    }
}