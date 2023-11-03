using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamManager : MonoBehaviour
{
    [Header("Cam Settings")]
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Transform cam;

    public void Init()
    {
        ActionManager.UpdateManager += OnUpdate;
        cam.DOLocalRotate(startRotation, 0.75f);
    }

    public void DeInit()
    {
        ActionManager.UpdateManager -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {

    }
}