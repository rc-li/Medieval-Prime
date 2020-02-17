using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChenPlayerControl : MonoBehaviour
{
    public float speed = 3.0f;
    Rigidbody2D rigidbody2d;
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void popUpWindow() {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);

    }

    public void continuePlay(){
        menuPanel.SetActive(false) ;
        Time.timeScale = 1f;
        //Vector2 position = rigidbody2d.position;

    }

    public void quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart the scene " + SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Debug.Log("quit the game");
    }
}
