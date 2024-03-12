using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildModel", menuName = "Model/BuildModel", order = 0)]
public class BuildModel : ScriptableObject
{
    public enum Type
    {
        A,
        B,
        C,
        D
    }

    private int ID;
    [SerializeField] private string sName;
    [SerializeField] private Sprite image;
    [SerializeField] private Resource resourceCost;
    [SerializeField] private float resourceGenerationDuration;
    [SerializeField] private Resource resourceGain;
    [SerializeField] private BuildModel.Type buildType;

    public string SName => sName;
    public Sprite Image => image;
    public Resource ResourceCost => resourceCost;
    public float ResourceGenerationDuration => resourceGenerationDuration;
    public Resource ResourceGain => resourceGain;
    public BuildModel.Type BuildType => buildType;

    private void Awake() 
    {
        ID = sName.GetHashCode();    
    }
}
