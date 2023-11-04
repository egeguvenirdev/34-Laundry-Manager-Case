using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thread : MonoBehaviour
{
    [Header("Selection Settings")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform model;
    [SerializeField] private Collider col;
    [SerializeField] private float placementDuration = 1f;

    private bool selected;

    private VibrationManager vibration;

    public void Init()
    {
        ActionManager.ClearThreadSelection += OnClearRopeSelection;
        vibration = VibrationManager.Instance;
        col = GetComponent<Collider>();
        sprite.gameObject.SetActive(true);
        col.enabled = true;
    }

    public void DeInit()
    {
        ActionManager.ClearThreadSelection -= OnClearRopeSelection;
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (selected)
        {
            OnClearRopeSelection();
            return;
        }

        ActionManager.ClearThreadSelection?.Invoke();
        ActionManager.ThreadSelection?.Invoke(this);
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

    public float MoveToTarget(Vector3 target)
    {
        StartCoroutine(MoveCo(target));
        return placementDuration;
    }

    private IEnumerator MoveCo(Vector3 target)
    {
        sprite.gameObject.SetActive(false);
        col.enabled = false;
        transform.DOMove(target, placementDuration);
        yield return new WaitForSeconds(placementDuration);
        DeInit();
    }

    private void PlayDoPunch(Transform refObject)
    {
        refObject.DOScale(new Vector3(1f, 1f, 1f), 0f);
        refObject.DOPunchScale(Vector3.one * 0.15f, 0.5f, 10, 1);
    }
}
