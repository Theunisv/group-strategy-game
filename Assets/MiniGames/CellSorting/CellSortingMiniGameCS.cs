using System;
using System.Collections;
using System.Collections.Generic;
using MiniGames.CellSorting;
using UnityEngine;

public class CellSortingMiniGameCS : MonoBehaviour
{
    //Cells
    [SerializeField] private GameObject _cellGreen;
    [SerializeField] private GameObject _cellBlue;
    [SerializeField] private GameObject _cellRed;
    [SerializeField] private GameObject _cellYellow;
    
    //Cell win pos
    [SerializeField] private GameObject _winPosGreen;
    [SerializeField] private GameObject _winPosBlue;
    [SerializeField] private GameObject _winPosRed;
    [SerializeField] private GameObject _winPosYellow;

    private int cellsInPosition = 0;

    private Vector3 originalPosGreen;
    private Vector3 originalPosBlue;
    private Vector3 originalPosRed;
    private Vector3 originalPosYellow;

    private bool _succesSent = false;
    private void Awake()
    {
        originalPosGreen = _cellGreen.transform.position;
        originalPosBlue = _cellBlue.transform.position;
        originalPosRed = _cellRed.transform.position;
        originalPosYellow = _cellYellow.transform.position;
    }
    private void Update()
    {
        if (!_succesSent)
        {
            if (cellsInPosition < 4)
            {
                CheckCellPositions();
            }

            else
            {
                cellsInPosition = 0;
                _succesSent = true;
                GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasSuccessful();
                
            }
        }
    }
    private void CheckCellPositions()
    {
        if (_cellGreen.transform.position.x >= _winPosGreen.transform.position.x - 5f && _cellGreen.transform.position.x <= _winPosGreen.transform.position.x + 5f
        && _cellGreen.transform.position.y >= _winPosGreen.transform.position.y - 5f && _cellGreen.transform.position.y <= _winPosGreen.transform.position.y + 5f && !_cellGreen.GetComponent<CellDragScript>().inWinPosition)
        {
            _cellGreen.transform.position = _winPosGreen.transform.position;
            _cellGreen.GetComponent<CellDragScript>().inWinPosition = true;
            cellsInPosition++;
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelPullSound();
        }
        
        if (_cellBlue.transform.position.x >= _winPosBlue.transform.position.x - 5f && _cellBlue.transform.position.x <= _winPosBlue.transform.position.x + 5f
                                                                                     && _cellBlue.transform.position.y >= _winPosBlue.transform.position.y - 5f && _cellBlue.transform.position.y <= _winPosBlue.transform.position.y + 5f && !_cellBlue.GetComponent<CellDragScript>().inWinPosition)
        {
            _cellBlue.transform.position = _winPosBlue.transform.position;
            _cellBlue.GetComponent<CellDragScript>().inWinPosition = true;
            cellsInPosition++;
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelPullSound();
        }
        
        if (_cellYellow.transform.position.x >= _winPosYellow.transform.position.x - 5f && _cellYellow.transform.position.x <= _winPosYellow.transform.position.x + 5f
                                                                                         && _cellYellow.transform.position.y >= _winPosYellow.transform.position.y - 5f && _cellYellow.transform.position.y <= _winPosYellow.transform.position.y + 5f && !_cellYellow.GetComponent<CellDragScript>().inWinPosition)
        {
            _cellYellow.transform.position = _winPosYellow.transform.position;
            _cellYellow.GetComponent<CellDragScript>().inWinPosition = true;
            cellsInPosition++;
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelPullSound();
        }
        
        if (_cellRed.transform.position.x >= _winPosRed.transform.position.x - 5f && _cellRed.transform.position.x <= _winPosRed.transform.position.x + 5f
                                                                                   && _cellRed.transform.position.y >= _winPosRed.transform.position.y - 5f && _cellRed.transform.position.y <= _winPosRed.transform.position.y + 5f && !_cellRed.GetComponent<CellDragScript>().inWinPosition)
        {
            _cellRed.transform.position = _winPosRed.transform.position;
            _cellRed.GetComponent<CellDragScript>().inWinPosition = true;
            cellsInPosition++;
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelPullSound();
        }
    }
    private void OnEnable()
    {
        _cellGreen.transform.position = originalPosGreen;
        _cellBlue.transform.position = originalPosBlue;
        _cellRed.transform.position = originalPosRed;
        _cellYellow.transform.position = originalPosYellow;
        _succesSent = false;
    }
    private void OnDisable()
    {
        cellsInPosition = 0;
        _cellGreen.GetComponent<CellDragScript>().inWinPosition = false;
        _cellBlue.GetComponent<CellDragScript>().inWinPosition = false;
        _cellRed.GetComponent<CellDragScript>().inWinPosition = false;
        _cellYellow.GetComponent<CellDragScript>().inWinPosition = false;
        
        _cellGreen.transform.position = originalPosGreen;
        _cellBlue.transform.position = originalPosBlue;
        _cellRed.transform.position = originalPosRed;
        _cellYellow.transform.position = originalPosYellow;
    }
}