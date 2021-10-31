using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _miniGames = new List<GameObject>();
    private GameObject _openTask;
    private int previousTaskIndex = 100;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _sucessText;
    [SerializeField] private GameObject _failureText;
    [SerializeField] private Slider _meltdownSlider;
    private float _meltDownLevel = 0.0f;
    [SerializeField] private List<GameObject> _consoles;
    [SerializeField] private List<GameObject> _faultyConsoles;
    private float _intervalBetweenBreaks = 20f;
    private float _minBreakTime = 15f;
    private Console _consolePlayerIsInteractingWith;
    [SerializeField] private List<GameObject> faultyIcons;

    [SerializeField] private ClockManager _gameClock;
    private bool _levelSuccess = false;
    [SerializeField] private GameEvents _gameEventManager;
    [SerializeField] private GameObject _getToTheExitText;
    [SerializeField] private GameObject _winScreen;

    public float passiveDailyDecay = 0.006f;
    private float currLevel;
    
    
    public float MeltDownLevel
    {
        get => _meltDownLevel;
        set => _meltDownLevel = value;
    }

    private void Start()
    {
        _minBreakTime = 15f - (currLevel / 2);
        _intervalBetweenBreaks = 20f - (currLevel / 2);
        currLevel = (float) (PlayerPrefs.GetInt("currentlevel"));
        passiveDailyDecay += currLevel / 1000;
        Debug.Log(passiveDailyDecay.ToString());
        _faultyConsoles.Clear();
        InvokeRepeating("PassiveMeltdownIncrease", 1, 1);
        StartCoroutine(StartMachineBreakingProcess());
    }

    private void PassiveMeltdownIncrease()
    {
        if (GameStateManager.IsState(GameStateManager.States.Running) || GameStateManager.IsState(GameStateManager.States.MiniGame))
        {
            _meltDownLevel += passiveDailyDecay;
        }
    }

    private void Update()
    {
        _meltdownSlider.value = _meltDownLevel;
        int index = 0;
        foreach (var console in _consoles)
        {
            if (!console.GetComponent<Console>().IsBroken)
            {
                faultyIcons[index].SetActive(false);
                _consoles[index].GetComponent<Console>().ConsoleLight.intensity = 0f;
            }
            else
            {
                faultyIcons[index].SetActive(true);
            }

            index++;
        }

        if (_meltDownLevel > 0.99f)
        {
            _failureText.SetActive(true);
            Debug.Log("Game over");
            PlayerPrefs.SetString("daywon", "no");
            GameStateManager.currentState = GameStateManager.States.GameOver;
            _winScreen.SetActive(true);
        }
        else
        {
            _failureText.SetActive(false);
        }

        if (_meltDownLevel > 0.90f && GameStateManager.currentState == GameStateManager.States.Running)
        {
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayNearingMeltdownAlarm();
        }
        
        if (_gameClock._hour >= 17 && GameStateManager.currentState == GameStateManager.States.Running)
        {
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelOverAlarm();
            _levelSuccess = true;
            _gameEventManager.levelSuccess = true;
            _getToTheExitText.SetActive(true);
            _gameEventManager.OpenDoor(false);
        }
        
    }

    public void OpenRandomMiniGame()
    {
        int nextTaskIndex = previousTaskIndex;
        while (nextTaskIndex == previousTaskIndex)
        {
            nextTaskIndex  = Random.Range(0, _miniGames.Count);
        }
        
        _openTask = _miniGames[nextTaskIndex];
        _openTask.SetActive(true);
        _closeButton.SetActive(true);
    }

    public void CloseCurrentTask()
    {
        Debug.Log("Closing");
        _openTask.SetActive(false);
        _closeButton.SetActive(false);
        GameStateManager.ChangeState(GameStateManager.States.Running);
    }

    public void TaskWasSuccessful()
    {
        _faultyConsoles.Remove(_consolePlayerIsInteractingWith.gameObject);
        _consolePlayerIsInteractingWith.IsBroken = false;
        _consolePlayerIsInteractingWith.WarningSign.SetActive(false);
        StartCoroutine(ShowSucessText());
        PlayerPrefs.SetInt("tasktotal", PlayerPrefs.GetInt("tasktotal", 0) + 1);
    }

    public void TaskFailed()
    {
        StartCoroutine(ShowFailureText());
        PlayerPrefs.SetInt("failstotal", PlayerPrefs.GetInt("failstotal", 0) + 1);
    }
    
    public void TaskWasClosed()
    {
        StartCoroutine(PrematureExit());
    }
    private IEnumerator ShowSucessText()
    {
        _meltDownLevel -= 0.05f;
        _closeButton.SetActive(false);
        _sucessText.SetActive(true);
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlaySuccessSound();
        yield return new WaitForSeconds(2.0f);
        _sucessText.SetActive(false);
        CloseCurrentTask();
        
        if (SceneManager.GetActiveScene().name.Contains("Intro"))
        {
            GameObject.Find("TutorialEvent").GetComponent<TutorialEvents>().LoadNextLine();
        }
    }
    
    private IEnumerator ShowFailureText()
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayFailSound();
        _meltDownLevel += 0.025f;
        _failureText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        _failureText.SetActive(false);
    }

    private IEnumerator PrematureExit()
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayFailSound();
        _meltDownLevel += 0.10f;
        _failureText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        _failureText.SetActive(false);
        CloseCurrentTask();
    }
    private IEnumerator StartMachineBreakingProcess()
    {
        float waitInterval = Random.Range(_minBreakTime, _intervalBetweenBreaks);
        BreakMachine();
        yield return new WaitForSeconds(waitInterval);
        StartCoroutine(StartMachineBreakingProcess());
    }

    private void BreakMachine()
    {
        if (_faultyConsoles.Count < _consoles.Count)
        {
            int randomConsoleIndex = Random.Range(0, _consoles.Count);
            while (_consoles[randomConsoleIndex].GetComponent<Console>().IsBroken)
            {
                randomConsoleIndex = Random.Range(0, _consoles.Count);
            }
            
            _faultyConsoles.Add(_consoles[randomConsoleIndex]);
            _consoles[randomConsoleIndex].GetComponent<Console>().IsBroken = true;
            _consoles[randomConsoleIndex].GetComponent<Console>().WarningSign.SetActive(true);
            _consoles[randomConsoleIndex].GetComponent<Console>().ConsoleLight.intensity = 50f;
            faultyIcons[randomConsoleIndex].SetActive(true);
        }
        else
        {
            _meltDownLevel += passiveDailyDecay + 0.002f;
        }
        
        
    }
    public void StartInteractionWithConsole(GameObject console)
    {
        _consolePlayerIsInteractingWith = console.GetComponent<Console>();
    }
}
