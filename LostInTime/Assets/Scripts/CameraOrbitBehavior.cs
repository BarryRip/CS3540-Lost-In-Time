using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public Transform player;
    public Transform childSphere;
    public Transform cameraTarget;

    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;

        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * moveX);

        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        childSphere.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        MoveCameraToTarget();
    }

    private void MoveCameraToTarget()
    {
        Rigidbody rb = Camera.main.GetComponent<Rigidbody>();
        rb.Move(cameraTarget.transform.position, cameraTarget.rotation);
    }
}
