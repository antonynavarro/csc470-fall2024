using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject startButton;  
    public GameBattleManager gameManager; 

   
    public void OnStartButtonClick()
    {
        if (!gameManager.gameStarted)
        {
            gameManager.StartGame();
            Debug.Log("Game Started from Button!");
            startButton.SetActive(false);
        }

       
        
    }

    
    
}
