using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text coinText;
	public Text TotalCoinText;
	public Text CoinMultiplierText;	//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;				//A reference to the object that displays the text which appears when the player dies.

	private int coin = 0;
	private int coinMultiplier = 2;
	//The player's score.
	public bool gameOver = false;				//Is the game over?
	public float scrollSpeed = -1.5f;
	public GameObject menuPanel;
	private static int totalCoin = 0;

	void Awake()
	{
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
		totalCoin = PlayerPrefs.GetInt("TotalCoin", totalCoin);
	}

	void Update()
	{
		//If the game is over and the player has pressed some input...
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		totalCoin = PlayerPrefs.GetInt("TotalCoin", totalCoin);
		TotalCoinText.text = "TotalCoins: " + totalCoin.ToString();
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
		//Activate the game over text.
		gameOvertext.SetActive (true);
		//Set the game to be over.
		gameOver = true;
	}

	public void hitNewTown()
	{
		//popUpWindow();
		//quit();
		continuePlay();
	}
	public void popUpWindow()
	{
		Time.timeScale = 0f;
		menuPanel.SetActive(true);
	}

	public void continuePlay()
	{
		menuPanel.SetActive(false);
		coinMultiplier *= 2;
		Time.timeScale = 1f;
		CoinMultiplierText.text = "Coin Multiplier: " + coinMultiplier.ToString();
	}
	public void quit()
	{
		totalCoin += coin;
		TotalCoinText.text = "TotalCoins: " + totalCoin.ToString();
		PlayerPrefs.SetInt("TotalCoin", totalCoin);
		PlayerPrefs.Save();
		coinMultiplier = 2;
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}
}
