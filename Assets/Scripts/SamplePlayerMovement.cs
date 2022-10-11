using System;
using UnityEngine;

public class SamplePlayerMovement : MonoBehaviour
{ 
    Rigidbody2D rb;

    float walkspeed = 4f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            rb.velocity = new Vector2(inputHorizontal * walkspeed, inputVertical * walkspeed);
        }
    }
}
