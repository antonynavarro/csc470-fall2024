using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartScene()
    {
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("GameScene");
    }
}
