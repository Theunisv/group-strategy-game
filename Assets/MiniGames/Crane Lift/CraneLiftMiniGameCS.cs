using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CraneLiftMiniGameCS : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rodPositions;
    [SerializeField] private GameObject _rod;
    [SerializeField] private GameObject _crane;
    
    private Vector3 originalCranePos;
    [SerializeField] private GameObject _craneExtensionParts;
    private Vector3 originalPosCraneExtension;
    [SerializeField] private GameObject _craneArm;
    private Vector3 originalPosCraneArm;
    private Vector3 originalLocalScaleCraneArm;
    
    [SerializeField] private RectTransform _craneMin;
    [SerializeField] private RectTransform _craneMax;
    [SerializeField] private GameObject _extendLimit;
    
    private bool _craneMovingToMax = true;
    int _rodPosIndex = 0;
    private float _craneSpeed = 4f;
    private bool _checkIfAlligned = false;
    private void OnEnable()
    {
        _checkIfAlligned = false;
        _craneMovingToMax = true;
        _crane.transform.position = originalCranePos;
        _craneExtensionParts.transform.position = originalPosCraneExtension;
        _craneArm.transform.position = originalPosCraneArm;
        _craneArm.transform.localScale = originalLocalScaleCraneArm;
        
        _rodPosIndex = Random.Range(0, _rodPositions.Count);
        _rod.transform.position = _rodPositions[_rodPosIndex].transform.position;
        _craneMovingToMax = true;
        InvokeRepeating("StartCraneMovement", 1, 0.08f);
    }

   private void Awake()
    {
        originalCranePos = _crane.transform.position;
        originalPosCraneExtension = _craneExtensionParts.transform.position; 
        originalPosCraneArm = _craneArm.transform.position;
        originalLocalScaleCraneArm = _craneArm.transform.localScale;
    }

    private void StartCraneMovement()
    {
        if (_craneMovingToMax && _crane.transform.position.x < _craneMax.position.x)
        {
            //_crane.transform.Translate(3f,0,0); 
        }
    }

    public void CheckAllignment()
    {
        _checkIfAlligned = true;
        
        StartCoroutine(ExtendArm());
    }

    private IEnumerator ExtendArm()
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayButtonSound();
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelPullSound();
        while (_craneExtensionParts.transform.position.y > _extendLimit.transform.position.y)
        {
            _craneExtensionParts.transform.Translate(0,-2f, 0);
            _craneArm.transform.localScale = new Vector3(_craneArm.transform.localScale.x,_craneArm.transform.localScale.y + 0.08f,_craneArm.transform.localScale.z);
            _craneArm.transform.Translate(0,1f, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (_crane.transform.position.x >= _rod.transform.position.x - 20f && _crane.transform.position.x <= _rod.transform.position.x + 20f) 
        {
            GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasSuccessful();
        }

        else
        {
            GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasClosed();    
        }
    }

    private void Update()
    {
        if (!_checkIfAlligned)
        {
            _rod.transform.position = _rodPositions[_rodPosIndex].transform.position;
            
            if (_craneMovingToMax && _crane.transform.position.x < _craneMax.position.x)
            {
                _crane.transform.Translate(_craneSpeed,0,0); 
            }
            else
            {
                _craneMovingToMax = false;
            }
            
            if (!_craneMovingToMax && _crane.transform.position.x > _craneMin.position.x)
            {
                _crane.transform.Translate(-_craneSpeed,0,0); 
            }
            else
            {
                _craneMovingToMax = true;
            }
        }
    }
}
