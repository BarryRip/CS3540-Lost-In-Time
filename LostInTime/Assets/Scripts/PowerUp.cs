using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int abilityID;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 1);
        }

    }

    private void OnDestroy()
    {
        PowerUpManager.UnlockAbility(abilityID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
