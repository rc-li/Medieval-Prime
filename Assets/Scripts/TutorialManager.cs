using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	public GameObject[] popUps;
	public int popUpIndex;
	public float waitTime;
	public int gravityScale;
	public int runSpeed;
	public int jumpForce;
	public Rigidbody2D playerRigidBody;
	public RayController playerController;

	void Awake() {
		//playerRigidBody = GameObject.Find("player").GetComponent<Rigidbody2D>();
		//playerRigidBody.velocity = new Vector2(runSpeed, playerRigidBody.velocity.y);
		//playerController = GameObject.Find("player").GetComponent<RayController>();

	}

	void Update() {

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
				//playerRigidBody.velocity = new Vector2(0, 0);
				//playerRigidBody.gravityScale = 0;
				Time.timeScale = 0f;
				popUpIndex++;
			} else {
				//playerRigidBody.velocity = new Vector2(runSpeed, playerRigidBody.velocity.y);
				waitTime -= Time.deltaTime;
			}

		}


		// the player click jump before the short obstacle
		if (popUpIndex == 1) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
                //playerRigidBody.velocity = new Vector2(runSpeed, jumpForce);
                //playerRigidBody.gravityScale = gravityScale;
                waitTime = 3f;
                Time.timeScale = 1f;
				popUpIndex++;
			}

		}

		// after the short obstacle
		if (popUpIndex == 2) {
			if (waitTime <= 0) {
				//playerRigidBody.velocity = new Vector2(0, 0);
				Time.timeScale = 0f;
				popUpIndex++;
			} else {
				//playerRigidBody.velocity = new Vector2(runSpeed, playerRigidBody.velocity.y);
				waitTime -= Time.deltaTime;
			}
		}

		// before the higher obstacle
		if (popUpIndex == 3) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				//playerRigidBody.velocity = new Vector2(runSpeed, jumpForce);
				//playerRigidBody.gravityScale = gravityScale;
				Time.timeScale = 1f;
				waitTime = 0.25f;
				popUpIndex++;
			}
		}

		// before the higher obstacle, in the air
		bool waitForUserInput = false;
		if (popUpIndex == 4) {
			if (waitTime <= 0) {
				//playerRigidBody.velocity = new Vector2(0, 0);
				//playerRigidBody.gravityScale = 0;
				Time.timeScale = 0f;
				waitForUserInput = true;
			} else {
				//playerRigidBody.velocity = new Vector2(runSpeed, playerRigidBody.velocity.y);
				waitTime -= Time.deltaTime;
			}
		}

		if (waitForUserInput) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				//playerRigidBody.velocity = new Vector2(runSpeed, jumpForce);
				//playerRigidBody.gravityScale = gravityScale;
				Time.timeScale = 1f;
				waitTime = 3f;
				popUpIndex++;
				waitForUserInput = false;
			}

		}

		if (popUpIndex == 5) {
			if (waitTime <= 0) {
				popUpIndex++;
				waitTime = 2f;
            } else {
				waitTime -= Time.deltaTime;
            }
		}

		if (popUpIndex == 6) {
			if (waitTime <= 0) {
				popUpIndex++;
				Time.timeScale = 0f;
			} else {
				waitTime -= Time.deltaTime;
			}
		}


		if (popUpIndex == 7) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				Time.timeScale = 1f;
				popUpIndex++;
			}
        }

		if (popUpIndex == 8) {

        }


	}

}