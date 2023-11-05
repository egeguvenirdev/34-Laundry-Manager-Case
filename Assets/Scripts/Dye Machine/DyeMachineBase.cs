using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DyeMachineBase : MonoBehaviour
{
    [Header("Components")]
    private Collider col;

    [Header("Properties")]
    [SerializeField] private ColorType colorType;
    [SerializeField] private Transform clothTargetPos;

    [Header("Lock Interface")]
    [SerializeField] private GameObject lockUI;
    [SerializeField] private TMP_Text levelText;

    [Header("Buy Interface")]
    [SerializeField] private GameObject moneyUI;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private ParticleSystem unlockParticle;

    [Header("Produce Settings")]
    [SerializeField] private Image cooldownImage;
    [SerializeField] protected Transform producePos;
    [SerializeField] private ParticleSystem producedParticle;

    //Machine Props
    protected float produceDuration;
    protected float unlockLevel;
    protected float moneyValue;
    protected float cooldown;

    private bool buyable = false;
    private bool canProduce = false;
    private bool producedClothes = false;

    protected ObjectPooler pooler;
    private MoneyManager moneyManager;

    public bool CanProduce
    {
        get => canProduce;
        private set => canProduce = value;
    }

    public ColorType GetColorType
    {
        get => colorType;
    }

    public Vector3 GetThreadTransform
    {
        get => clothTargetPos.position;
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
            ActionManager.GetSelectedCloth?.Invoke(this);
            ActionManager.ClearClothSelection?.Invoke();
            return;
        }
    }

    #region Unlock Check
    protected bool UnlockCheck
    {
        get
        {
            if (PlayerPrefs.GetInt(ConstantVariables.BuyCheck.UnlockCheck + colorType.ToString(), 0) == 0)
            {
                return false;
            }
            return true;
        }

        set
        {
            if (value) PlayerPrefs.GetInt(ConstantVariables.BuyCheck.UnlockCheck + colorType.ToString(), 1);
        }
    }

    protected void UnlockTheMachine()
    {
        CanProduce = true;
        //unlockParticle.Play();
        if (moneyUI != null) moneyUI.SetActive(false);
        UnlockCheck = true;
    }

    #endregion

    #region Produce
    public IEnumerator ProduceClothes(float delay)
    {
        CanProduce = false;
        yield return new WaitForSeconds(delay);
        StartDye();
        yield return new WaitForSeconds(produceDuration);
        producedClothes = true;
        PlayClothAnim();
        PlayProduceParticle();
    }

    protected void StartDye()
    {
        ClothesBase produceCloth = pooler.GetPooledClothes(GetClothType);
        produceCloth.gameObject.SetActive(true);
        produceCloth.Init(producePos.position, produceDuration);
    }

    private void PlayClothAnim()
    {
        DOTween.To(() => cooldown, x => cooldown = x, produceDuration, 0.5f).SetSpeedBased(false)
            .OnUpdate(() => { cooldownImage.fillAmount = cooldown / produceDuration; });
    }

    private void PlayProduceParticle()
    {
        producedParticle.Play();
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
        var machineProps = DyeUthility.GetDyeConfigByType(GetColorType);
        produceDuration = machineProps.ProduceDuration;
        unlockLevel = machineProps.UnlockLevel;
        moneyValue = machineProps.MoneyValue;

        levelText.text = "Level" + unlockLevel;
        moneyText.text = moneyValue + "$";
    }
}
