using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinscreenCS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayTitle;
    [SerializeField] private TextMeshProUGUI _dayTasksText;
    [SerializeField] private TextMeshProUGUI _dayFailsText;
    [SerializeField] private TextMeshProUGUI _dayFallsText;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _pbTasks, _pbDays, _pbFalls;
    

    private void OnEnable()
    {
        GameStateManager.currentState = GameStateManager.States.GameOver;
        if (PlayerPrefs.GetString("daywon").Equals("yes"))
        {
            _dayTitle.text = "Day " + PlayerPrefs.GetInt("currentlevel") + " completed";
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayVictoryWinscreen();
            
        }
        else
        {
            _dayTitle.text = "Day " + PlayerPrefs.GetInt("currentlevel") + " Failed";    
            _nextButton.SetActive(false);
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayGameOverSound();
        }

        _dayTasksText.text = PlayerPrefs.GetInt("tasktotal").ToString();
        _dayFailsText.text = PlayerPrefs.GetInt("failstotal").ToString();
        _dayFallsText.text = PlayerPrefs.GetInt("fallstotal").ToString();
        

        if (PlayerPrefs.GetInt("currentlevel") > PlayerPrefs.GetInt("pbdays", 0))
        {
            _pbDays.SetActive(true);
            PlayerPrefs.SetInt("pbdays", PlayerPrefs.GetInt("currentlevel"));
        }
        else
        {
            _pbDays.SetActive(false);  
        }
        if (PlayerPrefs.GetInt("tasktotal") > PlayerPrefs.GetInt("pbsuccess", 0))
        {
            _pbTasks.SetActive(true);
            PlayerPrefs.SetInt("pbsuccess", PlayerPrefs.GetInt("tasktotal"));
        }
        else
        {
            _pbTasks.SetActive(false);  
        }
        if (PlayerPrefs.GetInt("fallstotal") > PlayerPrefs.GetInt("pbfalls", 0))
        {
            _pbFalls.SetActive(true);
            PlayerPrefs.SetInt("pbfalls", PlayerPrefs.GetInt("fallstotal"));
        }
        else
        {
            _pbFalls.SetActive(false);  
        }
        
        PlayerPrefs.Save();
    }
}
