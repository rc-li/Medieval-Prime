using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChenLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        ChenPlayerControl controller = other.GetComponent<ChenPlayerControl >();

        if (controller != null){

            controller.popUpWindow();
                Debug.Log("touch");
        }
    }
}
