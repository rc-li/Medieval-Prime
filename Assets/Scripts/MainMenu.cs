using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
    }

    public void PlayGame ()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            // SceneManager.LoadScene("Tutorial");
            PlayerPrefs.SetInt("Tutorial", 1);
        } else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
