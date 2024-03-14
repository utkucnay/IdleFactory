using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SaveLoad : IInitializable, ITickable
{
    static readonly string GOLD = "Gold";
    static readonly string GEM = "Gem";

    static readonly string BUILD_NAME = "BuildName";
    static readonly string TILE_INDEX = "TileIndex";

    [Inject] ResourceModel resourceModel;
    [Inject] TilePresenter tilePresenter;

    int frame = 0;

    public void Initialize()
    {
        Application.quitting += OnAppExit;
    }

    public void Tick()
    {
        frame++;
        if(frame == 2)
        {
            int gem = PlayerPrefs.GetInt(GEM, 10);
            int gold = PlayerPrefs.GetInt(GOLD, 10);
            Resource newResource = new Resource() { gold = gold, gem = gem };

            resourceModel.CurrentResource = newResource;

            for (int i = 0; PlayerPrefs.HasKey(BUILD_NAME + i); i++)
            {
                var buildName = PlayerPrefs.GetString(BUILD_NAME + i);
                var tileIndex = PlayerPrefs.GetInt(TILE_INDEX + i);

                tilePresenter.CreateBuild(buildName, tilePresenter.GetTileIndex(tileIndex), false);
            }
        }
    }

    private void OnAppExit() 
    {
        PlayerPrefs.SetInt(GOLD, resourceModel.CurrentResource.gold);
        PlayerPrefs.SetInt(GEM, resourceModel.CurrentResource.gem);

        var saveData = tilePresenter.GetSaveData();

        for (int i = 0; i < saveData.Count; i++)
        {
            PlayerPrefs.SetString(BUILD_NAME + i, saveData[i].Item1);
            PlayerPrefs.SetInt(TILE_INDEX + i, saveData[i].Item2);
        }
    }

    public void Clear() 
    {
        PlayerPrefs.DeleteAll();
    }
}
