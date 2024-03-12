using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] CardViewScriptable cardViewScriptable;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin!");  
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag!");  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End!");  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Mouse");
        transform.DOScale(1f + cardViewScriptable.scaleFactor, cardViewScriptable.enterTime).SetEase(cardViewScriptable.enterScaleEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Mouse");
        transform.DOScale(1f, cardViewScriptable.exitTime).SetEase(cardViewScriptable.exitScaleEase);
    }
}
