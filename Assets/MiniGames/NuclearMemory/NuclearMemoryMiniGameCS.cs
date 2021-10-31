using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NuclearMemoryMiniGameCS : MonoBehaviour
{
    [SerializeField] private Sprite _clickedSprite;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _incorrectSprite;
    
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    private List<int> indexOrder = new List<int>();
    private int turnCount = 1;
    private int playerCurrentIndex = 0;
    private List<GameObject> correctOrder = new List<GameObject>();
    private List<GameObject> playerOrder = new List<GameObject>();


    private bool buttonClicksDisabled = true;
    private bool playerTurn = false;
    private bool successSent = false;
    private bool playerWasCorrect = true;

    private void OnEnable()
    {
        correctOrder.Clear();
        playerTurn = false;
        turnCount = 1;
        playerCurrentIndex = 0;
        successSent = false;
        playerWasCorrect = true;
        buttonClicksDisabled = true;
    }

    private void Update()
    {
        if (!playerTurn && !successSent)
        {
            playerTurn = true;
            
            if (playerWasCorrect)
            {
                correctOrder.Add(buttons[Random.Range(0, buttons.Count)]);
            }

            StartCoroutine(FlashOrder());
        }
    }

    public void FlashImage(GameObject buttonClicked)
    {
        if (!buttonClicksDisabled)
        {
            
            if (buttonClicked == correctOrder[playerCurrentIndex])
            {
                playerCurrentIndex++;
                StartCoroutine(Flash(buttonClicked));
            }
            else
            {
                StartCoroutine(FlashWrong(buttonClicked));
                playerTurn = false;
                playerCurrentIndex = 0;
                buttonClicksDisabled = true;
                playerWasCorrect = false;
            }

            if (playerCurrentIndex == turnCount)
            {
                playerTurn = false;
                playerCurrentIndex = 0;
                turnCount++;
                buttonClicksDisabled = true;
                playerWasCorrect = true;
            }

            if (turnCount == 6)
            {
                successSent = true;
                GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasSuccessful();
            }
        }
        
    }

    private IEnumerator Flash(GameObject buttonClicked)
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlaySuccessSound();
        buttonClicked.GetComponent<Image>().sprite = _clickedSprite;
        yield return new WaitForSeconds(0.2f);
        buttonClicked.GetComponent<Image>().sprite = _defaultSprite;
    }

    private IEnumerator FlashOrder()
    {
        yield return new WaitForSeconds(0.7f);
        foreach (var button in correctOrder)
        {
            StartCoroutine(Flash(button));
            yield return new WaitForSeconds(0.3f);
        }

        buttonClicksDisabled = false;
    }
    
    private IEnumerator FlashWrong(GameObject buttonClicked)
    {
        GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayFailSound();
        buttonClicked.GetComponent<Image>().sprite = _incorrectSprite;
        yield return new WaitForSeconds(0.2f);
        buttonClicked.GetComponent<Image>().sprite = _defaultSprite;
    }
}
