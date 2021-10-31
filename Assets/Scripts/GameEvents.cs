using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private CinemachineVirtualCamera _spawnCamera;
    public bool levelSuccess = false;
    [SerializeField] private GameObject _successText;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private GameObject _winScreen;
    private void Start()
    {
        PlayerPrefs.SetInt("currentlevel", PlayerPrefs.GetInt("currentlevel", 0) + 1);
        _cursorTexture.width /= 12;
        _cursorTexture.height /= 12;
        Cursor.SetCursor(_cursorTexture, new Vector2(0,0), CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Esq pressed");
            if (GameStateManager.currentState == GameStateManager.States.MiniGame || GameStateManager.currentState == GameStateManager.States.Running)
            {
                PauseGame();
            }
            
            else if (GameStateManager.currentState == GameStateManager.States.Pause)
            {
                ResumeGame();
            }
            
        }
    }

    public void OpenDoor(bool onDeath)
    {
        
        if (onDeath)
        {
            _spawnCamera.Priority = 30;
        }
        _doorAnimator.SetTrigger("OpenDoor");

        if (levelSuccess && onDeath)
        {
            levelSuccess = false;
            CloseDoor();
            PlayerPrefs.SetString("daywon", "yes");
            GameStateManager.currentState = GameStateManager.States.GameOver;
            _winScreen.SetActive(true);
        }
    }

    public void CloseDoor()
    {
        if (!levelSuccess)
        {
            _doorAnimator.SetTrigger("CloseDoor");
            _spawnCamera.Priority = 0;
        }
    }

    public void PauseGame()
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().SetSliderVals();
        GameStateManager.prePauseState = GameStateManager.currentState;
        GameStateManager.ChangeState(GameStateManager.States.Pause);
        
        _pauseMenu.SetActive(true);
       
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        GameStateManager.ChangeState(GameStateManager.prePauseState);
        
    }
}
