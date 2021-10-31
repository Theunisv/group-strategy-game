using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using System.Globalization;
using System.IO;

public class PickUp : MonoBehaviour
{
    public Text collectText;
    public Text winText;
    private int count;
    //public DeathMenue theDeathScreen;
   // public WinMenue theWinMenu;
   // private AudioSource audioCollect;
   // public AudioClip Collect;

    // Use this for initialization
    void Start()
    {
        count = 0;
        Collect();
    }

    void Update()
    {
     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            Collect();
           // audioCollect = GetComponent<AudioSource>();
          //  audioCollect.clip = Collect;
           // audioCollect.Play();
        }
    }

    void Collect()
    {
        collectText.text = "xxx : " + count.ToString() + "/10";
        if (count >= 10)
        {
          //  PauseGame();
           // theWinMenu.gameObject.SetActive(true);
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("PickUp");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
        }
    }
}
