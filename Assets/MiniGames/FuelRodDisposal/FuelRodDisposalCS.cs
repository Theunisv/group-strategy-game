using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FuelRodDisposalCS : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fuelRods = new List<GameObject>();
    private List<Vector2> _rodOriginalPositions = new List<Vector2>();
    private List<Quaternion> _rodRotation = new List<Quaternion>();

    [SerializeField] private Animator _leftAnim;
    [SerializeField] private Animator _rightAnim;
    private void Awake()
    {
        foreach (var rod in _fuelRods)
        {
            RectTransform oldTranform = rod.GetComponent<RectTransform>();
            _rodOriginalPositions.Add(new Vector2(oldTranform.position.x, oldTranform.position.y));
            _rodRotation.Add(oldTranform.rotation);
        }
        Debug.Log("Total rods = " + _rodOriginalPositions);
    }

    private void OnEnable()
    {
        int index = 0;
        foreach (var rod in _fuelRods)
        {
            RectTransform rodTransform = rod.GetComponent<RectTransform>();
            rodTransform.position = _rodOriginalPositions[index];
            rodTransform.rotation = _rodRotation[index];
           
            rod.GetComponent<RodsFalling>()._falling = false;
            index++;
        }
    }

    private void OnDisable()
    {
        _leftAnim.SetTrigger("Close");
        _rightAnim.SetTrigger("Close");
    }

    public void StartFallingAnimations()
    {
        _leftAnim.SetTrigger("Open");
        _rightAnim.SetTrigger("Open");
        foreach (var rod in _fuelRods)
        {
            rod.GetComponent<RodsFalling>().Fall();
        }
        
    }
}
