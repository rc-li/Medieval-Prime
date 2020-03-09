using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject hazardChunks;
    public GameObject town;
    private float spawnXPosition = 10f;
    private int spawnCount = 0;
    void Start()
    {
        InvokeRepeating("spawnChunk", 3.0f, 5f);
    }

    void spawnChunk()
    {
        if(spawnCount < 10)
        {
            Instantiate(hazardChunks, transform.TransformPoint(new Vector3(35f, -2f, 0f)), Quaternion.identity);
            spawnCount++;
        }
        else
        {
            town.transform.position = new Vector3(35f, -2f, 0f);
            spawnCount = 0;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
