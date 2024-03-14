using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartView : MonoBehaviour
{
    public event Action Click;

    [SerializeField] Button restartButton;

    public Button RestartButton => restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() => Click?.Invoke());
    }
}
