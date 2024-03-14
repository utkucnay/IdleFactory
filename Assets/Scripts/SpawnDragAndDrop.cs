using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnDragAndDrop : IDragAndDrop
{
    GameObject draggedObject;
    GameObject draggedObjectPrefab;
    Canvas canvas;

    string buildName;

    TilePresenter tilePresenter;
    
    BuildView buildView;

    public SpawnDragAndDrop(string buildName, GameObject gameObject, TilePresenter tilePresenter)
    {
        draggedObjectPrefab = gameObject;
        this.tilePresenter = tilePresenter;
        canvas = GameObject.FindAnyObjectByType<Canvas>();
        this.buildName = buildName;
    }
    public void OnDragBegin(PointerEventData eventData)
    {
        draggedObject = GameObject.Instantiate(draggedObjectPrefab, eventData.position, Quaternion.identity, canvas.transform);
        buildView = draggedObject.GetComponentInChildren<BuildView>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Tile tile = null;
        if (tilePresenter.IsValidTile(eventData.position, out tile))
        {
            draggedObject.transform.position = tile.transform.position;
            if (tilePresenter.IsConstructable(buildView.MainTile, tile))
            {
                buildView.SetColor(Color.green);
            }
            else
            {
                buildView.SetColor(Color.red);
            }
        }
        else 
        {
            draggedObject.transform.position = eventData.position;
            buildView.SetColor(Color.red);
        }
    }

    public void OnDragEnd(PointerEventData eventData)
    {
        Tile tile = null;
        if (tilePresenter.IsValidTile(eventData.position, out tile))
        {
            if (tilePresenter.IsConstructable(buildView.MainTile, tile))
            {
                tilePresenter.CreateBuild(buildName, tile);
            }
        }

        GameObject.Destroy(draggedObject);
    }
}
