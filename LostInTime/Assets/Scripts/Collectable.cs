using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int partID;
    public AudioClip collectSfx;
    public GameObject type1Model;
    public GameObject type2Model;
    public GameObject type3Model;

    private CollectableMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        // Despawn collectable on load if it was already collected
        if (GameManager.instance.collectedParts[partID])
        {
            gameObject.SetActive(false);
        }
        type1Model.SetActive(CollectableManager.GetType(partID) == CollectableManager.PartType.TYPE_1);
        type2Model.SetActive(CollectableManager.GetType(partID) == CollectableManager.PartType.TYPE_2);
        type3Model.SetActive(CollectableManager.GetType(partID) == CollectableManager.PartType.TYPE_3);
        movement = GetComponent<CollectableMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.instance.collectedParts[partID])
        {
            CollectableManager.GetPart(partID);
            AudioSource.PlayClipAtPoint(collectSfx, transform.position);
            movement.Disappear();
            Destroy(gameObject, 1);
        }

    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
