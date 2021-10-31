using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTimer : MonoBehaviour
{
    float timer;
    float randomtime; //random time for player to stop at.
    float seconds;
    float minutes;
    float timespeed = 2f; //time multi-speed if needed
    string passed = "Passed!";
    string failed = "Failed!";
    bool start;
 
    [SerializeField] Text StopTimeUI; //StopTime UI element //Needs implementation

    [SerializeField] Text RandomTimeUI; //RandomTime UI element //Needs implementation

    [SerializeField] Text Waiting; //pass or fail UI

    //UI Buttons: Start : Stop : Reset/Exit

    // if player clicks on stopTimer button, and foat timer = float randomtime, then win-condition, else loose.

    void Start()
    {
        start = false;
        timer = 0;
        randomtime = Random.Range(2, 11); //2 second to 11 seconds - random goal.
        RandomTimeUI.text = randomtime.ToString("Digit: 00");
    }


    void Update()
    {
        StopWatchCalc();
    }


    void StopWatchCalc()
    {
        if(start)
        {
            timer += Time.deltaTime; //can speed up time so it ticks faster .
            seconds = (int)timer % 60; 
            minutes = (int)((timer / 60) % 60);

            StopTimeUI.text = seconds.ToString("Timer: 00");
        }
    }


    public void randomTime()
    {
        randomtime = Random.Range(1, 15);
        RandomTimeUI.text = randomtime.ToString("Digit: 00");
    }


    public void startTimer()
    {
        start = true;
      //  randomTime();
    }


    public void stopTimer()
    {
        start = false;
        //  randomTime();
        {
            if (seconds == randomtime)
                print("correct number!");
         //   Waiting.text = passed.ToString();

            else
                 print("wrong number!");
               // Waiting.text = failed.ToString();
        }
    }


    public void resetTimer()
    {
        start = false;
        timer = 0;
        randomTime();
        StopTimeUI.text = "00:00";
    }
}
