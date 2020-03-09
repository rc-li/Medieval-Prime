using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTutorialManager : MonoBehaviour {
	public GameObject[] popUps;
	public int popUpIndex;

	void Update () {

		for (int i = 0; i < popUps.Length; i++) {
			if (i == popUpIndex) {
				popUps[popUpIndex].SetActive (true);
				print("set " + i + " to active");
			} else {
				popUps[popUpIndex].SetActive (false);
				print("set " + i + " to disabled");
			}
		}

		if (popUpIndex == 0 || popUpIndex == 1) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse0)) {
				popUpIndex++;
			}
		}

	}
	

}