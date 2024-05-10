using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFishSpawnerLeft : MonoBehaviour
{

    [SerializeField] private FishLeft YellowFishLeft;
    private float spawnTime;
    private float timer;

    void SpawnYellow()
    {
        timer = spawnTime;
        FishLeft yellowFishLeft = Instantiate(YellowFishLeft);
        yellowFishLeft.transform.SetParent(transform);
        yellowFishLeft.transform.position = new Vector3(-15, Random.Range(8, 0), 0);
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
