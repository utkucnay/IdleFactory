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

    public float minVibrationProgressBarFreq;
    public float minVibrationProgressBarFactor;

    public float maxVibrationProgressBarFreq;
    public float maxVibrationProgressBarFactor;

    public AnimationCurve curve;

    public GameObject goldParticlePrefab;
    public GameObject gemParticlePrefab;
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