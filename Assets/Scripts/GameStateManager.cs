using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    public static int CurrentLevel = 1;
    public static float MusicVolume;
    public static float SFXVolume;
    
    public enum States {
        Running, GameOver, Pause, Cutscene, MiniGame
    }
    
    public static States currentState = States.MiniGame;
    public static States prePauseState;
    
    public static void ChangeState(States stateTo) {
        if(currentState == stateTo) 
            return;  
        currentState = stateTo;  
    }
     
    public static bool IsState(States stateTest) {        
        if(currentState == stateTest)
            return true;
        return false;
    }
}
