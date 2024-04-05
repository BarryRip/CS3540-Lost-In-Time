using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that is attached to the CameraController to set up the follow and lookat fields.
/// </summary>
public class CameraInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject anchor = GameObject.FindGameObjectWithTag("CameraAnchor");

        if (anchor == null)
        {
            Debug.LogWarning("Object with CameraAnchor tag not found in scene. Did you forget to add the Player prefab to the scene?");
            return;
        }

        CinemachineFreeLook camController = GetComponent<CinemachineFreeLook>();
        camController.Follow = anchor.transform;
        camController.LookAt = anchor.transform;
    }
}
