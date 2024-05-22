using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLeft : MonoBehaviour
{
    private float speed;
    private bool caught;
    private float topSpeed = 10f;
    private float botSpeed = 5f;
    public int scoreValue = 10; // Points given for collecting this fish

    void Start()
    {
        speed = Random.Range(botSpeed, topSpeed);
    }

    void Update()
    {
        if (!caught)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            caught = true;
            transform.position = collision.transform.position;
            speed = 0;
        }
        else
        {
            caught = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            Destroy(this.gameObject);
            CollectFish();
        }
    }

    void CollectFish()
    {
        // Add score
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
