using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    public GameObject img;

    public int coins;
    public Text coinText;

    public int life;
    public Text lifeText;

    // Start is called before the first frame update
    void Start()
    {   
        coins = PlayerPrefs.GetInt("TotalCoin", 0);
        life = PlayerPrefs.GetInt("TotalLife", 1);

        coinText.text = coins.ToString();
        lifeText.text = life.ToString();
    }

    void ChangeImage()
    {
        RectTransform rt = img.GetComponent<Image>().rectTransform;
        float w = rt.rect.width + 50;
        float h = rt.rect.height + 50;
        img.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(w, h);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Upgrade()
    {
        if (coins < 500) {
            Debug.Log("NOT ENOUGH COINS");
        } else {
            coins -= 500;
            PlayerPrefs.SetInt("TotalCoin", coins);
            PlayerPrefs.Save();
            coinText.text = coins.ToString();
        }
        
    }

    public void lifeUpgrade(){
        Upgrade();
        life += 1;
        PlayerPrefs.SetInt("TotalLife", life);
        PlayerPrefs.Save();
        lifeText.text = life.ToString();
    }
}
