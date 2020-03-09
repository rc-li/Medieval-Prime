using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class RayTutorialManager : MonoBehaviour {
	public GameObject[] popUps;
	public int popUpIndex;
	public float waitTime;
	public bool tutorial_1;
	public bool tutorial_2;
	public Rigidbody2D playerRigidBody;
	public int gravityScale;
	public int runSpeed;
	public int jumpForce;
	public RayController playerController;

	void Awake () {
		playerRigidBody = GameObject.Find ("player").GetComponent<Rigidbody2D> ();
		playerRigidBody.velocity = new Vector2(runSpeed,playerRigidBody.velocity.y);
		playerController = GameObject.Find("player").GetComponent<RayController>();
		
	}

	void Update () {

		for (int i = 0; i < popUps.Length; i++) {
			if (i == popUpIndex) {
				popUps[i].SetActive (true);
			} else {
				popUps[i].SetActive (false);
			}
		}



        // the player walk towards the short obstacle
        if (popUpIndex == 0) {

			if (waitTime <= 0) {
				playerRigidBody.velocity = new Vector2 (0, 0);
				playerRigidBody.gravityScale = 0;
				popUpIndex++;
			} else {
				playerRigidBody.velocity = new Vector2(runSpeed,playerRigidBody.velocity.y);
				waitTime -= Time.deltaTime;
			}

		}
		

		// the player click jump before the short obstacle
		if (popUpIndex == 1) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				playerRigidBody.velocity = new Vector2(runSpeed, jumpForce);
				playerRigidBody.gravityScale = gravityScale;
				waitTime = 3f;
				popUpIndex++;
            }

		}

		// after the short obstacle
		if (popUpIndex == 2) {
			if (waitTime <= 0) {
				playerRigidBody.velocity = new Vector2(0, 0);
				popUpIndex++;
            } else {
				playerRigidBody.velocity = new Vector2(runSpeed, playerRigidBody.velocity.y);
				waitTime -= Time.deltaTime;
            }
        }

		// before the higher obstacle
		if (popUpIndex == 3) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				playerRigidBody.velocity = new Vector2(runSpeed, jumpForce);
				playerRigidBody.gravityScale = gravityScale;
				waitTime = 0.25f;
				popUpIndex++;
			}
		}

		// before the higher obstacle, in the air
		bool waitForUserInput = false;
		if (popUpIndex == 4) {
			if (waitTime <= 0) {
				playerRigidBody.velocity = new Vector2(0, 0);
				playerRigidBody.gravityScale = 0;
				waitForUserInput = true;
			} else {
				//playerRigidBody.velocity = new Vector2(runSpeed, playerRigidBody.velocity.y);
				waitTime -= Time.deltaTime;
			}
		}

		if (waitForUserInput) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
				playerRigidBody.velocity = new Vector2(runSpeed, jumpForce);
				playerRigidBody.gravityScale = gravityScale;
				waitTime = 0.25f;
				popUpIndex++;
				waitForUserInput = false;
			}

        }

		if (popUpIndex == 5) {

		}

		// the scene should be asking the user to perform a double jump now

		//if (popUpIndex == 1) {
		//	if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse0)) {
		//		if (waitTime <= 0) {
		//			playerRigidBody.gravityScale = 0;
		//			playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, 0);
		//			waitTime = 0.25f; //set the wait time to be used in next scene
		//			tutorial_1 = false;
		//			popUpIndex++; // turn the page
		//		} else {
		//			waitTime -= Time.deltaTime;
		//		}
		//	}
		//}

	}

}