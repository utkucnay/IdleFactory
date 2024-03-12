using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GamePresenter : IInitializable
{
    //Presenter
    ShopPresenter shopPresenter;
    TilePresenter tilePresenter;

    //Model
    ResourceModel resourceModel;
    //View
    ResourceView resourceView;

    bool Dragging = false;

    public GamePresenter(ResourceView resourceView, ResourceModel resourceModel, ShopPresenter shopPresenter)
    {
        this.resourceView = resourceView;
        this.resourceModel = resourceModel;
        this.shopPresenter = shopPresenter; 
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
        //Get Builder ID
        //Spawn It!
        Dragging = true;
    }

    public void OnDrag(CardView cardView, PointerEventData eventData)
    {
        //Check in Tile or not
        //if not Sync Mouse Pos
        //or sync pos with Tile
    }

    public void OnDragEnd(CardView cardView, PointerEventData eventData)
    {
        //Check is a valid tile.
        Dragging = false;
        cardView.ScaleDown();
    }

    public void OnPointerEnter(CardView cardView)
    {
        if(Dragging) return;
        cardView.ScaleUp();
    }

    public void OnPointerExit(CardView cardView)
    {
        if(Dragging) return;
        cardView.ScaleDown();
    }
}
