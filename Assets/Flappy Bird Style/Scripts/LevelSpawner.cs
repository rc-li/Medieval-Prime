using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] hazardChunks;
    public Sprite[] spriteList;
    public GameObject town;
    SpriteRenderer townSprite;
    [SerializeField]
    private int spawnCount = 0;
    int randomInt;
    void Start()
    {
        InvokeRepeating("spawnChunk", 3.0f, 5f);
        townSprite = town.GetComponent<SpriteRenderer>();
    }

    void spawnChunk()
    {
        if(spawnCount < 10)
        {
            randomInt = Random.Range(0,hazardChunks.Length);
            Instantiate(hazardChunks[randomInt], transform.TransformPoint(new Vector3(35f, -2f, 0f)), Quaternion.identity);
            spawnCount++;
        }
        else
        {
            // Move town to new position
            town.transform.position = new Vector3(35f, -2f, 0f);
            // Change sprite of town here
            townSprite.sprite = spriteList[Random.Range(0, spriteList.Length)];
            spawnCount = 0;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
