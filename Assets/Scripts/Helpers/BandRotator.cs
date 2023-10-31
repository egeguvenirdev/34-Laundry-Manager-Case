using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandRotator : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float speed;

    private float offsetValueY;
    private bool canMove;

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
        offsetValueY -= speed * deltaTime;
        mat.mainTextureOffset = new Vector2(0, offsetValueY);
    }
}