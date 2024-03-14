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
    private string buildName;
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject gemGO;
    [SerializeField] private GameObject columnGO;
    [SerializeField] private GameObject goldGO;
    [SerializeField] private TextMeshProUGUI gemReq;
    [SerializeField] private TextMeshProUGUI goldReq;
    private Image viewImage;

    bool interactable = true;

    public string BuildName => buildName;

    [Inject()]
    private void Inject(CardViewAnimation cardViewAnimation)
    {
        this.cvAnimation = cardViewAnimation;
    }

    private void Awake()
    {
        viewImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(interactable)
            DragBegin?.Invoke(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(interactable)
            Drag?.Invoke(this, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(interactable)
            DragEnd?.Invoke(this, eventData); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(interactable)    
            PointerEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(interactable)
            PointerExit?.Invoke(this);
    }

    public void ScaleUp()
    {
        transform.DOScale(1f + cvAnimation.scaleFactor, cvAnimation.enterTime).SetEase(cvAnimation.enterScaleEase);
    }

    public void ScaleNormal()
    {
        transform.DOScale(1f, cvAnimation.exitTime).SetEase(cvAnimation.exitScaleEase);
    }

    public void SetSprite(Sprite sprite)
    {
        cardImage.sprite = sprite;
    }

    public void SetBuildName(string name)
    {
        buildName = name;
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

    public void SetIntractable(bool interactable)
    {
        this.interactable = interactable;
        if(interactable)
        {
            var color = viewImage.color;
            color.a = 1;
            viewImage.color = color;
        }
        else
        {
            var color = viewImage.color;
            color.a = .55f;
            viewImage.color = color;
        }
    }
}
