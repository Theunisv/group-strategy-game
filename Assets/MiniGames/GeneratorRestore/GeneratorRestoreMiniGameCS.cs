using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneratorRestoreMiniGameCS : MonoBehaviour
{
    [SerializeField] private GameObject _genLight1;
    [SerializeField] private GameObject _genLight2;
    [SerializeField] private GameObject _genLight3;
    [SerializeField] private GameObject _genLight4;
    
    [SerializeField] private GameObject _genSwitch1;
    [SerializeField] private GameObject _genSwitch2;
    [SerializeField] private GameObject _genSwitch3;
    [SerializeField] private GameObject _genSwitch4;

    private bool isActive = false;
    [SerializeField] private Sprite _lightOff;
    

    private void OnEnable()
    {
        isActive = true;

        int randomChance = Random.Range(0, 100);
        if (randomChance > 50)_genSwitch1.GetComponent<Switch>().ToggleSwitch();
        randomChance = Random.Range(0, 100);
        if (randomChance > 50)_genSwitch2.GetComponent<Switch>().ToggleSwitch();
        randomChance = Random.Range(0, 100);
        if (randomChance > 50)_genSwitch3.GetComponent<Switch>().ToggleSwitch();
        randomChance = Random.Range(0, 100);
        if (randomChance > 50)_genSwitch4.GetComponent<Switch>().ToggleSwitch();
        
        
        _genLight1.GetComponent<GenLight>().lightIsOn = false;
        _genLight1.GetComponent<Image>().sprite = _lightOff;
        _genLight2.GetComponent<GenLight>().lightIsOn = false;
        _genLight2.GetComponent<Image>().sprite = _lightOff;
        _genLight3.GetComponent<GenLight>().lightIsOn = false;
        _genLight3.GetComponent<Image>().sprite = _lightOff;
        _genLight4.GetComponent<GenLight>().lightIsOn = false;
        _genLight4.GetComponent<Image>().sprite = _lightOff;
    }

    private void Update()
    {
        if (isActive)
        {
            int lightsOnCount = 0;
            int switchesOn = 0;

            if (_genLight1.GetComponent<GenLight>().lightIsOn) lightsOnCount++;
            if (_genLight2.GetComponent<GenLight>().lightIsOn) lightsOnCount++;
            if (_genLight3.GetComponent<GenLight>().lightIsOn) lightsOnCount++;
            if (_genLight4.GetComponent<GenLight>().lightIsOn) lightsOnCount++;

            if (_genSwitch1.GetComponent<Switch>().isSwitchUp) switchesOn++;
            if (_genSwitch2.GetComponent<Switch>().isSwitchUp) switchesOn++;
            if (_genSwitch3.GetComponent<Switch>().isSwitchUp) switchesOn++;
            if (_genSwitch4.GetComponent<Switch>().isSwitchUp) switchesOn++;

            if (lightsOnCount == 4 && switchesOn == 4)
            {
                isActive = false;
                GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasSuccessful();
            }


        }
    }
}
