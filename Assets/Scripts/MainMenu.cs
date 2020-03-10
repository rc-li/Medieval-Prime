using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        Debug.Log("TUTORIAL:" + PlayerPrefs.GetInt("Tutorial", 0).ToString());
        if(PlayerPrefs.GetInt("Tutorial", 0) == 0){
            PlayerPrefs.SetInt("Tutorial", 1);
            Debug.Log("TUTORIAL:" + "load tutorial");
            SceneManager.LoadScene("Tutorial");
        } else
        {
            Debug.Log("TUTORIAL:" + "skip tutorial");
            SceneManager.LoadScene("Main");
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
