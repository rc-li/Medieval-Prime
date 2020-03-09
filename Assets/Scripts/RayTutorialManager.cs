using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security;
using UnityEngine;

public class RayTutorialManager : MonoBehaviour {
	public GameObject[] popUps;
	public int popUpIndex;
	public float waitTime;
	public bool tutorial_1;
	public Rigidbody2D playerRigidBody;

	void Awake() {
		playerRigidBody = GameObject.Find ("player").GetComponent<Rigidbody2D> ();
	}



	void Update () {

		for (int i = 0; i < popUps.Length; i++) {
			if (i == popUpIndex) {
				popUps[i].SetActive (true);
			} else {
				popUps[i].SetActive (false);
			}
		}

		if (popUpIndex == 0) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse0)) {
				tutorial_1 = true;
			}
		}

		// 	for the first tutorial
		if (tutorial_1 == true) {
			if (waitTime <= 0) {
				playerRigidBody.gravityScale = 0;
				playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, 0);
				waitTime = 0.25f;
				tutorial_1 = false;
				popUpIndex++;
			} else {
				waitTime -= Time.deltaTime;
			}

		}

		if (popUpIndex == 1) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse0)) {
				playerRigidBody.gravityScale = 5;
				playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 5);
			}
		}

	}

}