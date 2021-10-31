using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelFillCS : MonoBehaviour
{
    [SerializeField] private Image _barrelFillImage;
    private bool _calledSuccess = false;
    private void Update()
    {
        if (_barrelFillImage.fillAmount == 1.0f && !_calledSuccess)
        {
            _calledSuccess = true;
            CancelInvoke("StartDepleting");
            GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasSuccessful();
        }
    }

    private void Start()
    {
        _barrelFillImage.fillAmount = 0.0f;
        InvokeRepeating("StartDepleting", 1 ,0.4f);
    }

    private void OnEnable()
    {
        _calledSuccess = false;
        _barrelFillImage.fillAmount = 0.0f;
        InvokeRepeating("StartDepleting", 1 ,0.4f);
    }

    private void StartDepleting()
    {
        if (_barrelFillImage.fillAmount > 0)
        {
            _barrelFillImage.fillAmount -= 0.04f;
        }
    }

    public void IncreaseFill()
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayButtonSound();
        _barrelFillImage.fillAmount += 0.05f;
    }

    private void OnDisable()
    {
        CancelInvoke("StartDepleting");
        _barrelFillImage.fillAmount = 0.0f;
    }
}
