using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 10f;
    public float gravity = 9.81f;
    public float airControl = 2f;

    private CharacterController controller;
    private Vector3 movement;

    private bool hasDoubleJumpCharge;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = GetNormalizedInput();
        input = CalculateCameraSpaceInput(input);
        ApplyMovement(input);
        HandleJump();
        ApplyGravity();
        controller.Move(Time.deltaTime * movement);
    }

    private void FixedUpdate()
    {
        UpdateStatusWhenGrounded();
    }

    /// <summary>
    /// Calculates the world space direction of the given input applied to the
    /// camera's world space.
    /// </summary>
    /// <param name="input">The input direction.</param>
    /// <returns>The normalized world space input vector based on the camera.</returns>
    private Vector3 CalculateCameraSpaceInput(Vector3 input)
    {
        Vector3 camDirection = Camera.main.transform.TransformDirection(input);
        camDirection.y = 0;
        return camDirection.normalized;
    }

    /// <summary>
    /// Applies player movement based on the given input.
    /// </summary>
    /// <param name="input">The world space input direction to move the player towards.</param>
    private void ApplyMovement(Vector3 input)
    {
        if (controller.isGrounded)
        {
            // When grounded, player snaps to face the direction we want, and moves forward.
            transform.LookAt(transform.position + input);
            movement = transform.forward * input.magnitude * speed;
        }
        else
        {
            // When airborne, player has loose air control and slowly faces direction we want.
            input.y = movement.y;
            movement = Vector3.Lerp(movement, input, airControl * Time.deltaTime);
            float rotationAngle = Mathf.LerpAngle(0f, Vector3.Angle(transform.forward, input), Time.deltaTime);
            transform.LookAt((transform.position + transform.forward));
        }
    }

    /// <summary>
    /// Handles the jump input to apply jumps and double jumps.
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                movement.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else if (CanDoubleJump() && hasDoubleJumpCharge)
            {
                hasDoubleJumpCharge = false;
                movement.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
        }
    }

    /// <summary>
    /// Apply gravity to the movement vector.
    /// </summary>
    private void ApplyGravity()
    {
        movement.y -= gravity * Time.deltaTime;
    }

    /// <summary>
    /// Returns a normalized vector indicating the direction to move the player
    /// in based on the horizontal and vertical input.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetNormalizedInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 input = Vector3.right * moveHorizontal + Vector3.forward * moveVertical;
        return input.normalized;
    }

    /// <summary>
    /// Updates the player's status when grounded.
    /// <para />
    /// Currently, this will simply give the player a double jump charge when
    /// grounded.
    /// </summary>
    private void UpdateStatusWhenGrounded()
    {
        if (controller.isGrounded)
        {
            hasDoubleJumpCharge = true;
        }
    }

    /// <summary>
    /// Check if the player has unlocked the double jump ability.
    /// <para />
    /// Currently, this simply returns true. In the future, we should connect
    /// this to some GameManager script that tracks the powerups collected
    /// by the player.
    /// </summary>
    /// <returns>True if the player has the double jump ability, false otherwise.</returns>
    private bool CanDoubleJump()
    {
        return true;
    }
}
