using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _clockHour;
    [SerializeField] private TextMeshPro _clockElipse;
    [SerializeField] private TextMeshPro _clockMinute;
    [SerializeField] private Light _clockLight;

    private float _lightIntensity = 3.0f;
    private bool _lightGrowing = true;

    public int _hour = 08;
    private int _minute = 0;

    private float _timeSpeed = 0.25f;//0.25
    private float _elipseSpeed = 0.5f;

    private string _sHour;
    private string _sMinute;

    private void Start()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Intro"))
        {
            InvokeRepeating("TimeTick", 1, _timeSpeed);
        }
            InvokeRepeating("FlashElipse", 1, _elipseSpeed);
    }

    private void Update()
    {
        if (_lightGrowing)
        {
            if (_lightIntensity <= 6.0f)
            {
                _lightIntensity += 0.2f;
            }
            else
                _lightGrowing = false;
        }

        else
        {
            if (_lightIntensity >= 0.2f)
            {
                _lightIntensity -= 0.2f;
            }
            else
                _lightGrowing = true;
        }

        _clockLight.intensity = _lightIntensity;
    }

    private void TimeTick()
    {
        if (_minute < 60)
        {
            _minute++;
        }
        
        else
        {
            _minute = 0;
            _hour++;
        }
        
        _sHour = AddLeadingZero(_hour);
        _sMinute = AddLeadingZero(_minute);

        _clockHour.SetText(_sHour);
        _clockMinute.SetText(_sMinute);
    }

    private void FlashElipse()
    {
        if (_clockElipse.text.Equals(":"))
            _clockElipse.SetText(" ");
        else
            _clockElipse.SetText(":");
    }


    private void FlashClockLight()
    {
        if (_lightIntensity == 3f)
        {
            _lightIntensity = 1.5f;
        }
        else
        {
            _lightIntensity = 3f;
        }
        _clockLight.range = _lightIntensity;
    }
    private string AddLeadingZero(int time)
    {
        if (time < 10)
        {
            return "0" + time;
        }

        return time.ToString();
    }
    
    
}
