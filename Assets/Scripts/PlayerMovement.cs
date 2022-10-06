using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    [SerializeField] private float speed = 200f;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        if (inputVector.x != 0 || inputVector.y != 0)
        {
            rbody.velocity = new Vector2(inputVector.x * speed, inputVector.y * speed);
        }
        else
        {
            rbody.AddForce((-rbody.velocity) * Time.deltaTime * 150);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}