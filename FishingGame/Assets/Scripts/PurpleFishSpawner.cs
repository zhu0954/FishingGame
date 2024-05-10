using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFishSpawner : MonoBehaviour
{
    [SerializeField] private Fish purpleFishRight;
    private float spawnTime;
    private float timer;
    

    void SpawnPurple()
    {
        timer = spawnTime;
        Fish newFish = Instantiate(purpleFishRight);
        newFish.transform.SetParent(transform);
        newFish.transform.position = new Vector3(15f, Random.Range(8, -3), 0);
    }
    void Start()
    {
        timer = spawnTime;
        spawnTime = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SpawnPurple();
            spawnTime = Random.Range(5, 10);
        }
    }

    
}
