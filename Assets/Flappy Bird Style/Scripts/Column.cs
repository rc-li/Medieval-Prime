using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerControl>() != null)
		{
			//If the player hits the trigger collider
			//tell the game control that the player collected a coin.
			GameControl.instance.CollectedGold();
		}
	}
}
