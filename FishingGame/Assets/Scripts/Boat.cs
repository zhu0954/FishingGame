using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed;
   
    private PlayerActions actions;
    private InputAction movementAction;
    public bool canMove;
   
    private void Awake()
    {
        actions = new PlayerActions();
        movementAction = actions.moving.movement;
    }

    void OnEnable()
    {
        movementAction.Enable();
    }

    void Start()
    {
        canMove = true;

    }

    void OnDisable()
    {
        movementAction.Disable();
    }

    void Update()
    {
      
        if (canMove)
        {
            float acceleration = movementAction.ReadValue<Vector2>().x;
            transform.Translate(Vector2.right * speed * acceleration * Time.deltaTime, Space.World);
            clamp();

            if (acceleration == 1)
            {
                gameObject.transform.localScale = new Vector2(-1, 1);
            }
            if (acceleration == -1)
            {
                gameObject.transform.localScale = new Vector2(1, 1);
            }
        }

    }

    void clamp()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.03f, 0.97f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

   


}
