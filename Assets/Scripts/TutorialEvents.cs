using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    [SerializeField] private GameObject _faultyIcons;
    [SerializeField] private GameObject _interactionText;
    [SerializeField] private GameObject _meltDownSlider;
    [SerializeField] private GameObject _miniGame;
    [SerializeField] private GameObject _console2;
    [SerializeField] private CinemachineVirtualCamera tutCamera;
    [SerializeField] private CinemachineVirtualCamera menuCamera;

    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private List<string> _dialogLines;
    private int _dialogIndex = 0;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _menuButtons;

    [SerializeField] private GameObject _playerModel;
    [SerializeField] private GameObject _playerCharacter;

    private bool userPromptedToInteract = false;
    private void Update()
    {
        if (userPromptedToInteract)
        {
            _faultyIcons.SetActive(true);
            _miniGame.SetActive(true);
            _interactionText.SetActive(true);
            if (Input.GetKeyUp(KeyCode.F))
            {
                LoadNextLine();
                userPromptedToInteract = false;
                _interactionText.SetActive(false);
                GameStateManager.ChangeState(GameStateManager.States.MiniGame);
                _miniGame.GetComponent<MiniGameManager>().StartInteractionWithConsole(_console2);
                _miniGame.GetComponent<MiniGameManager>().OpenRandomMiniGame();
                //_miniGame.
            }
        }
    }

    string _currentLine;

    private void PromptToWalk()
    {
        _playerModel.SetActive(true);
       _playerCharacter.GetComponent<PlayerControls>().enabled = true;
    }
    public void LoadTutorial()
    {
        menuCamera.Priority = 30;
        tutCamera.Priority = 50;
        _menuButtons.SetActive(false);
        _dialogIndex = 0;
        _dialogPanel.SetActive(true);
        _dialogWindow.SetActive(true);
        _nextButton.SetActive(false);
        _currentLine = _dialogLines[_dialogIndex];
        PromptToWalk();
        
        _dialogText.text = "Balding Scientist\n\n";;

        // TODO: add optional delay when to start
        StartCoroutine ("PlayText");
    }

    public void LoadNextLine()
    {
        _dialogIndex++;
        
        if (_dialogIndex == 1)
        {
            userPromptedToInteract = true;
        }

        if (_dialogIndex == 3)
        {
            userPromptedToInteract = false;
            _miniGame.SetActive(false);
            _meltDownSlider.SetActive(true);
        }

        if (_dialogIndex >= _dialogLines.Count)
        {
            menuCamera.Priority = 50;
            tutCamera.Priority = 30;
            
            _dialogWindow.SetActive(false);
            _dialogPanel.SetActive(false);
            _faultyIcons.SetActive(false);
            _meltDownSlider.SetActive(false);
            
            StartCoroutine(TutFinished());

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

        if (_dialogIndex != 1 && _dialogIndex != 2)
        {
            _nextButton.SetActive(true);
        }
        
    }

    IEnumerator TutFinished()
    {
        yield return new WaitForSeconds(2f);
        
        _playerModel.SetActive(false);
        _menuButtons.SetActive(true);

    }
}
