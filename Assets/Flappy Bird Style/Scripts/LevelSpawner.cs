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
        InvokeRepeating("spawnChunk", 2.0f, 3f);
    }

    void spawnChunk()
    {
        if(spawnCount < 10)
        {
            Instantiate(hazardChunks, transform.TransformPoint(new Vector3(5f, -3.5f, 0f)), Quaternion.identity);
            spawnCount++;
        }
        else
        {
            Instantiate(town, transform.TransformPoint(new Vector3(5f, -3.5f, 0f)), Quaternion.identity);
            spawnCount = 0;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
