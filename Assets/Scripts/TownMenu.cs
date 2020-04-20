using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMenu : MonoBehaviour
{
    public GameObject town;

    public int coins;
    public Text coinText;

    public int townLevel;
    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {   
        coins = PlayerPrefs.GetInt("TotalCoin", 0);
        townLevel = PlayerPrefs.GetInt("Town", 0);

        coinText.text = coins.ToString();
        levelText.text = townLevel.ToString();

        showTown();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Upgrade()
    {
        if (coins < townLevel * 100) {
            Debug.Log("NOT ENOUGH COINS");
        } else {
            coins -= townLevel * 100;
            townLevel += 1;
            PlayerPrefs.SetInt("TotalCoin", coins);
            PlayerPrefs.SetInt("Town", townLevel);
            PlayerPrefs.Save();
            coinText.text = coins.ToString();
            levelText.text = townLevel.ToString();

            showTown();
        }
    }

    void showTown(){
        Sprite house1 = Resources.Load<Sprite>("Buildings/house_25");
        Sprite house2 = Resources.Load<Sprite>("Buildings/house_26");
        Sprite house3 = Resources.Load<Sprite>("Buildings/house_27");
        Sprite house4 = Resources.Load<Sprite>("Buildings/house_29");
        Sprite house5 = Resources.Load<Sprite>("Buildings/house_30");
        Sprite house6 = Resources.Load<Sprite>("Buildings/house_28");

        if(PlayerPrefs.GetInt("Town", 0) == 1){
            town.GetComponent<Image>().sprite = house1;
        } if(PlayerPrefs.GetInt("Town", 0) == 2){
            town.GetComponent<Image>().sprite = house2;
        } if(PlayerPrefs.GetInt("Town", 0) == 3){
            town.GetComponent<Image>().sprite = house3;
        } if(PlayerPrefs.GetInt("Town", 0) == 4){
            town.GetComponent<Image>().sprite = house4;
        } if(PlayerPrefs.GetInt("Town", 0) == 5){
            town.GetComponent<Image>().sprite = house5;
        } if(PlayerPrefs.GetInt("Town", 0) == 6){
            town.GetComponent<Image>().sprite = house6;
        }
    }
}
