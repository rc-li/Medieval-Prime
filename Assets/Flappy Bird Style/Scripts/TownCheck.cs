using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class TownCheck : MonoBehaviour
{



    public static int townNumber = 0;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerControl>() != null)
        {
            townNumber = townNumber + 1;
            GameControl.instance.hitNewTown();
            ReportTownReach(townNumber);
            AnalyticsResult ar = Analytics.CustomEvent("town_reach");
            Debug.Log("Result = " + ar.ToString());


        }

    }


    public void ReportTownReach(int townNumber)
    {
        AnalyticsEvent.Custom("town_reach", new Dictionary<string, object>
        {
            { "town_Number", townNumber }
        });
    }


}
