using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ResourceModel resourceModel = new ResourceModel(10, 10);
        Container.BindInstance<ResourceModel>(resourceModel);
        Container.BindInterfacesAndSelfTo<TilePresenter>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle().NonLazy();
        Container.Bind<Canvas>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EventSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GraphicRaycaster>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<BuildPresenter>().AsSingle().NonLazy();
    }
}