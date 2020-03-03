using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointMenu : MonoBehaviour
{
    public float timeStart = 10;
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Mathf.Round(timeStart) == 0) {
            Time.timeScale = 0f;

            int choice = Random.Range(1,3);
            Debug.Log(choice.ToString());
            Debug.Log("CHANGE");
        } else {
            timeStart -= Time.deltaTime;
            textBox.text = Mathf.Round(timeStart).ToString();
        }
        
    }
}
