using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thread : MonoBehaviour
{
    [Header("Selection Settings")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform model;

    private bool selected;

    private VibrationManager vibration;

    public void Init()
    {
        ActionManager.ClearRopeSelection += OnClearRopeSelection;
        vibration = VibrationManager.Instance;
    }

    public void DeInit()
    {
        ActionManager.ClearRopeSelection -= OnClearRopeSelection;
    }

    private void OnMouseDown()
    {
        if (selected)
        {
            OnClearRopeSelection();
            return;
        }

        ActionManager.ClearRopeSelection?.Invoke();
        ActionManager.RopeSelection?.Invoke(gameObject);
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
        PlayDoPunch(model);
    }

    private void PlayDoPunch(Transform refObject)
    {
        refObject.DOScale(new Vector3(1f, 1f, 1f), 0f);
        refObject.DOPunchScale(Vector3.one * 0.15f, 0.5f, 10, 1);
    }
}
