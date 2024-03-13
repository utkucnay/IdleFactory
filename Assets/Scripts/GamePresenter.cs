using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GamePresenter : Zenject.IInitializable
{
    //Presenter
    ShopPresenter shopPresenter;
    TilePresenter tilePresenter;

    //Model
    ResourceModel resourceModel;
    //View
    ResourceView resourceView;

    IDragAndDrop dragAndDrop;
    Dictionary<string, BuildModel> buildModels;

    public GamePresenter(ResourceView resourceView, ResourceModel resourceModel, 
        ShopPresenter shopPresenter, TilePresenter tilePresenter, BuildModels6 buildModels6)
    {
        this.resourceView = resourceView;
        this.resourceModel = resourceModel;
        this.shopPresenter = shopPresenter; 
        this.tilePresenter = tilePresenter;

        buildModels = new();
        buildModels.Add(buildModels6.buildModel1.SName, buildModels6.buildModel1);
        buildModels.Add(buildModels6.buildModel2.SName, buildModels6.buildModel2);
        buildModels.Add(buildModels6.buildModel3.SName, buildModels6.buildModel3);
        buildModels.Add(buildModels6.buildModel4.SName, buildModels6.buildModel4);
        buildModels.Add(buildModels6.buildModel5.SName, buildModels6.buildModel5);
        buildModels.Add(buildModels6.buildModel6.SName, buildModels6.buildModel6);

    }

    private void OnResourceChanged() 
    {
        resourceView.SetResource(resourceModel.CurrentResource);
    }

    public void Initialize()
    {
        resourceModel.ResourceChanged += OnResourceChanged;
        OnResourceChanged();  

        shopPresenter.DragBegin += OnDragBegin;  
        shopPresenter.Drag += OnDrag;  
        shopPresenter.DragEnd += OnDragEnd;  

        shopPresenter.PointerEnter += OnPointerEnter;  
        shopPresenter.PointerExit += OnPointerExit;  
    }

    public void OnDragBegin(CardView cardView, PointerEventData eventData)
    {
        dragAndDrop = new SpawnDragAndDrop(buildModels[cardView.BuildName].BuildPrefab, tilePresenter);
        dragAndDrop.OnDragBegin(eventData);
    }

    public void OnDrag(CardView cardView, PointerEventData eventData)
    {
        dragAndDrop.OnDrag(eventData);
    }

    public void OnDragEnd(CardView cardView, PointerEventData eventData)
    {
        cardView.ScaleNormal();
        dragAndDrop.OnDragEnd(eventData);
        dragAndDrop = null;
    }

    public void OnPointerEnter(CardView cardView)
    {
        if(dragAndDrop != null) return;
        cardView.ScaleUp();
    }

    public void OnPointerExit(CardView cardView)
    {
        if(dragAndDrop != null) return;
        cardView.ScaleNormal();
    }
}
