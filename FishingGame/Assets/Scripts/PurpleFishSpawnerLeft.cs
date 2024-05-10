using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFishSpawnerLeft : MonoBehaviour
{
    [SerializeField] private FishLeft purpleFishLeft;
    private float spawnTime;
    private float timer;

    void SpawnPurple()
    {
        timer = spawnTime;
        //purple Fish
        FishLeft PurpleFishLeft = Instantiate(purpleFishLeft);
        PurpleFishLeft.transform.SetParent(transform);
        PurpleFishLeft.transform.position = new Vector3(-15f, Random.Range(8, 0), 0);
    }

    void Start()
    {
        timer = spawnTime;
        spawnTime = Random.Range(5, 10);
    }

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
