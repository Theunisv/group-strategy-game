using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartCreditRoll());
    }

    IEnumerator StartCreditRoll()
    {
        float secondCount = 0;
        while (secondCount < 25f)
        {
            transform.Translate(0,1,0);
            yield return new WaitForSeconds(Time.deltaTime);
            secondCount += Time.deltaTime;
        }
        
        Application.Quit();
    }
}
