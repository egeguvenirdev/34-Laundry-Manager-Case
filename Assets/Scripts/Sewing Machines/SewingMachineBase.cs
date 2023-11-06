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
    [SerializeField] protected Transform producePos;
    [SerializeField] private ParticleSystem producedParticle;
    private Color white = Color.white;
    private Color green = Color.green;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip finishAudio;

    //Machine Props
    protected float produceDuration;
    protected float unlockLevel;
    protected float moneyValue;

    private bool buyable = false;
    private bool canProduce = false;
    private bool producedClothes = false;

    protected ObjectPooler pooler;
    private ClothesBase produceCloth;
    private VibrationManager vibration;

    private Tween tween;

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
        vibration = VibrationManager.Instance;
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
            else
            {
                UnlockTheMachine();
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
            if (PlayerPrefs.GetInt(ConstantVariables.BuyCheck.UnlockCheck + clothType.ToString(), 0) == 0)
            {
                return false;
            }
            return true;
        }

        set
        {
            if (value) PlayerPrefs.SetInt(ConstantVariables.BuyCheck.UnlockCheck + clothType.ToString(), 1);
        }
    }

    protected void UnlockTheMachine()
    {
        CanProduce = true;
        buyable = false;
        if (moneyUI != null) unlockParticle.Play();
        if (moneyUI != null) moneyUI.SetActive(false);
        UnlockCheck = true;
        machineSymbol.SetActive(true);
    }

    #endregion

    #region Produce
    public IEnumerator ProduceClothes(float delay)
    {
        CanProduce = false;
        yield return new WaitForSeconds(delay);
        StartProduce();
        machineRope.Init(produceDuration);
        yield return new WaitForSeconds(produceDuration);
        producedClothes = true;
        ActionManager.PlayAudio?.Invoke(finishAudio);
        PlayClothAnim();
        PlayProduceParticle();
        yield return new WaitForSeconds(delay);
        machineRope.StopAnims();
        vibration.LightVibration();
    }

    protected void StartProduce()
    {
        produceCloth = pooler.GetPooledClothes(GetClothType);
        produceCloth.gameObject.SetActive(true);
        produceCloth.Init(producePos.position, produceDuration);
    }

    private void PlayClothAnim()
    {
        TurnToGreen();
    }

    private void PlayProduceParticle()
    {
        producedParticle.Play();
    }

    private void TurnToGreen()
    {
        Material spriteMat = machineSymbolBorder.material;
        tween = spriteMat.DOColor(white, 0.5f).OnComplete( () => { TurnToWhite(); } );
    }

    private void TurnToWhite()
    {
        Material spriteMat = machineSymbolBorder.material;
        tween = spriteMat.DOColor(green, 0.5f).OnComplete(() => { TurnToGreen(); });
    }

    public void GetClothes()
    {
        CanProduce = true;
        tween.Kill();
        Material spriteMat = machineSymbolBorder.material;
        spriteMat.DOColor(white, 0);
        produceCloth.MoveToUITarget();
        produceCloth = null;
        producedClothes = false;
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
        var machineProps = MachineUtility.GetMachineConfigByType(GetClothType);
        produceDuration = machineProps.ProduceDuration;
        unlockLevel = machineProps.UnlockLevel;
        moneyValue = machineProps.MoneyValue;

        levelText.text = "Level" + unlockLevel;
        moneyText.text = moneyValue + "$";
    }
}