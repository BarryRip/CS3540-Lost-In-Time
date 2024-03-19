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
        groundSoundSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        groundSoundSource.clip = groundSFX;
    }

    public void PlaySfxWhenWalking(GameObject obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (!groundSoundSource.isPlaying)
            {
                groundSoundSource.loop = true;
                groundSoundSource.Play();
            }
        }
    }

    public void StopSfx()
    {
        groundSoundSource.Stop();
    }
}
