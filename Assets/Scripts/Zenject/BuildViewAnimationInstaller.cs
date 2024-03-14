using DG.Tweening;
using UnityEngine;
using Zenject;

[System.Serializable]
public class BuildViewAnimation 
{
    [Header("Procedural Animation Fields")]
    public float minScaleBuildFreq;
    public float minScaleBuildFactor;

    public float maxScaleBuildFreq;
    public float maxScaleBuildFactor;

    public AnimationCurve scaleBuildcurve;

    [Header("Particle")]
    public GameObject goldParticlePrefab;
    public GameObject gemParticlePrefab;

    public float minParticleDistance;
    public float maxParticleDistance;

    public float exploseParticeDuration;
    public Ease exploseParticeEase;

    public float transitionWait;

    public float moveResourceDuration;
    public Ease moveResourceEase;
}

[CreateAssetMenu(fileName = "BuildViewInstaller", menuName = "Installers/BuildViewInstaller")]
public class BuildViewAnimationInstaller : ScriptableObjectInstaller<BuildViewAnimationInstaller>
{
    [SerializeField] BuildViewAnimation buildViewAnimation;
    public override void InstallBindings()
    {
        Container.Bind<BuildViewAnimation>().FromInstance(buildViewAnimation);
    }
}