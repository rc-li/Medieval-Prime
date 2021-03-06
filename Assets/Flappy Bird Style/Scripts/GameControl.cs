﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System;


public class GameControl : MonoBehaviour 
{
	public static int townNumber = 1;

	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text coinText;
	public Text TotalCoinText;
	public Text heartText;
	public Text CoinMultiplierText;	//A reference to the UI text component that displays the player's score.
	public GameObject gameOverPanel;				//A reference to the object that displays the text which appears when the player dies.

	private int coin = 0;
	private int coinMultiplier = 2;
	//The player's score.
	public bool gameOver = false;				//Is the game over?
	public float scrollSpeed = -1.5f;
	public GameObject menuPanel;
	private static int totalCoin = 0;
	public bool isPlaying = true;

	// For counter text
	public float timeStart = 10;
    public Text counterText;
	public bool newTown = false;

	// For life counter
	private int lifeCounter = 1;

	// Sound Effects
	public AudioSource myFx;
    public AudioClip jumpFx;
    public AudioClip coinFx;
    public AudioClip dashFx;
    public AudioClip collisionFx;

	public int townLevel;

	public Text gameOverCoinText;


	void Start()
	{
		lifeCounter = PlayerPrefs.GetInt("TotalLife", lifeCounter);
	}

	void Awake()
	{
		newTown = false;
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);

		// Make sure the towncheck panel is off at the beginning.
		menuPanel.SetActive(false);

		// COMMAND TO DELETE THE TOTALCOIN CACHE, ONLY FOR TEST PURPOSES
		// PlayerPrefs.SetInt("TotalCoin", 0);

		// Save total coin to disk
		// PlayerPrefs.SetInt("TotalCoin", 0);
		// PlayerPrefs.SetInt("TotalLife", 5);

		totalCoin = PlayerPrefs.GetInt("TotalCoin", totalCoin);
	}

	void Update()
	{	
		//If the game is over and the player has pressed some input...
		// && Input.GetMouseButtonDown(0)
		// if (gameOver && Input.GetMouseButtonDown(0)) 
		// {
		// 	//To Main Menu.
		// 	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		// }
		totalCoin = PlayerPrefs.GetInt("TotalCoin", totalCoin);
		TotalCoinText.text = "TotalCoins: " + totalCoin.ToString();
		heartText.text = lifeCounter.ToString();

		if (newTown == true) 
		{
			if (Mathf.Round(timeStart) == 0) {
				int choice = UnityEngine.Random.Range(1,3);
				if (choice == 1){
					quit();

				} else {

				}
			} else {
				//Debug.Log(Time.unscaledDeltaTime.ToString());
				timeStart -= Time.unscaledDeltaTime;
				counterText.text = Mathf.Round(timeStart).ToString();
			}
		}
	}

	public void CollectedGold()
	{
		
		//The bird can't score if the game is over.
		if (gameOver)	
			return;
		//If the game is not over, increase the score...
		coin += coinMultiplier;
		//...and adjust the coin text.
		coinText.text = "Coins: " + coin.ToString();
	}



	public void PlayerDied()
	{	
		townLevel = PlayerPrefs.GetInt("Town", 0);

		Debug.Log("coins_before: " + coin.ToString());

		coin = (int)Math.Round(coin * townLevel * 0.1);

		Debug.Log("coins_after: " + coin.ToString());

		totalCoin += coin;
        TotalCoinText.text = "TotalCoins: " + totalCoin.ToString();
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();
		
		newTown = false;
		//Activate the game over text.
		gameOverPanel.SetActive (true);
		gameOverCoinText.text = "You gained " + coin.ToString() + " coins";
		//Set the game to be over.
		gameOver = true;
	}

	public void hitNewTown()
	{

        popUpWindow();
		newTown = true;
        //quit();
        //continuePlay();
	}
	public void popUpWindow()
	{
		Time.timeScale = 0f;
		menuPanel.SetActive(true);
		counterText.text = timeStart.ToString();
		isPlaying = false;
	}

	public void continuePlay()
	{
		ReportTownSurvive(townNumber);
		townNumber += 1;
		timeStart = 10;
		newTown = false;
		isPlaying = true;
		menuPanel.SetActive(false);
		coinMultiplier *= 2;
		Time.timeScale = 1f;
		CoinMultiplierText.text = "Coin Multiplier: " + coinMultiplier.ToString();
	}

	public void playAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		gameOverPanel.SetActive(false);
	}

    public void quit()
    {
		ReportTownQuit(townNumber);
		newTown = false;
        totalCoin += coin;
        TotalCoinText.text = "TotalCoins: " + totalCoin.ToString();
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();
        coinMultiplier = 2;
		// SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("Lucy");
        Time.timeScale = 1f;
    }

	public void toMenu()
	{
		SceneManager.LoadScene("Lucy");
	}

	public void updateLifeCounter(){
		lifeCounter -= 1;
		ReportTownDead(townNumber);
		Debug.Log("LIFE: " + lifeCounter.ToString());
	}

	public bool checkDead(){
		if (lifeCounter == 0){
			return true;
		} else {
			return false;
		}
	}

	public void JumpSound(){
        myFx.PlayOneShot(jumpFx);
    }

	public void CoinSound(){
        myFx.PlayOneShot(coinFx);
    }

	public void DashSound(){
        myFx.PlayOneShot(dashFx);
    }

	public void CollsionSound(){
        myFx.PlayOneShot(collisionFx);
    }

    //decide which town people decide to quit
	public void ReportTownQuit(int townNumber)
	{
		AnalyticsEvent.Custom("town_quit", new Dictionary<string, object>
		{
			{ "town_quit", townNumber }
		});
	}


	//decide which town people decide to quit
	public void ReportTownDead(int townNumber)
	{
		AnalyticsEvent.Custom("dead_num", new Dictionary<string, object>
		{
			{ "town_dead" ,townNumber}
		});
	}

	//decide town survive number
	public void ReportTownSurvive(int townNumber)
	{
		AnalyticsEvent.Custom("town_survive", new Dictionary<string, object>
		{
			{ "town_survive" ,townNumber}
		});
	}



}
