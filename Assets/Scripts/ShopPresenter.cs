using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ShopPresenter : MonoBehaviour
{
    public event Action<CardView, PointerEventData> DragBegin;
    public event Action<CardView, PointerEventData> Drag;
    public event Action<CardView, PointerEventData> DragEnd;
    public event Action<CardView> PointerEnter;
    public event Action<CardView> PointerExit;

    //Models
    ResourceModel goldModel;

    //Views
    CardView[] cardView;

    [Inject()]
    private void Inject(BuildModels6 buildModels) 
    {
        cardView = GetComponentsInChildren<CardView>();    

        SetCardViewData(cardView[0], buildModels.buildModel1);
        SetCardViewData(cardView[1], buildModels.buildModel2);
        SetCardViewData(cardView[2], buildModels.buildModel3);
        SetCardViewData(cardView[3], buildModels.buildModel4);
        SetCardViewData(cardView[4], buildModels.buildModel5);
        SetCardViewData(cardView[5], buildModels.buildModel6);
    }

    private void Start() 
    {
        for(int i = 0; i < cardView.Length; i++)
            SetCardViewEvents(cardView[i]);
    }

    private void SetCardViewData(CardView cardView, BuildModel buildModel)
    {
        cardView.SetSprite(buildModel.Image);
        cardView.SetResourceCost(buildModel.ResourceCost);
    }

    private void SetCardViewEvents(CardView cardView)
    {
        cardView.DragBegin += (x, y) => DragBegin?.Invoke(x, y);
        cardView.Drag += (x, y) => Drag?.Invoke(x, y);
        cardView.DragEnd += (x, y) => DragEnd?.Invoke(x, y);
        cardView.PointerEnter += (x) => PointerEnter?.Invoke(x);
        cardView.PointerExit += (x) => PointerExit?.Invoke(x);
    }
}
