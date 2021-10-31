using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenLight : MonoBehaviour
{
    public bool lightIsOn;
    [SerializeField] private Sprite _lightOffImage;
    [SerializeField] private Sprite _lightOnImage;

    public void ToggleLight()
    {
        if (lightIsOn)
        {
            this.GetComponent<Image>().sprite = _lightOffImage;
            lightIsOn = false;
        }
        else
        {
            int chanceToSwitchOn = Random.Range(0, 101);
            if (chanceToSwitchOn > 60)
            {
                GameObject.Find("AudioSources").GetComponent<AudioManager>().PlaySuccessSound();
                this.GetComponent<Image>().sprite = _lightOnImage;
                lightIsOn = true;
            }
        }
    }
    
}
