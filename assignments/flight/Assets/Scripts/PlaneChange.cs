using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneChange : MonoBehaviour
{
    public GameObject jetPlane;
    public GameObject normalPlane;

    public void ChangePlane()
    {
        // Toggle between planes
        if (jetPlane.activeSelf)
        {
            PlayerPrefs.SetInt("SelectedPlane", 2); // Set to normalPlane
        }
        else
        {
            PlayerPrefs.SetInt("SelectedPlane", 1); // Set to jetPlane
        }

        PlayerPrefs.Save();

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
