using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldView;
    [SerializeField] TextMeshProUGUI gemView;

    public void SetResource(in Resource resource)
    {
        goldView.text = resource.gold.ToString();
        gemView.text = resource.gem.ToString();
    }
}
