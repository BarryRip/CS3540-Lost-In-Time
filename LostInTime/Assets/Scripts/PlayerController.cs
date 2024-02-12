using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 10f;
    public float gravity = 9.81f;
    public float airControl = 2f;
    public Transform playerCamera;

    private CharacterController controller;
    
    private bool hasDoubleJumpCharge;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfLanded();
        Vector3 input = GetNormalizedInput();
        Vector3 forwardPos = (Camera.main.transform.forward + transform.position);
        forwardPos = new Vector3(forwardPos.x, transform.position.y, forwardPos.z);
        transform.LookAt(forwardPos);
        controller.Move(speed * Time.deltaTime * input);
    }

    private Vector3 GetNormalizedInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 input = transform.right * moveHorizontal + transform.forward * moveVertical;
        return input.normalized;
    }

    private void CheckIfLanded()
    {
        if (controller.isGrounded)
        {
            hasDoubleJumpCharge = true;
        }
    }

    private bool CanDoubleJump()
    {
        return false;
    }
}
