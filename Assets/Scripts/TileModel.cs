using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel : MonoBehaviour
{
    public Tile[] tiles;

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                var tileCurrent = tiles[y * 10 + x];
                Tile tileRight = null;
                Tile tileDown = null;

                if (y != 9)
                {
                    tileDown = tiles[(y + 1) * 10 + x];
                    tileDown.Up = tileCurrent;
                }
                if (x != 9) 
                { 
                    tileRight = tiles[y * 10 + (x + 1)];
                    tileRight.Left = tileCurrent;
                }

                tileCurrent.Down = tileDown;
                tileCurrent.Right = tileRight;
            }
        }
    }
}
