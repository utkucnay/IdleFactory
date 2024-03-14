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
    [Inject] ResourceModel resourceModel;

    [Inject] BuildPresenter buildPresenter;

    //Views
    CardView[] cardViews;
    [Inject] ResourceView resourceView;

    public Transform GoldViewTransform => resourceView.GoldView.transform;
    public Transform GemViewTransform => resourceView.GemView.transform;

    [Inject]
    private void Inject(BuildModels6 buildModels) 
    {
        cardViews = GetComponentsInChildren<CardView>();    

        SetCardViewData(cardViews[0], buildModels.buildModel1);
        SetCardViewData(cardViews[1], buildModels.buildModel2);
        SetCardViewData(cardViews[2], buildModels.buildModel3);
        SetCardViewData(cardViews[3], buildModels.buildModel4);
        SetCardViewData(cardViews[4], buildModels.buildModel5);
        SetCardViewData(cardViews[5], buildModels.buildModel6);
    }

    private void Start() 
    {
        for(int i = 0; i < cardViews.Length; i++)
            SetCardViewEvents(cardViews[i]);

        resourceModel.ResourceChanged += OnResourceChanged;
        OnResourceChanged();
    }

    private void OnResourceChanged()
    {
        resourceView.SetResource(resourceModel.CurrentResource);
        foreach (var cardView in cardViews)
        {
            var resCost = buildPresenter.GetResourceCost(cardView.BuildName);
            cardView.SetIntractable(
                resourceModel.CurrentResource.gold >= resCost.gold
                & resourceModel.CurrentResource.gem >= resCost.gem);
        }
    }

    public void AddResource(int gold, int gem) 
    {
        resourceModel.IncreaseResource(new Resource() { gold = gold, gem = gem }); 
    }

    private void SetCardViewData(CardView cardView, BuildModel buildModel)
    {
        cardView.SetSprite(buildModel.Image);
        cardView.SetBuildName(buildModel.SName);
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
