using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that is attached to the CameraController to set up the follow and lookat fields as well as update mouse sensitivity.
/// </summary>
public class CameraInitializer : MonoBehaviour
{
    CinemachineFreeLook camController;

    // Start is called before the first frame update
    void Start()
    {
        camController = GetComponent<CinemachineFreeLook>();
        GameObject anchor = GameObject.FindGameObjectWithTag("CameraAnchor");

        if (anchor == null)
        {
            Debug.LogWarning("Object with CameraAnchor tag not found in scene. Did you forget to add the Player prefab to the scene?");
            return;
        }
        
        camController.Follow = anchor.transform;
        camController.LookAt = anchor.transform;
    }

    private void Update()
    {
        camController.m_XAxis.m_MaxSpeed = GameManager.instance.GetMouseXSpeed();
        camController.m_YAxis.m_MaxSpeed = GameManager.instance.GetMouseYSpeed();
    }
}
