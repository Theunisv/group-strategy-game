using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed = 6;
    [SerializeField] private float _rotSpeed = 4;
    private Vector3 _spawnPos;
    private Vector3 _velocity;
    private const float Gravity = -2;
    private bool dead = false;
    [SerializeField] private List<GameObject> _miniGameConsoles = new List<GameObject>();
    [SerializeField] private GameObject _interactionText;
    private bool _inRangeToInteract = false;
    private MiniGameManager _miniGameManager;
    private Animator _animController;
    [SerializeField] private CinemachineVirtualCamera _spawnCamera;
    private GameObject consoleClosestToUser;


    private Vector3 textPanelOriginalPos;
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private TextMeshProUGUI _textBody;
    [SerializeField] private List<string> _charDialogue;

    private void Start()
    {
        _animController = GetComponent<Animator>();
        _spawnPos = _characterController.transform.position;
        if (!SceneManager.GetActiveScene().name.Contains("Intro"))
        {
            textPanelOriginalPos = _textPanel.transform.position; 
            _miniGameManager = GameObject.Find("MiniGames").GetComponent<MiniGameManager>();
            StartCoroutine(UtterRandomDialogue());
        }
        GameStateManager.ChangeState(GameStateManager.States.Running);
        HideInteractionText();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.IsState(GameStateManager.States.Running))
        {
            Move();
            Rotate();
            MiniGameCheck();
        }

        if (!SceneManager.GetActiveScene().name.Contains("Intro"))
        {
            if (_inRangeToInteract)
            {
                if (Input.GetKeyUp(KeyCode.F) && GameStateManager.IsState(GameStateManager.States.Running))
                {
                    GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayButtonSound();
                    GameStateManager.ChangeState(GameStateManager.States.MiniGame);
                    _miniGameManager.OpenRandomMiniGame();
                    _miniGameManager.StartInteractionWithConsole(consoleClosestToUser);
                    HideInteractionText();
                }
            }
        }
    }

    private void Move()
    {
        Quaternion rotation = _characterController.transform.rotation;
       
        float vertMove = Input.GetAxis("Vertical");
        if (vertMove > 0)
        {
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayRunningSound();
            _animController.SetBool("Walking", true);
            //_characterController.transform.forward = new Vector3(0, rotation.y, 0);

            Vector3 move =  _characterController.transform.forward * vertMove;
            _characterController.Move(_speed * Time.deltaTime * move);
        }
        else
        {
            //GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayRunningSound(false);
            _animController.SetBool("Walking", false);
        }

        if (!SceneManager.GetActiveScene().name.Contains("Intro"))
        {
            _velocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        if (dead)
        {
            
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayFailSound();
            _characterController.transform.position = _spawnPos;
            //dead = false;
        }
    }
    
    private void Rotate()
    {
        float horRot = Input.GetAxis("Horizontal");
        
        _characterController.transform.Rotate(0, horRot * _rotSpeed * Time.deltaTime,0);
        _characterController.transform.localRotation = Quaternion.Euler(_characterController.transform.localEulerAngles);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.name.Contains("DeathZone") && !dead)
        {
            PlayerPrefs.SetInt("fallstotal", PlayerPrefs.GetInt("fallstotal", 0) + 1);
            dead = true;
            _miniGameManager.MeltDownLevel += 0.02f;
            _spawnCamera.Priority = 30;
        }
    }
    
    private void OnTriggerExit(Collider hit)
    {
        if (hit.name.Contains("DeathZone"))
        {
            dead = false;
        }
    }



    private void MiniGameCheck()
    {
        foreach (var console in _miniGameConsoles)
        {
            //Debug.Log(Vector3.Distance(transform.position, console.transform.position));
            if (Vector3.Distance(transform.position, console.transform.position) > 1.2f)
            {
                _inRangeToInteract = false;
                consoleClosestToUser = null;
            }
            else
            {
                if (console.GetComponent<Console>().IsBroken)
                {
                    _inRangeToInteract = true;
                    consoleClosestToUser = console;
                    break;
                }
                else
                {
                    _inRangeToInteract = false;
                }
                
            }
        }
        
        if (_inRangeToInteract)
        {
            ShowInteractionText();    
        }
        else
        {
            HideInteractionText();
        }

    }

    private void ShowInteractionText()
    {
        _interactionText.SetActive(true);
        //Debug.Log("Trying to show interaction");
    }
    
    private void HideInteractionText()
    {
        _interactionText.SetActive(false);
        
    }

    IEnumerator UtterRandomDialogue()
    {
        int randomWaitTime = Random.Range(20, 35);
        yield return new WaitForSeconds(randomWaitTime);
        
        int randomNum = Random.Range(0, _charDialogue.Count);
        _textPanel.SetActive(true);
        _textBody.text = "Balding Scientist\n\n";
        foreach (char c in _charDialogue[randomNum]) 
        {
            _textBody.text += c;
            yield return new WaitForSeconds (0.02f);
        }
        
        yield return new WaitForSeconds(3f);
        _textBody.text = "";
        while (_textPanel.GetComponent<Image>().fillAmount > 0.0f)
        {
            _textPanel.GetComponent<Image>().fillAmount -= 0.1f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _textPanel.SetActive(false);
        _textPanel.GetComponent<Image>().fillAmount = 1.0f;
        
        StartCoroutine(UtterRandomDialogue());
    }
}
