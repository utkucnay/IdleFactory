using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<CardView, PointerEventData> DragBegin;
    public event Action<CardView, PointerEventData> Drag;
    public event Action<CardView, PointerEventData> DragEnd;
    public event Action<CardView> PointerEnter;
    public event Action<CardView> PointerExit;

    private CardViewAnimation cvAnimation;
    private int BuildID;
    [SerializeField] private Image image;
    [SerializeField] private GameObject gemGO;
    [SerializeField] private GameObject columnGO;
    [SerializeField] private GameObject goldGO;
    [SerializeField] private TextMeshProUGUI gemReq;
    [SerializeField] private TextMeshProUGUI goldReq;

    [Inject()]
    private void Inject(CardViewAnimation cardViewAnimation)
    {
        this.cvAnimation = cardViewAnimation;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragBegin?.Invoke(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Drag?.Invoke(this, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnd?.Invoke(this, eventData); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke(this);
    }

    public void ScaleUp()
    {
        transform.DOScale(1f + cvAnimation.scaleFactor, cvAnimation.enterTime).SetEase(cvAnimation.enterScaleEase);
    }

    public void ScaleDown()
    {
        transform.DOScale(1f, cvAnimation.exitTime).SetEase(cvAnimation.exitScaleEase);
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetResourceCost(Resource resource)
    {
        gemReq.text = resource.gem.ToString();
        goldReq.text = resource.gold.ToString();
        
        if(resource.gem <= 0)
        {
            gemGO.SetActive(false);
            columnGO.SetActive(false);
        }

        if(resource.gold <= 0)
        {
            goldGO.SetActive(false);
            columnGO.SetActive(false);
        }
    }

    public void SetActive(bool active)
    {
        if(active)
        {

        }
        else
        {

        }
    }
}
