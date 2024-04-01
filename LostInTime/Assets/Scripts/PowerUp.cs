using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int abilityID;
    public AudioClip collectSfx;

    private CollectableMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        // Despawn powerup if already collected
        if (PowerUpManager.HasAbility(abilityID))
        {
            gameObject.SetActive(false);
        }

        movement = GetComponent<CollectableMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PowerUpManager.HasAbility(abilityID))
        {
            PowerUpManager.UnlockAbility(abilityID);
            AudioSource.PlayClipAtPoint(collectSfx, transform.position);
            movement.Disappear();
            Destroy(gameObject, 1);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
