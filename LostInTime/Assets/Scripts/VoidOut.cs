using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOut : MonoBehaviour
{
    public AudioClip voidSfx;
    public string voidMessage;
    Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.TeleportTo(spawnPoint);
            AudioSource.PlayClipAtPoint(voidSfx, spawnPoint, 0.3f);
            UiTextManager textManager = FindObjectOfType<UiTextManager>();
            textManager.SetNotificationText(voidMessage);
        }
    }
}
