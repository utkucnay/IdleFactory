using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ModestTree;

public class TilePresenter : IInitializable
{
    TileModel tileModel;

    BuildPresenter buildPresenter;

    GraphicRaycaster raycaster;
    Canvas canvas;
    EventSystem eventSystem;

    public TilePresenter(TileModel tileModel, GraphicRaycaster graphicRaycaster, 
        Canvas canvas, EventSystem eventSystem, BuildPresenter buildPresenter) 
    {
        this.tileModel = tileModel;
        this.raycaster = graphicRaycaster;
        this.canvas = canvas;
        this.eventSystem = eventSystem;
        this.buildPresenter = buildPresenter;
    }

    public void Initialize()
    {

    }

    public Tile GetTileIndex(int index)
    {
        return tileModel.tiles[index];
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

    public bool IsConstructable(BuildTile buildTile, Tile tile) 
    {
        HashSet<BuildTile> set = new();
        bool isCtor = true;
        IsConstructableRec(set, buildTile, tile, ref isCtor);
        return isCtor;
    }

    private void IsConstructableRec(HashSet<BuildTile> set, BuildTile buildTile, Tile tile, ref bool isCtor)
    {
        if (tile == null) { isCtor = false; return; }
        if (!tile.IsEmpty) { isCtor = false; return; }

        set.Add(buildTile);

        if (buildTile.Up != null && !set.Contains(buildTile.Up as BuildTile)) IsConstructableRec(set, buildTile.Up as BuildTile, tile.Up, ref isCtor);
        if (buildTile.Down != null && !set.Contains(buildTile.Down as BuildTile)) IsConstructableRec(set, buildTile.Down as BuildTile, tile.Down, ref isCtor);
        if (buildTile.Left != null && !set.Contains(buildTile.Left as BuildTile)) IsConstructableRec(set, buildTile.Left as BuildTile, tile.Left, ref isCtor);
        if (buildTile.Right != null && !set.Contains(buildTile.Right as BuildTile)) IsConstructableRec(set, buildTile.Right as BuildTile, tile.Right, ref isCtor);

        if(set.Count >= buildTile.GetCount()) { isCtor = true; return; }
    }

    public void CreateBuild(string buildName, Tile tile, bool isBuy = true) 
    {
        var go = buildPresenter.Build(buildName, tile.transform.position, isBuy);
        
        BuildTile buildTile = go.GetComponent<BuildView>().MainTile;
        HashSet<BuildTile> set = new();
        CreateBuildRec(set, buildTile, tile);
    }

    private void CreateBuildRec(HashSet<BuildTile> set, BuildTile buildTile, Tile tile)
    {
        if (tile != null)
            tile.IsEmpty = false;

        set.Add(buildTile);

        if (buildTile.Up != null && !set.Contains(buildTile.Up as BuildTile)) CreateBuildRec(set, buildTile.Up as BuildTile, tile.Up);
        if (buildTile.Down != null && !set.Contains(buildTile.Down as BuildTile)) CreateBuildRec(set, buildTile.Down as BuildTile, tile.Down);
        if (buildTile.Left != null && !set.Contains(buildTile.Left as BuildTile)) CreateBuildRec(set, buildTile.Left as BuildTile, tile.Left);
        if (buildTile.Right != null && !set.Contains(buildTile.Right as BuildTile)) CreateBuildRec(set, buildTile.Right as BuildTile, tile.Right);
    }

    public List<System.Tuple<string, int>> GetSaveData()
    {
        List<System.Tuple<string, int>> ret = new();
        for (int i = 0; i < buildPresenter.BuildViews.Count; i++)
        { 
            var buildView = buildPresenter.BuildViews[i];
            var buildProp = buildPresenter.BuildProps[i];

            var buildName = buildProp.sName;

            Tile tile;
            IsValidTile(buildView.MainTile.transform.position, out tile);
            var index = tileModel.tiles.IndexOf(tile);

            ret.Add(new System.Tuple<string, int>(buildName, index));
        }

        return ret;
    }

    public void Clear() 
    {
        buildPresenter.BuildViews.Clear();
        buildPresenter.BuildProps.Clear();
    }
}
