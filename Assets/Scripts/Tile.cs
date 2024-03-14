using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] protected Tile up;
    [SerializeField] protected Tile down;
    [SerializeField] protected Tile left;
    [SerializeField] protected Tile right;
    [SerializeField] protected bool isEmpty;

    public Tile Up { get => up; set => up = value; }
    public Tile Down { get => down; set => down = value; }
    public Tile Left { get => left; set => left = value; }
    public Tile Right { get => right; set => right = value; }
    public bool IsEmpty { get => isEmpty; set => isEmpty = value; }

    private void Start()
    {
        IsEmpty = true;
    }

    public int GetCount() 
    {
        HashSet<Tile> set = new();
        GetCountRec(this, set);
        return set.Count;
    }

    public void GetCountRec(Tile tile, HashSet<Tile> set) 
    {
        if (tile != null && !set.Contains(tile)) 
        {
            set.Add(tile);
            GetCountRec(tile.Up, set);
            GetCountRec(tile.Down, set);
            GetCountRec(tile.Left, set);
            GetCountRec(tile.Right, set);
        }
    }
}
