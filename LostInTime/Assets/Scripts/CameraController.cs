using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontalMouseSensitivity = 1000f;
    public float verticalMouseSensitivity = 1000f;
    public Transform player;
    public Transform cameraSphere;

    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * horizontalMouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * verticalMouseSensitivity * Time.deltaTime;

        xRotation += moveX;
        yRotation -= moveY;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);
        cameraSphere.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);

        CenterCameraOnPlayer();
    }

    private void CenterCameraOnPlayer()
    {
        cameraSphere.transform.position = player.transform.position;
    }
}
