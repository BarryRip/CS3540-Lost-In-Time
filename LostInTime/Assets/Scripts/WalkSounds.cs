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
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        if (collision.gameObject.CompareTag("Player"))
        {
            if (moveHorizontal < 0 || moveHorizontal > 0 || moveVertical < 0 || moveVertical > 0)
            {
                groundSoundSource.loop = true;
                groundSoundSource.Play();
            }
        }
        else
        {
            groundSoundSource.loop = false;
        }
    }
}
