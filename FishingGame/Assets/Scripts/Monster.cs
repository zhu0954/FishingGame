using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform oceanFloor;
    [SerializeField] private float sightLength = 20f;
    [SerializeField] private float strikeDuration = 2.5f;
    [SerializeField] private float strikeSpeed = 30f;
    [SerializeField] private float descentSpeed = 10f;
    [SerializeField] private float speed = 5f;

    private SpriteRenderer sprite;
    [SerializeField] private Color alertColor;
    [SerializeField] private Color scanningColor;
    [SerializeField] private LayerMask layerMask;

    private bool isPlayerInSight = false;
    private float strikeTimer = 0f;
    private bool Descending = false;
    private bool direction;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!Descending)
        {
            LookForPlayer();
            patrol();

            if (isPlayerInSight)
            {
                strikeTimer += Time.deltaTime;
                if (strikeTimer >= strikeDuration)
                {
                    Strike();
                }
            }
            else
            {
                DescendToOceanFloor();
            }
        }
        else
        {
            DescendToOceanFloor();
        }

    }

    void patrol()
    {
        if (!isPlayerInSight) 
        {
            if (direction)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime, Space.Self);
            }
            strikeTimer = 0;
        }
    }

    void LookForPlayer()
    {
        Vector2 directionToPlayer = target.position - transform.position;
        directionToPlayer.Normalize();
        Debug.DrawRay(transform.position, directionToPlayer * sightLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, sightLength, layerMask);

        if (hit.transform == target)
        {
            isPlayerInSight = true;
            SeesTarget();
        }
        else
        {
            isPlayerInSight = false;
            CantSeeTarget();        
        }
    }

    void SeesTarget()
    {
        sprite.color = alertColor;
    }

    void CantSeeTarget()
    {
        sprite.color = scanningColor;
    }



    void Strike()
    {
        Vector2 strikeLocation = target.position - transform.position;
        transform.Translate(strikeLocation * strikeSpeed * Time.deltaTime, Space.Self);

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Turn"))
        {
            if (direction)
            {
                direction = false;
            }
            else
            {
                direction = true;
            }
        }
        else if (collider.gameObject.CompareTag("Boat"))
        {
            Debug.Log("death");
            HealthManager.instance.DecreaseHealth(1);
            ReturnToOceanFloor();
        }
        else if (collider.gameObject.CompareTag("Water"))
        {
            Debug.Log("nothing");
            ReturnToOceanFloor();
        }
    }
    void ReturnToOceanFloor()
    {
        Descending = true;
        sprite.color = Color.white;
    }

    void DescendToOceanFloor()
    {
        transform.Translate(Vector2.down * descentSpeed * Time.deltaTime, Space.Self);

        if (transform.position.y <= oceanFloor.position.y)
        {
            Descending = false;
            transform.position = new Vector2(transform.position.x, oceanFloor.position.y); 
            isPlayerInSight = false; 
            patrol();
            strikeTimer = 0f;
        }
    }
}

