using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceModel
{
    public event Action ResourceChanged;

    public ResourceModel(int gold, int gem)
    {
        currentResource.gold = gold;
        currentResource.gem = gem;
    }

    private Resource currentResource;
    private Resource maxResource = new Resource() { gold = 999, gem = 999 };
    private Resource minResource = new Resource() { gold = 0, gem = 0 };

    public Resource CurrentResource { get => currentResource; }
    public Resource MaxResource { get => maxResource; }
    public Resource MinResource { get => minResource; }

    public void IncreaseResource(Resource amount)
    {
        currentResource.gold += amount.gold;
        currentResource.gem += amount.gem;        

        currentResource.gold = Mathf.Clamp(currentResource.gold, minResource.gold, maxResource.gold);
        currentResource.gem = Mathf.Clamp(currentResource.gem, minResource.gem, maxResource.gem);
        UpdateResource();
    }

    public void DecreaseResource(Resource amount)
    {
        currentResource.gold -= amount.gold;
        currentResource.gem -= amount.gem;        

        currentResource.gold = Mathf.Clamp(currentResource.gold, minResource.gold, maxResource.gold);
        currentResource.gem = Mathf.Clamp(currentResource.gem, minResource.gem, maxResource.gem);
        UpdateResource();
    }

    public void UpdateResource()
    {
        ResourceChanged?.Invoke();
    }
}
