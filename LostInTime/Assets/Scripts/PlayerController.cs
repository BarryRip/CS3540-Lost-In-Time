using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 10f;
    public float gravity = 9.81f;
    public float airControl = 2f;
    public AudioClip walkSfx;

    Animator anim;

    private CharacterController controller;
    private Vector3 movement;
    private AudioSource walkAudioSource;

    private bool hasDoubleJumpCharge;
    // This "wasApplyingGroundingForce" bool is a bit confusing, but
    // essentially, it indicates if a grounding force was applied to
    // the player last frame. Should reset y velocity to 0 if true.
    private bool wasApplyingGroundingForce;
    private static float SLOPE_RAYCAST_LENGTH = 5f;
    private static float GROUNDING_FORCE = 1 / 6f;
    private static float SLOPE_SLIDE_SPEED = 10f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        walkAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = GetNormalizedInput();
        input = CalculateCameraSpaceInput(input);
        ApplyMovement(input);
        HandleJump();
        ApplyGravity();
        HandleSteepSlopes();
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

            // To avoid slopes eating jumps, apply grounding force to player (scaled to slope limit)
            movement.y = controller.slopeLimit * -GROUNDING_FORCE;
            wasApplyingGroundingForce = true;
            
            if (isMoving())
            {
                anim.SetInteger("animState", 1);
                if (!walkAudioSource.isPlaying)
                {
                    walkAudioSource.clip = walkSfx;
                    walkAudioSource.loop = true;
                    walkAudioSource.Play();
                }
            }
            else
            {
                anim.SetInteger("animState", 0);
                walkAudioSource.Stop();
            }
            
        }
        else
        {
            if (wasApplyingGroundingForce)
            {
                wasApplyingGroundingForce = false;
                movement.y = 0f;
                anim.SetInteger("animState", (speed > 0) ? 1 : 0);
            }
            // When airborne, player has loose air control.
            input *= speed;
            input.y = movement.y;
            movement = Vector3.Lerp(movement, input, airControl * Time.deltaTime);
            walkAudioSource.Stop();
        }
    }

    private bool isMoving()
    {
        return Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0
            || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0;
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
                wasApplyingGroundingForce = false;
                movement.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                anim.SetInteger("animState", 2);
            }
            else if (CanDoubleJump() && hasDoubleJumpCharge)
            {
                hasDoubleJumpCharge = false;
                movement.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                anim.SetInteger("animState", 2);
            }
        }
    }

    /// <summary>
    /// Handles checking the slope below the player and changing movement
    /// appropriately. This is done to stop player sticking to slopes.
    /// </summary>
    private void HandleSteepSlopes()
    {
        if (OnSteepSlope())
        {
            wasApplyingGroundingForce = false;
            RaycastHit hit;
            Physics.Raycast(transform.position, -transform.up, out hit, SLOPE_RAYCAST_LENGTH);
            Vector3 tangentDir = Vector3.Cross(hit.normal, transform.up);
            Vector3 downSlopeDir = Vector3.Cross(hit.normal, tangentDir);
            movement = downSlopeDir.normalized * SLOPE_SLIDE_SPEED;
        }
    }

    /// <summary>
    /// Determines if the player is currently on a steep slope.
    /// </summary>
    private bool OnSteepSlope()
    {
        RaycastHit hit;
        if (controller.isGrounded && Physics.Raycast(transform.position, -transform.up, out hit, SLOPE_RAYCAST_LENGTH))
        {
            float angleOfSlope = Vector3.Angle(hit.normal, transform.up);
            if (angleOfSlope > controller.slopeLimit)
            {
                return true;
            }
        }
        return false;
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
        return PowerUpManager.HasAbility(0);
    }
}
