using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothManager : MonoBehaviour
{
    [Header("Placement Settings")]
    [SerializeField] private Transform[] threadPlacementTransforms;
    [SerializeField] private Transform threadStackTransform;
    [SerializeField] private float placementDuration;

    private List<ClothesBase> clothes = new List<ClothesBase>();
    private ObjectPooler pooler;

    private ClothesBase currentCloth;

    public void Init()
    {
        ActionManager.GainClothes += OnEarnClothes;
        ActionManager.ClothSelection += OnClothesSelection;
        ActionManager.ClearClothSelection += OnClearClothesSelection;
        ActionManager.GetSelectedCloth += OnGetSelectedClothes;
        pooler = ObjectPooler.Instance;
    }

    public void DeInit()
    {
        ActionManager.GainClothes -= OnEarnClothes;
        ActionManager.ClothSelection -= OnClothesSelection;
        ActionManager.ClearClothSelection -= OnClearClothesSelection;
        ActionManager.GetSelectedCloth -= OnGetSelectedClothes;

        for (int i = 0; i < clothes.Count; i++)
        {
            clothes[i].DeInit();
        }
    }

    private void OnEarnClothes(ClothesBase clothesBase)
    {
        clothesBase.transform.position = threadStackTransform.position;
        clothes.Add(clothesBase);

        ReplaceClothes();
    }

    private void OnClothesSelection(ClothesBase selectedThread)
    {
        currentCloth = selectedThread;
    }

    private void OnClearClothesSelection()
    {
        currentCloth = null;
    }

    private void OnGetSelectedClothes(DyeMachineBase machine)
    {
        if (currentCloth != null)
        {
            clothes.Remove(currentCloth);
            ReplaceClothes();
            StartCoroutine(machine.ProduceClothes(currentCloth.MoveToTarget(machine.GetThreadTransform)));
        }
    }

    private void ReplaceClothes()
    {
        for (int i = 0; i < threadPlacementTransforms.Length; i++)
        {
            if (i >= clothes.Count) return;
            clothes[i].transform.DOMove(threadPlacementTransforms[i].position, placementDuration);
        }
    }
}
