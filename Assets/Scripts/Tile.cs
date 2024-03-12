using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Tile up;
    private Tile down;
    private Tile left;
    private Tile right;

    public Tile Up { get => up; set => up = value; }
    public Tile Down { get => down; set => down = value; }
    public Tile Left { get => left; set => left = value; }
    public Tile Right { get => right; set => right = value; }

}
