using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

public class GamePresenter : Zenject.IInitializable
{
    //Presenter
    ShopPresenter shopPresenter;
    TilePresenter tilePresenter;
    BuildPresenter buildPresenter;

    //View
    [Inject] RestartView restartView;

    [Inject] SaveLoad saveLoad;

    IDragAndDrop dragAndDrop;

    public GamePresenter( 
        ShopPresenter shopPresenter, TilePresenter tilePresenter, BuildPresenter buildPresenter)
    {
        this.shopPresenter = shopPresenter; 
        this.tilePresenter = tilePresenter;
        this.buildPresenter = buildPresenter;
    }

    public void Initialize()
    {
        shopPresenter.DragBegin += OnDragBegin;  
        shopPresenter.Drag += OnDrag;  
        shopPresenter.DragEnd += OnDragEnd;  

        shopPresenter.PointerEnter += OnPointerEnter;  
        shopPresenter.PointerExit += OnPointerExit;

        restartView.Click += OnGameRestart;
    }

    public void OnDragBegin(CardView cardView, PointerEventData eventData)
    {
        dragAndDrop = new SpawnDragAndDrop(cardView.BuildName, buildPresenter.GetPrefab(cardView.BuildName), tilePresenter);
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

    public void OnGameRestart() 
    {
        saveLoad.Clear();
        tilePresenter.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
