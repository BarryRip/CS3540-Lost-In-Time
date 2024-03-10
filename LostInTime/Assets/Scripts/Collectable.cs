using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int partID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        // Despawn collectable on load if it was already collected
        if (CollectableManager.collectedIds[partID])
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableManager.collectedIds[partID] = true;
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
