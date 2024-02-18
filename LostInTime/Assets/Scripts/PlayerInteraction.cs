using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float range = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NpcInteraction npcInteractable))
                {
                    npcInteractable.ShowInteractionButton();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        npcInteractable.Interact();
                    }
                }
                else
                {
                    // If the collider doesn't have NpcInteraction, hide the interact button
                    collider.GetComponent<NpcInteraction>()?.HideInteractionButton();
                }
            }
        }
    }
}
