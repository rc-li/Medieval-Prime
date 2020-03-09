using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text coinText;
	public Text TotalCoinText;
	public Text heartText;
	public Text CoinMultiplierText;	//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;				//A reference to the object that displays the text which appears when the player dies.

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
		totalCoin = PlayerPrefs.GetInt("TotalCoin", totalCoin);
		lifeCounter = PlayerPrefs.GetInt("TotalLife", lifeCounter);
	}

	void Update()
	{	
		//If the game is over and the player has pressed some input...
		// && Input.GetMouseButtonDown(0)
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		totalCoin = PlayerPrefs.GetInt("TotalCoin", totalCoin);
		TotalCoinText.text = "TotalCoins: " + totalCoin.ToString();
		heartText.text = lifeCounter.ToString();

		if (newTown == true) 
		{
			if (Mathf.Round(timeStart) == 0) {
				int choice = Random.Range(1,3);
				if (choice == 1){
					quit();

				} else {

				}
			} else {
				Debug.Log(Time.unscaledDeltaTime.ToString());
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
		newTown = false;
		//Activate the game over text.
		gameOvertext.SetActive (true);
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
		newTown = false;
		isPlaying = true;
		menuPanel.SetActive(false);
		coinMultiplier *= 2;
		Time.timeScale = 1f;
		CoinMultiplierText.text = "Coin Multiplier: " + coinMultiplier.ToString();
	}

    public void quit()
    {	
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

	public void updateLifeCounter(){
		lifeCounter -= 1;
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
}
