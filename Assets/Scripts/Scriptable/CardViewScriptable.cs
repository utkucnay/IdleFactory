using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine;

[CreateAssetMenu(fileName = "CardViewScriptable", menuName = "Card/CardViewScriptable", order = 0)]
public class CardViewScriptable : ScriptableObject 
{
    public float scaleFactor;
    public Ease enterScaleEase;
    public Ease exitScaleEase;
    public float enterTime;
    public float exitTime;
}
