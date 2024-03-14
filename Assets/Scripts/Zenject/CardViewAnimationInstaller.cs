using DG.Tweening;
using UnityEngine;
using Zenject;

[System.Serializable]
public class CardViewAnimation
{
    public float scaleFactor;
    public Ease enterScaleEase;
    public Ease exitScaleEase;
    public float enterTime;
    public float exitTime;
}

[CreateAssetMenu(fileName = "CardViewInstaller", menuName = "Installers/CardViewInstaller")]
public class CardViewAnimationInstaller : ScriptableObjectInstaller<CardViewAnimationInstaller>
{
    [SerializeField] CardViewAnimation cardViewAnimation;

    public override void InstallBindings()
    {
        Container.BindInstance<CardViewAnimation>(cardViewAnimation);
    }
}