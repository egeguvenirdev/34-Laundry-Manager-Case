using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapImageScale : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private RectTransform rectTransform;
    private Vector3 smallScale = new Vector3(0.7f, 0.7f, 1f);
    private Vector3 bigScale = new Vector3(1f, 1f, 1f);
    
    private void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        TappingScaler();
    }

    private void TappingScaler()
    {
        float lerpValue = (Mathf.Sin(speed * Time.time) + 1f) / 2f; // <0, 1> verecek hep
        rectTransform.localScale = Vector3.Lerp(smallScale, bigScale, lerpValue); // burdan da baslangıç ile bitiş değeri arasında value alacaksın
    }
}
