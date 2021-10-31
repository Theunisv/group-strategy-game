using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Hover()
    {
        _text.color = Color.green;
    }

    public void StopHover()
    {
        _text.color = Color.white;
    }

    public void Click()
    {
        _text.color = new Color(0f,0.1f,0f,1f);
    }
}
