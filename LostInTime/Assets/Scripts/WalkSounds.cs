using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounds : MonoBehaviour
{
    public AudioClip groundSFX;
    AudioSource groundSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        groundSoundSource = GetComponent<AudioSource>();
        groundSoundSource.clip = groundSFX;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            groundSoundSource.loop = true;
            groundSoundSource.Play();
        }
        else
        {
            groundSoundSource.loop = false;
        }
    }
}
