using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class IntroEvents : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _cameraList;
    List<int> _transitionQueueIndex = new List<int>{1,11,15,18,19,21};
  

    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private List<string> _dialogLines;
    private int _dialogIndex = 0;
    private int _cameraIndex = 0;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private Texture2D _cursorTexture;

    string _currentLine;

    private void Awake()
    {
        PlayerPrefs.SetInt("currentlevel", 0);
        PlayerPrefs.SetInt("tasktotal",0);
        PlayerPrefs.SetInt("failstotal",0);
        PlayerPrefs.SetInt("fallstotal",0);
        _cursorTexture.width /= 12;
        _cursorTexture.height /= 12;
        Cursor.SetCursor(_cursorTexture, new Vector2(0,0), CursorMode.Auto);
        if (!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1f);
        }
        GameStateManager.MusicVolume = PlayerPrefs.GetFloat("musicvolume");
        
        if (!PlayerPrefs.HasKey("sfxvolume"))
        {
            PlayerPrefs.SetFloat("sfxvolume", 1f);
        }
        GameStateManager.SFXVolume = PlayerPrefs.GetFloat("sfxvolume");
        PlayerPrefs.Save();
    }

    public void LoadIntro()
    {
        _menuButtons.SetActive(false);
        _dialogIndex = 0;
        _cameraIndex = 0;
        _dialogPanel.SetActive(true);
        _dialogWindow.SetActive(true);
        _nextButton.SetActive(false);
        _currentLine = _dialogLines[_dialogIndex];
        
        _dialogText.text = "Balding Scientist\n\n";;

        // TODO: add optional delay when to start
        StartCoroutine ("PlayText");
    }

    public void LoadNextLine()
    {
        _dialogIndex++;
        Debug.Log(_dialogIndex);
        
        if (_transitionQueueIndex.Contains(_dialogIndex))
        {
            Debug.Log("Index found!");
            if (_dialogIndex < 20)
            {
                _cameraIndex++;
                _cameraList[_cameraIndex].Priority = 50;
                _cameraList[_cameraIndex-1].Priority = 30;
            }
            else
            {
                _cameraIndex = 0;
                _cameraList[_cameraIndex].Priority = 50;
                _cameraList[4].Priority = 30;
            }
        }

        if (_dialogIndex >= _dialogLines.Count)
        {
            _menuButtons.SetActive(true);
            _dialogWindow.SetActive(false);
            _dialogPanel.SetActive(false);
        }
        else
        {
            _currentLine = _dialogLines[_dialogIndex];
        
            _dialogText.text = "Balding Scientist\n\n";;

            // TODO: add optional delay when to start
            StartCoroutine ("PlayText");
        }
        
    }
    IEnumerator PlayText()
    {
        Debug.Log(_currentLine);
        foreach (char c in _currentLine) 
        {
            _dialogText.text += c;
            yield return new WaitForSeconds (0.02f);
        }

        _nextButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
