using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Console : MonoBehaviour
{
    private bool _isBroken;
    [SerializeField] private GameObject _warningSign;
    [SerializeField] private Light _consoleLight;

    public bool IsBroken
    {
        get => _isBroken;
        set => _isBroken = value;
    }

    public GameObject WarningSign
    {
        get => _warningSign;
        set => _warningSign = value;
    }

    public Light ConsoleLight
    {
        get => _consoleLight;
        set => _consoleLight = value;
    }
}
