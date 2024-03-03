using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleport : MonoBehaviour
{
    [Tooltip("The teleporter that this teleporter connects to. If left " +
        "empty, the teleport trigger won't do anything.")]
    public RoomTeleport destination;
    [Tooltip("In the prefab, this should be the pink character model. " +
        "The position and orientation of this object is how the player " +
        "will appear when teleported.")]
    public GameObject playerSpawn;

    void Start()
    {
        playerSpawn.SetActive(false);
    }

    public void TeleportHere()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
        controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && destination != null)
        {
            destination.TeleportHere();
        }
    }
}
