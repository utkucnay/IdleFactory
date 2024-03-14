using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildView : MonoBehaviour
{
    [SerializeField] public GameObject buildTilePrefab;
    [SerializeField] BuildTile mainTile;
    [SerializeField] Image[] images;
    [SerializeField] Slider progressionBar;

    public Image[] Images => images;
    public BuildTile MainTile => mainTile;
    public Slider ProgressionBar => progressionBar;

    private void Awake()
    {
        Dictionary<Tile, Image> imageMap = new();
        AddImageInTile(imageMap, mainTile);
        images = imageMap.Values.ToArray();
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < Images.Length; i++)
            Images[i].color = color;
    }

    private void AddImageInTile(Dictionary<Tile, Image> imageMap, Tile tile)
    {
        if (tile.Up && !imageMap.ContainsKey(tile.Up)) 
        {
            imageMap.Add(tile.Up, tile.Up.GetComponent<Image>());
            AddImageInTile(imageMap, tile.Up);
        }

        if (tile.Down && !imageMap.ContainsKey(tile.Down))
        {
            imageMap.Add(tile.Down, tile.Down.GetComponent<Image>());
            AddImageInTile(imageMap, tile.Down);
        }

        if (tile.Left && !imageMap.ContainsKey(tile.Left))
        {
            imageMap.Add(tile.Left, tile.Left.GetComponent<Image>());
            AddImageInTile(imageMap, tile.Left);
        }

        if (tile.Right && !imageMap.ContainsKey(tile.Right))
        {
            imageMap.Add(tile.Right, tile.Right.GetComponent<Image>());
            AddImageInTile(imageMap, tile.Right);
        }
    }

    public void SetProgressionRatio(float ratio)
    {
        progressionBar.DOKill(true);
        progressionBar.DOValue(ratio, .1f);
    }
}
