using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    public GameObject img;
    public int coins = 500;
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
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
        if (coins < 100) {
            Debug.Log("NOT ENOUGH COINS");
        } else {
            coins -= 100;
            textBox.text = coins.ToString();
            ChangeImage();
        }
        
    }
}
