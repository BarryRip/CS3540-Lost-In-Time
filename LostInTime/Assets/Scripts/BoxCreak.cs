using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreak : MonoBehaviour
{
    public AudioClip creakSFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(creakSFX, transform.position);
        }
    }
}
