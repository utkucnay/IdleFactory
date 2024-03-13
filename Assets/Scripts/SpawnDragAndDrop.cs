using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnDragAndDrop : IDragAndDrop, IDisposable
{
    GameObject draggedObject;
    GameObject draggedObjectPrefab;


    TilePresenter tilePresenter;
    Canvas canvas;

    public SpawnDragAndDrop(GameObject gameObject, TilePresenter tilePresenter)
    {
        draggedObjectPrefab = gameObject;
        this.tilePresenter = tilePresenter;
        canvas = GameObject.FindAnyObjectByType<Canvas>(); 

    }
    public void OnDragBegin(PointerEventData eventData)
    {
        draggedObject = GameObject.Instantiate(draggedObjectPrefab, eventData.position, Quaternion.identity, canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Check Valid Tile
        Tile tile = null;
        if (tilePresenter.IsValidTile(eventData.position, out tile))
        {
            draggedObject.transform.position = tile.transform.position;
        }
        else 
        {
            draggedObject.transform.position = eventData.position;
        }
    }

    public void OnDragEnd(PointerEventData eventData)
    {
        //Check Valid Tile

        Dispose();
    }

    public void Dispose()
    {
        GameObject.Destroy(draggedObject);
    }
}
