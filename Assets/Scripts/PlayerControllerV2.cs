using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using System.Globalization;
using System.IO;

public class PlayerControllerV2 : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Text countText;
    public Text winText;
    public SphereCollider col;
    public Vector2 jumpHeight;
    private int count;
    public int curHealth;
    public string seconds;
    public int maxHealth = 5;
    //public DeathMenue theDeathScreen;
    //public WinMenue theWinMenu;
    private AudioSource audioCollect;
    public AudioClip Collect;
    private AudioSource audioJump;
    public AudioClip Jump;
    private AudioSource audioDamage;
    public AudioClip DamageSound;
    public Text timerText;
    private float startTime;
   

    // rough player code foundations //

    void Start()
    {
      //  ContinueGame();
      //  rb = GetComponent<Rigidbody>();
     //   col = GetComponent<SphereCollider>();
        count = 0;
        SetCountText();
        curHealth = maxHealth;
    }

    public void Damage(int dmg)
    {
        if (curHealth < dmg) { dmg = curHealth; }
        curHealth -= dmg;

        audioDamage = GetComponent<AudioSource>();
        audioDamage.clip = DamageSound;
        audioDamage.Play();
    }

    void Update()
    {

        float t = Time.time - startTime;
        seconds = (t % 60).ToString("f2");
//        timerText.text = "TIME : " + seconds;

      //  float moveHorizontal = Input.GetAxis("Horizontal");
       // float moveVertical = Input.GetAxis("Vertical");
     //   Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
     //   rb.AddForce(movement * speed);

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            audioCollect = GetComponent<AudioSource>();
            audioCollect.clip = Collect;
            audioCollect.Play();
        }
    }

    void SetCountText()
    {
       // countText.text = "Pick Up? : " + count.ToString() + "/10";
        if (count >= 10)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            PauseGame();
          //  theWinMenu.gameObject.SetActive(true);
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Pick Up");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
    }

    void Die()
    {
       // GameObject.FindGameObjectWithTag("Player").SetActive(false);
      //  PauseGame();
      //  theDeathScreen.gameObject.SetActive(true);
      //  GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Pick Up");
//
      //  foreach (GameObject go in gameObjectArray)
      //  {
     //       go.SetActive(false);
      //  }
      
    }
}
