using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.GetComponent<PlayerControl>() != null)
        {
            GameControl.instance.CollectedGold();
            Destroy(this.gameObject);
        }
    }
}
