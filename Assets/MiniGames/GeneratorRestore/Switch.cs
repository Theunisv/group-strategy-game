using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public bool isSwitchUp;
    [SerializeField] private Sprite _switchUpImg;
    [SerializeField] private Sprite _switchDownImg;
    [SerializeField] private GameObject _switchLight;
    [SerializeField] private Sprite _lightOnImg;
    [SerializeField] private Sprite _lightOffImg;

    public bool canSwitch = true;

    public void ToggleSwitch()
    {
        if (canSwitch)
        {
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlaySwitchSound();
            if (isSwitchUp)
            {
                isSwitchUp = false;
                GetComponent<Image>().sprite = _switchDownImg; 
                transform.Translate(0, -36.9f, 0);
                //_switchLight.GetComponent<GenLight>().ToggleLight();
            }
            else
            {
                isSwitchUp = true;
                GetComponent<Image>().sprite = _switchUpImg; 
                transform.Translate(0, 36.9f, 0);
                _switchLight.GetComponent<GenLight>().ToggleLight();
            }
        }
    }
}
