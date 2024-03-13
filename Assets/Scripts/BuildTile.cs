using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTile : Tile
{
    [Button]
    private void CreateUP() 
    {
        if (Up != null)
        {
            Debug.LogWarning("Warning Created Up Tile");
            return;
        }
        Create(new Vector3(0, 100, 0), ref up);
        up.Down = this;
        if (Right != null)
        {
            Up.Right = Right.Up;
        }

        if (Left != null)
        { 
            Up.Left = Left.Up;
        }

        SyncOtherTiles(Up);
    }

    [Button]
    private void CreateDown()
    {
        if (Down != null)
        {
            Debug.LogWarning("Warning Created Down Tile");
            return;
        }
        Create(new Vector3(0, -100, 0), ref down);
        down.Up = this;

        if (Right != null)
        {
            Up.Right = Right.Down;
        }

        if (Left != null)
        {
            Up.Left = Left.Down;
        }

        SyncOtherTiles(Down);
    }

    [Button]
    private void CreateLeft()
    {
        if (Left != null)
        {
            Debug.LogWarning("Warning Created Left Tile");
            return;
        }
        Create(new Vector3(-100, 0, 0), ref left);
        left.Right = this;

        if (Up != null)
        {
            Left.Up = Up.Left;
        }

        if (Down != null) 
        {
            Left.Down = Down.Left;
        }

        SyncOtherTiles(Left);
    }

    [Button]
    private void CreateRight()
    {
        if (Right != null)
        {
            Debug.LogWarning("Warning Created Righ Tile");
            return;
        }
        Create(new Vector3(100, 0, 0), ref right);
        right.Left = this;

        if (Up != null)
        {
            Right.Up = Up.Right;
        }

        if (Down != null)
        {
            Right.Down = Down.Right;
        }

        SyncOtherTiles(Right);
    }

    [Button]
    private void DeleteTile() 
    {
        if(Up != null)
            Up.Down = null;
        if(Down != null)
            Down.Up = null;
        if(Left != null)
            Left.Right = null;
        if(Right != null)
            Right.Left = null;
        DestroyImmediate(this.gameObject);
    }

    private void SyncOtherTiles(Tile tile) 
    {
        if (tile.Up)
            tile.Up.Down = tile;
        if (tile.Down)
            tile.Down.Up = tile;
        if (tile.Left)
            tile.Left.Right = tile;
        if (tile.Right)
            tile.Right.Left = tile;
    }

    private void Create(Vector3 move, ref Tile tile)
    {
        var prefab = GetComponentInParent<BuildView>().buildTilePrefab;
        var GO = Instantiate(prefab, transform.position, Quaternion.identity, this.transform.parent);
        GO.transform.localPosition += move;
        tile = GO.GetComponent<Tile>();
    }
}
