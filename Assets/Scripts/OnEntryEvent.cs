using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEntryEvent : MonoBehaviour
{
    private GameEvents _gameEvents;
    private void OnTriggerEnter(Collider other)
    {
        _gameEvents = GameObject.Find("GameEventManager").GetComponent<GameEvents>();
        if (gameObject.CompareTag("OpenDoorTrigger"))
        {
            _gameEvents.OpenDoor(true);
        }
        
        if (gameObject.CompareTag("CloseDoorTrigger"))
        {
            _gameEvents.CloseDoor();
        }
      
        
    }
}
