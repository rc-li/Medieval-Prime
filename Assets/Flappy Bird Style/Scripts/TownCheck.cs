using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownCheck : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerControl>() != null)
        {
            GameControl.instance.hitNewTown();
        }
    }

    
}
