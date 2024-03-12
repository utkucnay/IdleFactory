using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ResourceModel resourceModel = new ResourceModel(10, 10);
        Container.BindInstance<ResourceModel>(resourceModel);
        Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle().NonLazy();
    }
}