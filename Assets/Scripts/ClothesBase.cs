using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothesBase : MonoBehaviour
{
    [Header("Clothes Settings")]
    [SerializeField] private MeshRenderer wobbleRenderer;
    [SerializeField] private float clothMoneyValue;
    private Material wobbleMat;

    [Header("Produce Settings")]
    [SerializeField] private ClothType clothType;
    [SerializeField] private float startValue;
    [SerializeField] private float endValue;
    [SerializeField] private float placementDuration = 1f;

    [Header("Dye Settings")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform model;
    [SerializeField] private Collider col;

    private bool selected;
    private bool painted;

    private StageSwapperButton swapButton;
    private VibrationManager vibration;
    private Camera cam;
    private float currentValue;

    private ColorType colorType;

    public ClothType GetClothesType
    {
        get => clothType;
    }

    public ColorType ClothesColorType
    {
        get => colorType;
        set => colorType = value;
    }

    public void Init(Vector3 instantiatePos, float produceDuration)
    {
        ActionManager.ClearClothSelection += OnClearRopeSelection;

        cam = Camera.main;
        vibration = VibrationManager.Instance;

        swapButton = FindObjectOfType<StageSwapperButton>();
        colorType = ColorType.nullColor;
        wobbleMat = wobbleRenderer.material;
        wobbleMat.SetFloat("_Fill", 0);

        transform.position = instantiatePos;
        transform.parent = null;
        painted = false;

        StartProducing(produceDuration);
    }

    public void DeInit()
    {
        ActionManager.ClearClothSelection -= OnClearRopeSelection;
        sprite.gameObject.SetActive(false);
        sprite.color = Color.white;
        gameObject.SetActive(false);
    }

    private void StartProducing(float duration)
    {
        currentValue = startValue;
        DOTween.To(() => currentValue, x => currentValue = x, endValue, duration)
            .OnUpdate(() =>
            {
                wobbleMat.SetFloat("_Fill", currentValue);
            });
    }

    private void OnMouseDown()
    {
        if (painted)
        {
            ActionManager.SellTheClothes?.Invoke(this);
            painted = false;
            return;
        }

        if (selected)
        {
            OnClearRopeSelection();
            return;
        }

        ActionManager.ClearClothSelection?.Invoke();
        ActionManager.ClothSelection?.Invoke(this);
        SelectTheThread();
    }

    private void SelectTheThread()
    {
        selected = true;
        sprite.color = Color.green;
        vibration.SoftVibration();
        PlayDoPunch(model);
    }

    private void OnClearRopeSelection()
    {
        selected = false;
        sprite.color = Color.white;
        vibration.SoftVibration();
    }

    public float StartDyeProcess(Transform target, Color targetColor, float duration)
    {
        StartCoroutine(DyeCo(target, targetColor, duration));
        return placementDuration;
    }

    public void MoveToUITarget()
    {
        Vector3 target = swapButton.transform.position;
        target.z = (transform.position - cam.transform.position).z;
        Vector3 result = cam.ScreenToWorldPoint(target);

        transform.DOMove(result, 0.75f).OnComplete(() => 
        {
            ActionManager.GainClothes?.Invoke(this);
            sprite.gameObject.SetActive(true);
            col.enabled = true;
        });
    }

    private IEnumerator DyeCo(Transform target, Color targetColor, float duration)
    {
        transform.DOMove(target.position, placementDuration);
        sprite.gameObject.SetActive(false);
        col.enabled = false;
        yield return new WaitForSeconds(placementDuration);
        transform.parent = target;

        Color currentColor = wobbleMat.GetColor("_SideColor");
        DOVirtual.Color(currentColor, targetColor, duration, (value) =>
        {
            wobbleMat.SetColor("_SideColor", value);
            wobbleMat.SetColor("_TopColor", value);
        });

        yield return new WaitForSeconds(duration);

        //DeInit();
    }

    public void SellTheClothes(Vector3 target)
    {
        target.z = (transform.position - cam.transform.position).z;
        Vector3 result = cam.ScreenToWorldPoint(target);

        transform.DOMove(result, 0.75f).OnComplete(() =>
        {
            ActionManager.GainClothes?.Invoke(this);
            sprite.gameObject.SetActive(true);
            col.enabled = true;
        });
    }

    private void PlayDoPunch(Transform refObject)
    {
        refObject.DOScale(new Vector3(1f, 1f, 1f), 0f);
        refObject.DOPunchScale(Vector3.one * 0.15f, 0.5f, 10, 1);
    }
}
