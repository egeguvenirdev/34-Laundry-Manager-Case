using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveImage : MonoBehaviour
{  
    [SerializeField] private float speed = 5f;

    private RectTransform rectTransform;
    private Vector3 smallScale = new Vector3(-100f, 0, 0);
    private Vector3 bigScale = new Vector3(100f, 0, 0);

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
        rectTransform.localPosition = Vector3.Lerp(smallScale, bigScale, lerpValue); // burdan da baslangıç ile bitiş değeri arasında value alacaksın
    }
}
