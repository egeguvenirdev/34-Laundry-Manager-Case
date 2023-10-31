using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamManager : MonoBehaviour
{
    [Header("Cam Follow Settings")]
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float playerFollowSpeed = 0.125f;
    [SerializeField] private float clampLocalX = 1.5f;

    public void Init()
    {
        ActionManager.UpdateManager += OnUpdate;
    }

    public void DeInit()
    {
        ActionManager.UpdateManager -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {

    }
}