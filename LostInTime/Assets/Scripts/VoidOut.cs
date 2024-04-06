using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOut : MonoBehaviour
{
    public AudioClip voidSfx;
    public string voidMessage;
    Vector3 spawnPointPos;
    Vector3 spawnPointRot;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointPos = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        spawnPointRot = GameObject.FindGameObjectWithTag("SpawnPoint").transform.rotation.eulerAngles;
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
            pc.TeleportTo(new Vector3[] { spawnPointPos, spawnPointRot } );
            AudioSource.PlayClipAtPoint(voidSfx, spawnPointPos, 0.3f);
            UiTextManager textManager = FindObjectOfType<UiTextManager>();
            textManager.SetNotificationText(voidMessage);
        }
    }
}
