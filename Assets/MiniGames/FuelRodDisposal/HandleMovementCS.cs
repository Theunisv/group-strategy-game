using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HandleMovementCS : EventTrigger {

    private bool dragging;
    private float limitYTop = 58f;
    private float limitYBottom = -63.1f;
    private bool taskSucess = false;
    private Vector2 originalPos;
    private RectTransform currentPos;
    private RectTransform oldPos;
    private bool successCalled = false;
    

    private void Start()
    {
        oldPos = GetComponent<RectTransform>();
        originalPos = new Vector2(oldPos.position.x, oldPos.position.y);
        limitYTop = transform.position.y;
    }

    private void OnEnable()
    {
        successCalled = false;
        currentPos = GetComponent<RectTransform>();
        limitYBottom = GameObject.Find("BottomLimitY").transform.position.y;
        limitYTop = GameObject.Find("TopLimitY").transform.position.y;
        taskSucess = false;
        currentPos.position = new Vector2(currentPos.position.x, limitYTop);
        //this.GetComponent<RectTransform>() =  currentPos;
    }

    private void OnDisable()
    {
        taskSucess = false;
        currentPos.position = new Vector2(currentPos.position.x, limitYTop);
    }

    public void Update() {
        if (dragging && Input.mousePosition.y < limitYTop && !taskSucess) {
            transform.position = new Vector2(transform.position.x, Input.mousePosition.y);
        }

        if (transform.position.y <= limitYBottom && !successCalled)
        {
            GameObject.Find("AudioSources").GetComponent<AudioManager>().PlayLevelPullSound();
            successCalled = true;
            taskSucess = true;
            StartCoroutine(StartEmptyingAnimation());
        }
    }

    public override void OnPointerDown(PointerEventData eventData) {
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData) {
        dragging = false;
    }

    private IEnumerator StartEmptyingAnimation()
    {
        GameObject.Find("FuelRodDisposalMiniGame").GetComponent<FuelRodDisposalCS>().StartFallingAnimations();
        yield return new WaitForSeconds(4f);
        GameObject.Find("MiniGames").GetComponent<MiniGameManager>().TaskWasSuccessful();
        
    }
}
