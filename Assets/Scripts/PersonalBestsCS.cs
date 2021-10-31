using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonalBestsCS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _successesText;
    [SerializeField] private TextMeshProUGUI _daysText;
    [SerializeField] private TextMeshProUGUI _fallsText;

    private void OnEnable()
    {
        _successesText.text = PlayerPrefs.GetInt("pbsuccess", 0).ToString();
        _daysText.text = PlayerPrefs.GetInt("pbdays", 0).ToString();
        _fallsText.text = PlayerPrefs.GetInt("pbfalls", 0).ToString();
    }
}
