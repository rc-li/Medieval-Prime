using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TownMenu : MonoBehaviour
{
    public GameObject town;

    public int coins;
    public Text coinText;

    public int townLevel;
    public Text levelText;

    public int percent;
    public Text percentText;

    public int cost;
    public Text costText;

    // Start is called before the first frame update
    void Start()
    {   
        coins = PlayerPrefs.GetInt("TotalCoin", 0);
        townLevel = PlayerPrefs.GetInt("Town", 0);
        percent = 10 * townLevel;
        cost = 10 * (int)Math.Pow(10, townLevel);

        coinText.text = coins.ToString();
        levelText.text = townLevel.ToString();
        percentText.text = percent.ToString();
        costText.text = cost.ToString();

        showTown();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Upgrade()
    {
        if (coins < cost) {
            Debug.Log("NOT ENOUGH COINS");
        } else {
            coins -= cost;
            townLevel += 1;
            percent = 10 * townLevel;
            cost = 10 * (int)Math.Pow(10, townLevel);
            PlayerPrefs.SetInt("TotalCoin", coins);
            PlayerPrefs.SetInt("Town", townLevel);
            PlayerPrefs.Save();
            coinText.text = coins.ToString();
            levelText.text = townLevel.ToString();
            percentText.text = percent.ToString();
            costText.text = cost.ToString();

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
