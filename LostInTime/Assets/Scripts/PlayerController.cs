using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 2f;

    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Handle player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);

        // Move the player
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        float currentSpeed = sprintInput ? moveSpeed * sprintMultiplier : moveSpeed;
        Vector3 moveVelocity = moveDirection * currentSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Jumping
        if (jumpInput && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
