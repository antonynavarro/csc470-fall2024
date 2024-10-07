using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControl : MonoBehaviour
{
    
    public GameBattleManager gameBattleManager; 
    public float speedMultiplier = 2f; 
   
    public void SpeedUp()
    {
        gameBattleManager.tickRate /= speedMultiplier;
    }

}

