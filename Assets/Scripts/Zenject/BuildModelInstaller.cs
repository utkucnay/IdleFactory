using UnityEngine;
using Zenject;

[System.Serializable]
public class BuildModels6
{
    public BuildModel buildModel1;
    public BuildModel buildModel2;
    public BuildModel buildModel3;
    public BuildModel buildModel4;
    public BuildModel buildModel5;
    public BuildModel buildModel6;

}

[CreateAssetMenu(fileName = "BuildModelInstaller", menuName = "Installers/BuildModelInstaller")]
public class BuildModelInstaller : ScriptableObjectInstaller<BuildModelInstaller>
{
    [SerializeField] BuildModels6 buildModels;
    public override void InstallBindings()
    {
        Container.BindInstance<BuildModels6>(buildModels);
    }
}