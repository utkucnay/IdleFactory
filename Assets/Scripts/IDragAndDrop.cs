using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDragAndDrop
{
    public void OnDragBegin(PointerEventData eventData);
    public void OnDrag(PointerEventData eventData);
    public void OnDragEnd(PointerEventData eventData);
}
