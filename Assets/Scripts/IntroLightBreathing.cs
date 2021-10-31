using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLightBreathing : MonoBehaviour
{
    public float intensityMax = 4.5f;
    public float intensityMin = 1.8f;
    public float interval = 0.05f;
    private bool increasing = true;
    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("Breath", 0f,interval);
    }

    private void Breath()
    {
        if (GetComponent<Light>().intensity <= intensityMax && increasing)
        {
            GetComponent<Light>().intensity += 0.3f;
        }
        else
        {
            increasing = false;
        }

        if (GetComponent<Light>().intensity >= intensityMin && !increasing)
        {
            GetComponent<Light>().intensity -= 0.3f;
        }
        else
        {
            increasing = true;
        }
    }
}
