using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transTime = 1f;
    public AudioSource musicPlayer;
    
    public void LoadLevel1()
    {
        StartCoroutine(LoadLevel(1));
    }
    
    public void LoadAIScene()
    {
        StartCoroutine(LoadLevel(3));
    }
    
    public void LoadTrainingScene()
    {
        StartCoroutine(LoadLevel(4));
    }

    private void Awake()
    {
        StartCoroutine(RevealScene());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        while (this.GetComponent<Image>().fillAmount < 1.0f)
        {
            GetComponent<Image>().fillAmount += 0.01f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator RevealScene()
    {
        yield return new WaitForSeconds(2f);
        while (this.GetComponent<Image>().fillAmount > 0.0f)
        {
            GetComponent<Image>().fillAmount -= 0.01f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }


    public void LoadLevelWithIndex(int index)
    {
        StartCoroutine(LoadLevel(index));
    }
}
