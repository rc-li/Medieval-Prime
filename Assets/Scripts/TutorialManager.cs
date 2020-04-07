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
	public bool wait;
	public bool waitScene5;
	public bool finalWait;

	private bool tutorialEnd;
	private bool jumping;
	private bool dashing;

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
		if (popUpIndex == 0) {

			if (waitTime <= 0) {
				Time.timeScale = 0f;
				popUpIndex++;
			} else {
				waitTime -= Time.deltaTime;
			}

		}


		// the player click jump before the short obstacle
		if (popUpIndex == 1) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				waitTime = 3f;
				Time.timeScale = 1f;
				popUpIndex++;
			}

		}

		// after the short obstacle
		if (popUpIndex == 2) {
			if (waitTime <= 0) {
				Time.timeScale = 0f;
				popUpIndex++;
			} else {
				waitTime -= Time.deltaTime;
			}
		}

		// before the higher obstacle
		if (popUpIndex == 3) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				Time.timeScale = 1f;
				waitTime = 0.25f;
				popUpIndex++;
			}
		}

		// before the higher obstacle, in the air
		bool waitForUserInput = false;
		if (popUpIndex == 4) {
			if (waitTime <= 0) {
				Time.timeScale = 0f;
				waitForUserInput = true;
			} else {
				waitTime -= Time.deltaTime;
			}
		}

		if (waitForUserInput) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				Time.timeScale = 1f;
				waitTime = 1.25f;

				wait = true;
				waitForUserInput = false;
			}

		}

		if (wait) {
			if (waitTime <= 0) {
				Time.timeScale = 0f;
				popUpIndex++;
				wait = false;
			} else {
				waitTime -= Time.deltaTime;
			}
		}

		// right before the dash down
		if (popUpIndex == 5) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
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