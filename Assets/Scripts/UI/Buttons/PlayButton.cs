using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonBase
{
    [SerializeField] private GameObject panelElements;
    private GameManager gameManager;

    public override void Init()
    {
        gameManager = GameManager.Instance;
    }

    public override void DeInit()
    {

    }

    public override void OnButtonClick()
    {
        gameObject.SetActive(false);
        gameManager.OnStartTheGame();
    }
}
