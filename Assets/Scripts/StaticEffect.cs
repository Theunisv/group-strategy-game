using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEffect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(fadeOut());
    }

    private IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
