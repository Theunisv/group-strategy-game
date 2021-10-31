using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StationLocationRandomisation : MonoBehaviour
{
    [SerializeField] private GameObject _station1;
    [SerializeField] private GameObject _station2;
    [SerializeField] private GameObject _station3;
    [SerializeField] private GameObject _station4;

    [SerializeField] private float _station1Min, _station1Max;
    [SerializeField] private float _station2Min, _station2Max;
    [SerializeField] private float _station3Min, _station3Max;
    [SerializeField] private float _station4Min, _station4Max;

    private void Start()
    {
        _station1.transform.localPosition = new Vector3(Random.Range(_station1Min, _station1Max), _station1.transform.localPosition.y, _station1.transform.localPosition.z);
        _station2.transform.localPosition = new Vector3(Random.Range(_station2Min, _station2Max), _station2.transform.localPosition.y, _station2.transform.localPosition.z);
        _station3.transform.localPosition = new Vector3(Random.Range(_station3Min, _station3Max), _station3.transform.localPosition.y, _station3.transform.localPosition.z);
        _station4.transform.localPosition = new Vector3(Random.Range(_station4Min, _station4Max), _station4.transform.localPosition.y, _station4.transform.localPosition.z);
    }
}
