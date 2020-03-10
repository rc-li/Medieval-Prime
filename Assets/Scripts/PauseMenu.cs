using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update() {
    }

    public void Resume()
    {
        GameControl.instance.isPlaying = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        GameControl.instance.isPlaying = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameControl.instance.isPlaying = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lucy");
    }

    public void QuitGame()
    {
        GameControl.instance.isPlaying = false;
        Debug.Log("QUIT");
        Application.Quit();
    }
}
