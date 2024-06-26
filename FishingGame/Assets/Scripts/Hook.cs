using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    private enum HookState
    {
        Idle,
        Hooking,
        Reeling,
    }

    private bool floorHit;
    private bool boatHit;
    private bool fishHit;

    public Boat movement;
    private HookState hookState;

    private PlayerActions actions;
    private InputAction hookAction;

    [Range(0, 20)][SerializeField] private float hookSpeed = 15f;
    private float speed;
    [SerializeField] private float ascentSpeed = 20f;

    private GameObject hookedFish;

    void Awake()
    {
        actions = new PlayerActions();
        hookAction = actions.moving.Hooking;
    }

    void OnEnable()
    {
        hookAction.Enable();
    }

    void OnDisable()
    {
        hookAction.Disable();
    }

    void Start()
    {
        hookState = HookState.Idle;
    }

    void Update()
    {
        switch (hookState)
        {
            case HookState.Idle:
                movement.canMove = true;
                speed = 0;
                if (hookAction.ReadValue<float>() > 0)
                {
                    hookState = HookState.Hooking;
                    speed = hookSpeed;
                }
                break;
            case HookState.Hooking:
                movement.canMove = false;
                if (floorHit)
                {
                    hookState = HookState.Reeling;
                }
                else
                {
                    transform.Translate(Vector2.down * speed * Time.deltaTime, Space.Self);
                }

                if (fishHit)
                {
                    hookState = HookState.Reeling;
                }
                else
                {
                    transform.Translate(Vector2.down * speed * Time.deltaTime, Space.Self);
                }

                break;
            case HookState.Reeling:
                transform.Translate(Vector2.up * ascentSpeed * Time.deltaTime, Space.Self);
                if (boatHit)
                {
                    hookState = HookState.Idle;
                    if (hookedFish != null)
                    {
                        ScoreManager.instance.AddScore(hookedFish.GetComponent<Fish>().scoreValue);
                        Destroy(hookedFish);
                        hookedFish = null;
                    }
                }
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Boundary"))
        {
            floorHit = true;
        }
        else
        {
            floorHit = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (hookState == HookState.Reeling && collision.gameObject.CompareTag("Boat"))
        {
            boatHit = true;
            transform.position = collision.gameObject.transform.position;
        }
        else
        {
            boatHit = false;
        }

        if (collision.gameObject.CompareTag("Fish"))
        {
            fishHit = true;
            hookedFish = collision.gameObject;
        }
        else
        {
            fishHit = false;
        }
    }
}
