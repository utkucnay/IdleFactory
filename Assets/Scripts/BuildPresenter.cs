using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BuildProp 
{
    public static event Action<BuildProp> ChangedProgress;

    public BuildProp(string sName)
    {
        seed = UnityEngine.Random.Range(0f, 10000f);
        this.sName = sName;
    }

    public string sName;
    public float seed;
    private float spentTime;
    public float SpentTime { get => spentTime; set { spentTime = value; ChangedProgress?.Invoke(this); } }
}

public class BuildPresenter : ITickable
{
    //Model
    Dictionary<string, BuildModel> buildModels;
    
    //Presenter
    [Inject] ShopPresenter shopPresenter;

    //View
    List<BuildView> buildViews;
    List<BuildProp> buildProps;

    public BuildViewAnimation BuildViewAnimation { get; private set; }

    Canvas canvas;

    public BuildPresenter(BuildModels6 buildModels6, Canvas canvas, BuildViewAnimation buildViewAnimation)
    {
        buildModels = new()
        {
            { buildModels6.buildModel1.SName, buildModels6.buildModel1 },
            { buildModels6.buildModel2.SName, buildModels6.buildModel2 },
            { buildModels6.buildModel3.SName, buildModels6.buildModel3 },
            { buildModels6.buildModel4.SName, buildModels6.buildModel4 },
            { buildModels6.buildModel5.SName, buildModels6.buildModel5 },
            { buildModels6.buildModel6.SName, buildModels6.buildModel6 }
        };

        this.canvas = canvas;

        buildViews = new();
        buildProps = new();
        BuildViewAnimation = buildViewAnimation;

        BuildProp.ChangedProgress += OnChangedProgress;
    }

    public GameObject GetPrefab(string name)
    {
        return buildModels[name].BuildPrefab;
    }

    public Resource GetResourceCost(string name)
    {
        return buildModels[name].ResourceCost;
    }

    public void Build(string buildName, in Vector3 pos)
    {
        var go = GameObject.Instantiate(GetPrefab(buildName), pos, Quaternion.identity, canvas.transform);

        var cost = GetResourceCost(buildName);
        shopPresenter.AddResource(-cost.gold, -cost.gem);
        buildViews.Add(go.GetComponent<BuildView>());
        buildProps.Add(new BuildProp(buildName));
    }

    public void Tick()
    {
        for (int i = 0; i < buildViews.Count; i++)
        {
            var buildView = buildViews[i];
            var buildProp = buildProps[i];

            var scaleRatio = buildProp.SpentTime / buildModels[buildProp.sName].ResourceGenerationDuration;

            if (scaleRatio >= .97f) continue;

            ProceduralBuildAnimation(scaleRatio, buildView, buildProp);

            if (scaleRatio > .95f)
            {
                buildProp.SpentTime = buildModels[buildProp.sName].ResourceGenerationDuration;
                ResourceGainedAnimation(buildModels[buildProp.sName], buildViews[i], () => buildProp.SpentTime = 0);
            }
            else 
            {
                buildProp.SpentTime += Time.deltaTime;
            }
        }
    }

    private void ProceduralBuildAnimation(float scaleRatio, BuildView buildView, BuildProp buildProp)
    {
        var scaleEvaluatedRatio = BuildViewAnimation.curve.Evaluate(scaleRatio);
        var scaleFreq = Mathf.Lerp(BuildViewAnimation.minScaleBuildFreq, BuildViewAnimation.maxScaleBuildFreq, scaleEvaluatedRatio);
        var scaleFactor = Mathf.Lerp(BuildViewAnimation.minScaleBuildFactor, BuildViewAnimation.maxScaleBuildFactor, scaleEvaluatedRatio);
        var scale = Mathf.Sin((Time.time + buildProp.seed) * scaleFreq) * scaleFactor;
        scale = Mathf.Lerp(buildView.transform.localScale.x, 1 + scale, .2f);

        buildView.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void ResourceGainedAnimation(BuildModel buildModel, BuildView buildView, TweenCallback onEnd) 
    {
        var seq = DOTween.Sequence();
        seq
        .Append(buildView.transform.DOScale(.75f, .3f))
        .AppendCallback(() =>
        {
            for (int gold = 0; gold < buildModel.ResourceGain.gold; gold++)
            {
                ParticleAnimation(BuildViewAnimation.goldParticlePrefab, 
                    () => 
                    {
                        shopPresenter.AddResource(1, 0);
                    },
                    buildView.transform.position, shopPresenter.GoldViewTransform.transform.position);
            }

            for (int gem = 0; gem < buildModel.ResourceGain.gem; gem++)
            {
                ParticleAnimation(BuildViewAnimation.gemParticlePrefab, 
                    () =>
                    {
                        shopPresenter.AddResource(0, 1);
                    },
                    buildView.transform.position, shopPresenter.GemViewTransform.transform.position);
            }
        });
        seq.AppendCallback(onEnd);
        seq.Append(buildView.transform.DOScale(1, .1f));
    }

    private void ParticleAnimation(GameObject prefab, TweenCallback onEnd,
        in Vector3 spawnPos, in Vector3 targetPos) 
    {
        var go = GameObject.Instantiate(prefab, spawnPos, Quaternion.identity, canvas.transform);
        var pos = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        pos = pos.normalized * UnityEngine.Random.Range(80f, 95f);

        DOTween.Sequence()
        .Append(go.transform.DOMove(pos, .1f).SetRelative(true))
        .AppendInterval(.15f)
        .Append(go.transform.DOJump(targetPos, 1, 1, 1.2f).SetEase(Ease.OutQuad))
        .AppendCallback(() => GameObject.Destroy(go.gameObject))
        .OnComplete(onEnd);
        ;
    }

    private void OnChangedProgress(BuildProp buildProp) 
    {
        var ratio = buildProp.SpentTime / buildModels[buildProp.sName].ResourceGenerationDuration;
        int i = buildProps.IndexOf(buildProp);
        buildViews[i].SetProgressionRatio(ratio);
    }
}
