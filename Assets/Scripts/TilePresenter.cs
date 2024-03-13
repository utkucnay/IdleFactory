using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilePresenter : IInitializable
{
    TileModel tileModel;

    GraphicRaycaster raycaster;
    Canvas canvas;
    EventSystem eventSystem;

    public TilePresenter(TileModel tileModel, GraphicRaycaster graphicRaycaster, Canvas canvas, EventSystem eventSystem) 
    {
        this.tileModel = tileModel;
        this.raycaster = graphicRaycaster;
        this.canvas = canvas;
        this.eventSystem = eventSystem;
    }

    public void Initialize()
    {

    }

    public bool IsValidTile(in Vector3 pos, out Tile tile) 
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = pos;

        raycaster.Raycast(eventData, raycastResults);
        tile = null;

        foreach (var raycastResult in raycastResults)
        {
            if (raycastResult.gameObject.TryGetComponent<Tile>(out tile))
            {
                if (tile is BuildTile)
                {
                    tile = null;
                    continue;
                }
                break;
            }
        }

        return tile != null;
    }
}
