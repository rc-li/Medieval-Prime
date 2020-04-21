using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {
	public GameObject[] popUps;
	public int popUpIndex;
	public float waitTime;
	public int gravityScale;
	public int runSpeed;
	public int jumpForce;
	public Rigidbody2D playerRigidBody;
	public RayController playerController;
	public TutorialPlayerControl tutorialPlayerControl;

	public bool waitIndex1;
	public bool wait;
	public bool waitScene5;
	public bool finalWait;

	private bool tutorialEnd;
	private bool jumping;
	private bool dashing;


	void Start() {
		GameObject player = GameObject.Find("Player (1)");
		tutorialPlayerControl = player.GetComponent<TutorialPlayerControl>();
		playerRigidBody = player.GetComponent<Rigidbody2D>();
	}

	void Awake() {
		wait = false;
		tutorialEnd = false;
	}

	void Update() {

		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			if (touch.position.x < Screen.width / 2) {
				dashing = true;
            } else {
				jumping = true;
            }

        } else {
			jumping = false;
			dashing = false;
        }



        if (tutorialEnd)
		{
			SceneManager.LoadScene("Main");
		} else 
		{
			LoadTutorial();
		}

	}

	private void LoadTutorial() {
		for (int i = 0; i < popUps.Length; i++) {
			if (i == popUpIndex) {
				popUps[i].SetActive(true);
			} else {
				popUps[i].SetActive(false);
			}
		}


        // the player walk towards the short obstacle
		// This code block, at the begining of the tutorial, lets the player run 
		// for some time before it display prompt1: "press the right side 
		// of the screen to perform a jump
        if (popUpIndex == 0) {

			if (waitTime <= 0) {
				Time.timeScale = 0f;
                tutorialPlayerControl.enabled = true;
                popUpIndex++;
			} else {
                tutorialPlayerControl.enabled = false; // disabling tutorial at the first several seconds 
                waitTime -= Time.deltaTime;
			}

		}


		// the player is currently standing in front of the short obstacle
		// This code block, when getting the jumping signal, will turn the tutorial page
		// and set time wait before the next user input
		if (popUpIndex == 1) {
			if (jumping) {
				Time.timeScale = 1f;
				waitTime = 0.05f;
				popUpIndex++;
				waitIndex1 = true;
			}
		}
		if (waitIndex1) {
			if (waitTime < 0) {
				tutorialPlayerControl.enabled = false;
				waitTime = 3f;
				waitIndex1 = false;
            } else {
				waitTime -= Time.deltaTime;
            }

        }



		// the player is running after jumping over the short obstacle
		// This code block lets the player run for the time set by the 
		// previous code block, and freeze time later
		if (popUpIndex == 2) {
			if (waitTime <= 0) {
				tutorialPlayerControl.enabled = true;
				Time.timeScale = 0f;
				popUpIndex++;
			} else {
				waitTime -= Time.deltaTime;
			}
		}

		////////////////////////// HIGHER OBSTACLE PART /////////////////////////////////

		// the player is standing at the higher obstacle
		// This code block will, upon getting jumping signal, turn the tutorial page
		// and restore the time
		if (popUpIndex == 3) {
			if (jumping) {
				Time.timeScale = 1f;
				waitTime = 0.25f;
				popUpIndex++;
			}
		}

		// the player did the first jump and is now in the air
		// This code block will wait for a while then freeze the time
		bool waitForUserInput = false;
		if (popUpIndex == 4) {
			if (waitTime <= 0) {
				Time.timeScale = 0f;
				waitForUserInput = true;
			} else {
				waitTime -= Time.deltaTime;
			}
		}


		// the player is frozen in the air, awaiting the double jump signal
		// This code block, upon getting jumping signal, will restore the time
		if (waitForUserInput) {
			if (jumping) {
				Time.timeScale = 1f;
                //tutorialPlayerControl.DoubleJump();
                playerRigidBody.velocity = Vector2.zero;
                playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                Debug.Log("force added from tutorial manager: " + jumpForce);
                waitTime = 1.25f;
				wait = true;
				waitForUserInput = false;
			}

		}

		// the player just finished double jumping and is doing the dash down soon
		// This code block will freeze the time for the dash down
		if (wait) {
			if (waitTime <= 0) {
				Time.timeScale = 0f;
				popUpIndex++;
				wait = false;
			} else {
				waitTime -= Time.deltaTime;
			}
		}

		// the player is awaiting "dash down" user input
		// This code block will, upon getting "dash down" signal, restore time and 
		// set wait time for the next block
		if (popUpIndex == 5) {
			if (dashing) {
				Time.timeScale = 1f;
				waitScene5 = true;
				waitTime = 2f;
			}
		}

		if (waitScene5) {
			if (waitTime <= 0) {
				popUpIndex++;
				waitScene5 = false;
				finalWait = true;
				waitTime = 3f;
			} else {
				waitTime -= Time.deltaTime;
			}
		}

		if (finalWait) {
			if (waitTime <= 0) {
				popUpIndex++;
				tutorialEnd = true;
			} else {
				waitTime -= Time.deltaTime;
			}
		}
	}

}