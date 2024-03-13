using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildModel", menuName = "Model/BuildModel", order = 0)]
public class BuildModel : ScriptableObject
{
    [SerializeField] private string sName;
    [SerializeField] private Sprite image;
    [SerializeField] private Resource resourceCost;
    [SerializeField] private float resourceGenerationDuration;
    [SerializeField] private Resource resourceGain;
    [SerializeField] private GameObject buildPrefab;

    public string SName => sName;
    public Sprite Image => image;
    public Resource ResourceCost => resourceCost;
    public float ResourceGenerationDuration => resourceGenerationDuration;
    public Resource ResourceGain => resourceGain;
    public GameObject BuildPrefab => buildPrefab;
}
