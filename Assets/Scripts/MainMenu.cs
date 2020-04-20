using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject town;

    public void PlayGame ()
    {
        PlayerPrefs.DeleteKey("Tutorial");
        Debug.Log("TUTORIAL:" + PlayerPrefs.HasKey("Tutorial").ToString());
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

    void Start(){
        if (PlayerPrefs.GetInt("Town", 0) == 0){
            PlayerPrefs.SetInt("Town", 1);
        }
        showTown();
    }

    void showTown(){
        Debug.Log("town");
        Sprite house1 = Resources.Load<Sprite>("Buildings/house_25");
        Sprite house2 = Resources.Load<Sprite>("Buildings/house_26");
        Sprite house3 = Resources.Load<Sprite>("Buildings/house_27");
        Sprite house4 = Resources.Load<Sprite>("Buildings/house_29");
        Sprite house5 = Resources.Load<Sprite>("Buildings/house_30");
        Sprite house6 = Resources.Load<Sprite>("Buildings/house_28");

        if(PlayerPrefs.GetInt("Town", 0) == 1){
            Debug.Log("town 1");
            town.GetComponent<Image>().sprite = house1;
        } if(PlayerPrefs.GetInt("Town", 0) == 2){
            Debug.Log("town 2");
            town.GetComponent<Image>().sprite = house2;
        } if(PlayerPrefs.GetInt("Town", 0) == 3){
            Debug.Log("town 3");
            town.GetComponent<Image>().sprite = house3;
        } if(PlayerPrefs.GetInt("Town", 0) == 4){
            Debug.Log("town 4");
            town.GetComponent<Image>().sprite = house4;
        } if(PlayerPrefs.GetInt("Town", 0) == 5){
            Debug.Log("town 5");
            town.GetComponent<Image>().sprite = house5;
        } if(PlayerPrefs.GetInt("Town", 0) == 6){
            Debug.Log("town 6");
            town.GetComponent<Image>().sprite = house6;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
