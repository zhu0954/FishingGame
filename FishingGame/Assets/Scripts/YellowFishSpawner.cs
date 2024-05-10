using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFishSpawner : MonoBehaviour
{

    [SerializeField] private Fish YellowFishRight;
    private float spawnTime;
    private float timer;

    void SpawnYellow() 
    {
        timer = spawnTime;
        Fish yellowFishRight = Instantiate(YellowFishRight);
        yellowFishRight.transform.SetParent(transform);
        yellowFishRight.transform.position = new Vector3(15f, Random.Range(8, 0), 0);
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
            SpawnYellow();
            spawnTime = Random.Range(5, 10);
        }
    }
}
